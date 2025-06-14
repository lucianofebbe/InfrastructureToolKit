using InfrastructureToolKit.Interfaces.Logs.Log;

namespace InfrastructureToolKit.Interfaces.Logs.LogFactory
{
    // Interface para a fábrica de criação de instâncias de logs.
    public interface ILogFactory
    {
        // Cria uma instância de ILog com configurações fornecidas pelo LogComposerSettings.
        Task<ILog> Create();
    }
}
