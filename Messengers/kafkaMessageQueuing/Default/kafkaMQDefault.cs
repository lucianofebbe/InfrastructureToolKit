using Confluent.Kafka;
using InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Default;
using InfrastructureToolKit.Messengers.kafkaMessageQueuing.Settings;

namespace InfrastructureToolKit.Messengers.kafkaMessageQueuing.Default
{
    // Implementação padrão do cliente Kafka para produção e consumo de mensagens
    public class kafkaMQDefault : IkafkaMQDefault
    {
        private AuthenticationSettings AuthenticationSettings;

        // Recebe as configurações necessárias para conexão e mensagens Kafka
        public kafkaMQDefault(AuthenticationSettings AuthenticationSettings)
        {
            this.AuthenticationSettings = AuthenticationSettings;
        }

        // Método assíncrono para consumir mensagens do tópico configurado
        public virtual async Task ConsumerAsync(MessageSettings MessageSettings, QueueSettings QueueSettings)
        {
            // Cria consumidor Kafka com as configurações de autenticação
            using var consumer = new ConsumerBuilder<Ignore, string>(await AuthenticateAsync()).Build();

            try
            {
                // Inscreve no tópico definido nas configurações
                consumer.Subscribe(QueueSettings.Topic);

                // Consome uma mensagem usando token de cancelamento
                var cr = consumer.Consume(MessageSettings.CancellationToken);

                // Invoca callback se configurado para processar a mensagem recebida
                if (MessageSettings.OnMessageReceivedAsync is not null)
                    await MessageSettings.OnMessageReceivedAsync.Invoke(cr.Message.Value);
            }
            catch { }
            finally
            {
                // Garante fechamento correto do consumidor
                consumer.Close();
            }

        }

        // Método assíncrono para produzir/enviar mensagem para o tópico Kafka
        public virtual async Task ProducerAsync(MessageSettings MessageSettings, QueueSettings QueueSettings)
        {
            // Cria produtor Kafka com as configurações de autenticação
            using var producer = new ProducerBuilder<Null, string>(await AuthenticateAsync()).Build();

            try
            {
                // Envia a mensagem configurada para o tópico
                await producer.ProduceAsync(QueueSettings.Topic,
                    new Message<Null, string> { Value = MessageSettings.Message });

                // Marca entrega como bem-sucedida
                MessageSettings.DeliveryStatus = true;
            }
            catch
            {
                // Marca entrega como falha em caso de exceção
                MessageSettings.DeliveryStatus = false;
            }
            finally
            {
                // Garante que o produtor finalize o envio das mensagens pendentes
                producer.Flush();
            }
        }

        // Configura as opções de autenticação para conexão com Kafka
        private async Task<ProducerConfig> AuthenticateAsync()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = AuthenticationSettings.Server,
                SecurityProtocol = (SecurityProtocol)AuthenticationSettings.Protocol,
                SaslMechanism = (SaslMechanism)AuthenticationSettings.Sasl,
                SaslUsername = AuthenticationSettings.Username,
                SaslPassword = AuthenticationSettings.Password
            };

            return config;
        }

        // Implementação pendente do método DisposeAsync (limpeza de recursos)
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
