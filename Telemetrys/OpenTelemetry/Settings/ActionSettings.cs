using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace InfrastructureToolKit.Telemetrys.OpenTelemetry.Settings
{
    public record ActionSettings
    {
        public Action<TracerProviderBuilder>? tracing { get; set; }
        public Action<MeterProviderBuilder>? metrics { get; set; }
    }
}
