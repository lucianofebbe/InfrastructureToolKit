using InfrastructureToolKit.Settings.Messengers.RabbitMessageQueuing.Settings;

namespace InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.RequestReply
{
    // Interface para comunicação RabbitMQ no padrão Request-Reply
    public interface IRabbitMQRequestReply : IAsyncDisposable
    {
        // Método para enviar mensagens (Producer)
        Task ProducerAsync(MessageSettings messageSettings, QueueSettings queueSettings);

        // Método para consumir mensagens (Consumer)
        Task ConsumerAsync(MessageSettings messageSettings, QueueSettings queueSettings);
    }
}
