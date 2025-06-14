using InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.Telemetry;
using InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.TelemetryFactory;
using InfrastructureToolKit.Telemetrys.OpenTelemetry.Settings;

namespace InfrastructureToolKit.Telemetrys.OpenTelemetry.TelemetryFactory
{
    // Fábrica responsável por criar instâncias de ITelemetryBuilder
    public class TelemetryBuilderFactory : ITelemetryBuilderFactory
    {
        // Cria uma instância de ITelemetryBuilder utilizando as configurações fornecidas
        public async Task<ITelemetryBuilder> Create(TelemetrySettings telemetrySettings)
        {
            ITelemetryBuilder fac = new TelemetryBuilder(telemetrySettings);
            return fac;
        }
    }
}
