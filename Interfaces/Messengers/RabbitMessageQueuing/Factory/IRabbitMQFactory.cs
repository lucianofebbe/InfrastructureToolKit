using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Default;
using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.RequestReply;
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.Settings;

namespace InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Factory
{
    // Interface para criação de instâncias do RabbitMQ com diferentes modos de operação
    public interface IRabbitMQFactory
    {
        // Cria uma instância padrão do RabbitMQ com configurações personalizadas
        Task<IRabbitMQDefault> CreateDefault(AuthenticationSettings AuthenticationSettings, ServerSettings ServerSettings);

        // Cria uma instância para comunicação Request-Reply com configurações personalizadas
        Task<IRabbitMQRequestReply> CreateRequestReply(AuthenticationSettings AuthenticationSettings, ServerSettings ServerSettings);
    }
}
