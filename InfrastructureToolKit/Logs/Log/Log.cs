using InfrastructureToolKit.Interfaces.Logs.Log;
using InfrastructureToolKit.Settings.Logs.Settings;
using System.Text;

namespace InfrastructureToolKit.Logs.Log
{
    public class Log : ILog
    {
        // Gera uma string formatada para envio por email contendo detalhes do log
        public virtual async Task<LogSettingsResultSettings> GenerateLog(LogSettingsCreateSettings LogSettings)
        {
            var strBuilder = new StringBuilder();

            // Linha separadora visual
            strBuilder.Append("----------------------------------------------------//");
            strBuilder.Append("----------------------------------------------------" + Environment.NewLine);

            // Data e hora do log no formato UTC
            strBuilder.Append("Data: " + DateTime.UtcNow.ToString() + "." + Environment.NewLine);

            // Informações da exceção capturada (se houver)
            strBuilder.Append("Exception: " + LogSettings.Exception + "." + Environment.NewLine);

            // Informações da exceção capturada (se houver)
            strBuilder.Append("StackTrace: " + LogSettings.Exception.StackTrace + "." + Environment.NewLine);

            // Linha separadora visual
            strBuilder.Append("----------------------------------------------------//");
            strBuilder.Append("----------------------------------------------------" + Environment.NewLine);

            var result = new LogSettingsResultSettings() { Result = strBuilder.ToString() };
            return result;
        }
    }
}
