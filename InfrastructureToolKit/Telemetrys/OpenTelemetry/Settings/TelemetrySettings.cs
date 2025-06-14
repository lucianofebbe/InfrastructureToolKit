using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureToolKit.Telemetrys.OpenTelemetry.Settings
{
    public record TelemetrySettings
    {
        public IServiceCollection services{ get; set; }
        public string serviceName { get; set; }
        public string serviceVersion { get; set; }
        public string activitySourceName { get; set; }
    }
}
