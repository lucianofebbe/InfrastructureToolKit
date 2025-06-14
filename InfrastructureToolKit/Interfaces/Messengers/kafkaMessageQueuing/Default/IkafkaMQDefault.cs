using InfrastructureToolKit.Messengers.kafkaMessageQueuing.Settings;

namespace InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Default
{
    // Interface para o uso padrão do Kafka Message Queuing (Produtor e Consumidor)
    public interface IkafkaMQDefault : IAsyncDisposable
    {
        // Método assíncrono para consumir mensagens do Kafka
        Task ConsumerAsync(MessageSettings MessageSettings, QueueSettings QueueSettings);

        // Método assíncrono para produzir/enviar mensagens para o Kafka
        Task ProducerAsync(MessageSettings MessageSettings, QueueSettings QueueSettings);
    }
}
