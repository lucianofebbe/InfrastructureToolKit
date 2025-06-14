using InfrastructureToolKit.Interfaces.Logs.Log;
using InfrastructureToolKit.Interfaces.Logs.LogFactory;

namespace InfrastructureToolKit.Logs.LogFactory
{
    public class LogFactory : ILogFactory
    {
        // Cria um ILog usando as configurações recebidas como parâmetro
        public Task<ILog> Create()
        {
            ILog log = new Log();
            return Task.FromResult(log);
        }
    }
}
