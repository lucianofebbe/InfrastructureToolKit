using InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Default;
using InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Factory;
using InfrastructureToolKit.Messengers.kafkaMessageQueuing.Default;
using InfrastructureToolKit.Settings.Messengers.kafkaMessageQueuing.Settings;

namespace InfrastructureToolKit.Messengers.kafkaMessageQueuing.kafkaMQFactory
{
    // Fábrica para criar instâncias padrão do KafkaMQ com configurações específicas
    public class kafkaMQFactory : IkafkaMQFactory
    {
        // Cria uma instância padrão do KafkaMQ usando configurações passadas no método
        public virtual async Task<IkafkaMQDefault> CreateDefault(AuthenticationSettings AuthenticationSettings)
        {
            IkafkaMQDefault fac = new kafkaMQDefault(AuthenticationSettings);
            return fac;
        }
    }
}
