using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Default;
using InfrastructureToolKit.Settings.Messengers.RabbitMessageQueuing.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;

namespace InfrastructureToolKit.Messengers.RabbitMessageQueuing.Default
{
    public class RabbitMQDefault : IRabbitMQDefault
    {
        private AuthenticationSettings authenticationSettings;
        private ServerSettings serverSettings;

        // Canal de comunicação com RabbitMQ
        private IChannel channel;

        // Conexão com o servidor RabbitMQ
        private IConnection connection;

        // Dicionário para mapear CorrelationId e TaskCompletionSource para resposta assíncrona
        private ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper;

        // Construtor que recebe as configurações e inicializa o callbackMapper
        public RabbitMQDefault(AuthenticationSettings authenticationSettings, ServerSettings serverSettings)
        {
            this.authenticationSettings = authenticationSettings;
            this.serverSettings = serverSettings;
            callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        // Método para iniciar o consumidor (consumer) da fila RabbitMQ
        public virtual async Task ConsumerAsync(MessageSettings messageSettings, QueueSettings queueSettings)
        {
            // Autentica e cria canal para consumo
            await AuthenticateAsync();
            await StartChannelConsumerAsync(queueSettings);

            // Cria um consumidor assíncrono para escutar mensagens da fila
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                // Extrai a mensagem do body recebido
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Executa callback configurado para mensagem recebida, se existir
                if (messageSettings.OnMessageReceivedAsync is not null)
                    await messageSettings.OnMessageReceivedAsync.Invoke(message);
            };

            // Começa a consumir mensagens da fila configurada, respeitando a flag autoAck
            await channel.BasicConsumeAsync(
                queue: queueSettings.QueueName,
                autoAck: serverSettings.AutoAck,
                consumer: consumer);
        }

        // Método para enviar mensagens (producer) para o RabbitMQ
        public virtual async Task ProducerAsync(MessageSettings messageSettings, QueueSettings queueSettings)
        {
            // Autentica e cria canal para produção
            await AuthenticateAsync();
            await StartChannelProducerAsync(queueSettings);

            // Codifica a mensagem para bytes
            var body = Encoding.UTF8.GetBytes(messageSettings.Message);

            // Publica a mensagem no exchange com a routing key configurada
            await channel.BasicPublishAsync(
                exchange: serverSettings.Exchange,
                routingKey: queueSettings.RoutingKey,
                mandatory: serverSettings.Mandatory,
                body: body);
        }

        // Método que autentica e cria a conexão e canal se ainda não estiverem abertos
        private async Task AuthenticateAsync()
        {
            if (connection != null && channel != null && channel.IsOpen)
                return; // Já conectado e canal aberto

            // Cria uma fábrica de conexões com as configurações de autenticação
            var connectionFactory = new ConnectionFactory
            {
                HostName = authenticationSettings.HostName,
                UserName = authenticationSettings.UserName,
                Password = authenticationSettings.Password,
                Port = authenticationSettings.Port
            };

            // Cria conexão e canal assíncronos
            connection = await connectionFactory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
        }

        // Método para configurar o canal do produtor, declarando fila de reply e consumindo mensagens de resposta
        private async Task StartChannelProducerAsync(QueueSettings queueSettings)
        {
            // Declara fila para replies, com as configurações passadas
            await channel.QueueDeclareAsync(
                queue: queueSettings.QueueNameReply,
                durable: serverSettings.Durable,
                exclusive: serverSettings.Exclusive,
                autoDelete: serverSettings.AutoDelete);

            // Consumidor para tratar mensagens de reply assíncronas
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                string? correlationId = ea.BasicProperties.CorrelationId;

                // Verifica se a mensagem tem CorrelationId para mapear o callback correto
                if (!string.IsNullOrEmpty(correlationId))
                {
                    if (callbackMapper.TryRemove(correlationId, out var tcs))
                    {
                        var body = ea.Body.ToArray();
                        var response = Encoding.UTF8.GetString(body);
                        tcs.TrySetResult(response); // Completa a Task com a resposta
                    }
                }

                return Task.CompletedTask;
            };

            // Inicia consumo da fila de replies
            await channel.BasicConsumeAsync(
                queue: queueSettings.QueueNameReply,
                autoAck: serverSettings.AutoAck,
                consumer: consumer);
        }

        // Método para configurar o canal do consumidor, declarando exchange e fila conforme configurações
        private async Task StartChannelConsumerAsync(QueueSettings queueSettings)
        {
            // Declara exchange se configurada
            if (!string.IsNullOrEmpty(serverSettings.Exchange))
            {
                await channel.ExchangeDeclareAsync(
                    exchange: serverSettings.Exchange,
                    type: serverSettings.ExchangeType,
                    durable: serverSettings.Durable,
                    autoDelete: serverSettings.AutoDelete);
            }

            // Se configurado para criar fila, declara e vincula ao exchange
            if (serverSettings.CreateQueue)
            {
                await channel.QueueDeclareAsync(
                    queue: queueSettings.QueueName,
                    durable: serverSettings.Durable,
                    exclusive: serverSettings.Exclusive,
                    autoDelete: serverSettings.AutoDelete,
                    arguments: queueSettings.Arguments);

                if (!string.IsNullOrEmpty(serverSettings.Exchange))
                {
                    await channel.QueueBindAsync(
                        queue: queueSettings.QueueName,
                        exchange: serverSettings.Exchange,
                        routingKey: queueSettings.RoutingKey);
                }
            }
        }

        // Método para fechar conexões e liberar recursos assincronamente
        public virtual async ValueTask DisposeAsync()
        {
            if (channel != null)
            {
                await channel.CloseAsync();
                channel.Dispose();
                channel = null!;
            }

            if (connection != null)
            {
                await connection.CloseAsync();
                connection.Dispose();
                connection = null!;
            }
        }
    }
}
