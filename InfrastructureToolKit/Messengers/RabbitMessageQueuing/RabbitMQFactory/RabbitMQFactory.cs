using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Default;
using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Factory;
using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.RequestReply;
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.Default;
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.RequestReply;
using InfrastructureToolKit.Settings.Messengers.RabbitMessageQueuing.Settings;

namespace InfrastructureToolKit.Messengers.RabbitMessageQueuing.RabbitMQFactory
{
    // Classe responsável por criar instâncias de componentes RabbitMQ com diferentes propósitos
    public class RabbitMQFactory : IRabbitMQFactory
    {
        // Cria e retorna uma instância do produtor/consumidor padrão com configurações explícitas
        public virtual async Task<IRabbitMQDefault> CreateDefault(AuthenticationSettings AuthenticationSettings, ServerSettings ServerSettings)
        {
            IRabbitMQDefault fac = new RabbitMQDefault(AuthenticationSettings, ServerSettings);
            return fac;
        }

        // Cria e retorna uma instância com suporte a comunicação do tipo Request-Reply com configurações explícitas
        public virtual async Task<IRabbitMQRequestReply> CreateRequestReply(AuthenticationSettings AuthenticationSettings, ServerSettings ServerSettings)
        {
            IRabbitMQRequestReply fac = new RabbitMQRequestReply(AuthenticationSettings, ServerSettings);
            return fac;
        }
    }
}
