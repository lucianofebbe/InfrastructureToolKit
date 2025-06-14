using InfrastructureToolKit.Logs.Settings;

namespace InfrastructureToolKit.Interfaces.Logs.Log
{
    // Interface que define as operações para geração de logs.
    public interface ILog
    {
        // Gera um log estruturado e retorna as configurações do log gerado.
        Task<LogSettingsResultSettings> GenerateLog(LogSettingsCreateSettings LogSettings);
    }
}
