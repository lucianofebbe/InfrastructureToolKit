using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.RequestReply;
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;

namespace InfrastructureToolKit.Messengers.RabbitMessageQueuing.RequestReply
{
    // Implementa o padrão Request-Reply usando RabbitMQ
    public class RabbitMQRequestReply : IRabbitMQRequestReply
    {
        private AuthenticationSettings authenticationSettings;
        private ServerSettings serverSettings;
        private IChannel channel;
        private IConnection connection;

        // Mapeia correlationIds com os objetos que aguardam resposta (resolvendo a Task)
        private ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper;

        public RabbitMQRequestReply(AuthenticationSettings authenticationSettings, ServerSettings serverSettings)
        {
            this.authenticationSettings = authenticationSettings;
            this.serverSettings = serverSettings;
            callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        // Envia uma mensagem e aguarda uma resposta assincronamente
        public virtual async Task ProducerAsync(MessageSettings messageSettings, QueueSettings queueSettings)
        {
            await AuthenticateAsync();
            await StartChannelProducerAsync(queueSettings);

            string correlationId = Guid.NewGuid().ToString();
            var props = new BasicProperties
            {
                CorrelationId = correlationId,
                ReplyTo = queueSettings.QueueNameReply
            };

            var messageBytes = Encoding.UTF8.GetBytes(messageSettings.Message);
            await channel.BasicPublishAsync(
                exchange: serverSettings.Exchange,
                routingKey: queueSettings.RoutingKey,
                mandatory: serverSettings.Mandatory,
                basicProperties: props,
                body: messageBytes);

            // Cria Task de resposta e adiciona ao mapa de callbacks
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            callbackMapper.TryAdd(correlationId, tcs);

            // Cancela a Task se o token for cancelado
            using CancellationTokenRegistration ctr = messageSettings.CancellationToken.Register(() =>
            {
                callbackMapper.TryRemove(correlationId, out _);
                tcs.SetCanceled();
            });

            // Aguarda resposta
            messageSettings.MessageResult = await tcs.Task;
        }

        // Consome uma mensagem, processa e responde
        public virtual async Task ConsumerAsync(MessageSettings messageSettings, QueueSettings queueSettings)
        {
            await AuthenticateAsync();
            await StartChannelConsumerAsync(queueSettings);

            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                AsyncEventingBasicConsumer cons = (AsyncEventingBasicConsumer)sender;
                IChannel ch = cons.Channel;
                string response = string.Empty;

                byte[] body = ea.Body.ToArray();
                IReadOnlyBasicProperties props = ea.BasicProperties;

                var replyProps = new BasicProperties
                {
                    CorrelationId = props.CorrelationId
                };

                try
                {
                    var message = Encoding.UTF8.GetString(body);

                    // Invoca delegate para processar a mensagem recebida
                    if (messageSettings.OnMessageReceivedAsync is not null)
                        response = await messageSettings.OnMessageReceivedAsync.Invoke(message);
                    else
                        response = "Fail Process";
                }
                catch (Exception e)
                {
                    Console.WriteLine($" [.] {e.Message}");
                    response = string.Empty;
                }
                finally
                {
                    // Publica a resposta para o remetente original
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    await ch.BasicPublishAsync(
                        exchange: serverSettings.Exchange,
                        routingKey: props.ReplyTo!,
                        mandatory: serverSettings.Mandatory,
                        basicProperties: replyProps,
                        body: responseBytes);

                    // Confirma o processamento da mensagem
                    await ch.BasicAckAsync(
                        deliveryTag: ea.DeliveryTag,
                        multiple: serverSettings.Multiple);
                }
            };

            await channel.BasicConsumeAsync(
                queueSettings.QueueName,
                serverSettings.AutoAck,
                consumer);
        }

        // Garante conexão e canal abertos
        private async Task AuthenticateAsync()
        {
            if (connection != null && channel != null && channel.IsOpen)
                return;

            var connectionFactory = new ConnectionFactory
            {
                HostName = authenticationSettings.HostName,
                UserName = authenticationSettings.UserName,
                Password = authenticationSettings.Password,
                Port = authenticationSettings.Port
            };

            connection = await connectionFactory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
        }

        // Inicializa o canal para consumo (fila e binding)
        private async Task StartChannelConsumerAsync(QueueSettings queueSettings)
        {
            if (!string.IsNullOrEmpty(serverSettings.Exchange))
            {
                await channel.ExchangeDeclareAsync(
                    exchange: serverSettings.Exchange,
                    type: serverSettings.ExchangeType,
                    durable: serverSettings.Durable,
                    autoDelete: serverSettings.AutoDelete);
            }

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

        // Inicializa o canal para produção (resposta)
        private async Task StartChannelProducerAsync(QueueSettings queueSettings)
        {
            await channel.QueueDeclareAsync(
                queue: queueSettings.QueueNameReply,
                durable: serverSettings.Durable,
                exclusive: serverSettings.Exclusive,
                autoDelete: serverSettings.AutoDelete);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                string? correlationId = ea.BasicProperties.CorrelationId;

                if (!string.IsNullOrEmpty(correlationId))
                {
                    // Ao receber a resposta, resolve a Task pendente correspondente
                    if (callbackMapper.TryRemove(correlationId, out var tcs))
                    {
                        var body = ea.Body.ToArray();
                        var response = Encoding.UTF8.GetString(body);
                        tcs.TrySetResult(response);
                    }
                }

                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: queueSettings.QueueNameReply,
                autoAck: serverSettings.AutoAck,
                consumer: consumer);
        }

        // Libera recursos de canal e conexão
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
