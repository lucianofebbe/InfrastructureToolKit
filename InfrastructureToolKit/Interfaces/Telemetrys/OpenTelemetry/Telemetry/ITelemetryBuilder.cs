using InfrastructureToolKit.Telemetrys.OpenTelemetry.Settings;

namespace InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.Telemetry
{
    // Interface para construir a configuração de Telemetria OpenTelemetry
    public interface ITelemetryBuilder
    {
        // Adiciona configuração de tracing e métricas, com base em configurações específicas
        Task<TelemetryBuilder> AddTracingAndMetrics(ActionSettings tctionSettings);

        // Adiciona apenas a configuração de tracing (rastreio de atividades)
        Task<TelemetryBuilder> AddTracing();

        // Adiciona apenas a configuração de métricas (coleta de métricas customizadas e padrões)
        Task<TelemetryBuilder> AddMetrics();
    }
}
