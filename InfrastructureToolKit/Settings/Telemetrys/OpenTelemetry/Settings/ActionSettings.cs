using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace InfrastructureToolKit.Settings.Telemetrys.OpenTelemetry.Settings
{
    public record ActionSettings
    {
        public Action<TracerProviderBuilder>? tracing { get; set; }
        public Action<MeterProviderBuilder>? metrics { get; set; }
    }
}
