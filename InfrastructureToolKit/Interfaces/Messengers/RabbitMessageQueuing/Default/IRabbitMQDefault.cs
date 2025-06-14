
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.Settings;

namespace InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Default
{
    // Interface que representa uma instância padrão do RabbitMQ com capacidade de produção e consumo assíncrono de mensagens
    public interface IRabbitMQDefault : IAsyncDisposable
    {
        // Método assíncrono para enviar mensagens (produtor)
        Task ProducerAsync(MessageSettings messageSettings, QueueSettings queueSettings);

        // Método assíncrono para consumir mensagens (consumidor)
        Task ConsumerAsync(MessageSettings messageSettings, QueueSettings queueSettings);
    }
}
