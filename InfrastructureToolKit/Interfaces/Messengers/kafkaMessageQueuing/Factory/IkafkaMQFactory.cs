using InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Default;
using InfrastructureToolKit.Messengers.kafkaMessageQueuing.Settings;

namespace InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Factory
{
    // Interface responsável por criar instâncias padrão de KafkaMQ
    public interface IkafkaMQFactory
    {
        // Cria uma instância padrão do KafkaMQ usando configurações específicas
        Task<IkafkaMQDefault> CreateDefault(AuthenticationSettings AuthenticationSettings);
    }
}
