using InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.Telemetry;
using InfrastructureToolKit.Telemetrys.OpenTelemetry.Settings;

namespace InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.TelemetryFactory
{
    // Interface para criação de instâncias de ITelemetryBuilder
    public interface ITelemetryBuilderFactory
    {
        // Cria uma instância de ITelemetryBuilder com base nas configurações fornecidas
        Task<ITelemetryBuilder> Create(TelemetrySettings telemetrySettings);
    }
}
