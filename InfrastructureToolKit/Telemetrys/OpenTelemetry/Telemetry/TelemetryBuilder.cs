using InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.Telemetry;
using InfrastructureToolKit.Settings.Telemetrys.OpenTelemetry.Settings;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public class TelemetryBuilder : ITelemetryBuilder
{
    private TelemetrySettings telemetrySettings;
    private ActionSettings tctionSettings;
    private bool _openTelemetryRegistered;

    public TelemetryBuilder(TelemetrySettings telemetrySettings)
    {
        this.telemetrySettings = telemetrySettings;
    }

    /// <summary>
    /// Registra simultaneamente os providers de tracing e métricas.
    /// Deve ser chamado apenas uma vez por aplicação.
    /// </summary>
    public async Task<TelemetryBuilder> AddTracingAndMetrics(ActionSettings actionSettings)
    {
        this.tctionSettings = actionSettings;
        EnsureOpenTelemetryNotRegistered();

        // Propagadores padrão (W3C + Baggage)
        Sdk.SetDefaultTextMapPropagator(new CompositeTextMapPropagator(new TextMapPropagator[]
        {
            new TraceContextPropagator(),
            new BaggagePropagator()
        }));

        telemetrySettings.services.AddOpenTelemetry()
            .WithTracing(builder =>
            {
                builder.AddSource(telemetrySettings.activitySourceName);
                builder.SetResourceBuilder(CreateSharedResourceBuilder());
                actionSettings.tracing?.Invoke(builder);
            })
            .WithMetrics(builder =>
            {
                builder.AddMeter(telemetrySettings.activitySourceName);
                builder.SetResourceBuilder(CreateSharedResourceBuilder());
                actionSettings.metrics?.Invoke(builder);
            });

        _openTelemetryRegistered = true;

        return this;
    }

    /// <summary>
    /// Registra apenas o tracing. Só pode ser chamado se AddTracingAndMetrics ou AddMetrics ainda não tiverem sido usados.
    /// </summary>
    public async Task<TelemetryBuilder> AddTracing()
    {
        EnsureOpenTelemetryNotRegistered();

        Sdk.SetDefaultTextMapPropagator(new CompositeTextMapPropagator(new TextMapPropagator[]
        {
            new TraceContextPropagator(),
            new BaggagePropagator()
        }));

        telemetrySettings.services.AddOpenTelemetry().WithTracing(builder =>
        {
            builder.AddSource(telemetrySettings.activitySourceName);
            builder.SetResourceBuilder(CreateSharedResourceBuilder());
            tctionSettings.tracing?.Invoke(builder);
        });

        _openTelemetryRegistered = true;
        return this;
    }

    /// <summary>
    /// Registra apenas as métricas. Só pode ser chamado se AddTracingAndMetrics ou AddTracing ainda não tiverem sido usados.
    /// </summary>
    public async Task<TelemetryBuilder> AddMetrics()
    {
        EnsureOpenTelemetryNotRegistered();

        telemetrySettings.services.AddOpenTelemetry().WithMetrics(builder =>
        {
            builder.AddMeter(telemetrySettings.activitySourceName);
            builder.SetResourceBuilder(CreateSharedResourceBuilder());
            tctionSettings.metrics?.Invoke(builder);
        });

        _openTelemetryRegistered = true;
        return this;
    }

    /// <summary>
    /// Garante que o OpenTelemetry ainda não foi registrado.
    /// </summary>
    private void EnsureOpenTelemetryNotRegistered()
    {
        if (_openTelemetryRegistered || telemetrySettings.services.Any(s => s.ServiceType == typeof(TracerProvider) || s.ServiceType == typeof(MeterProvider)))
            throw new InvalidOperationException("OpenTelemetry já foi registrado. Não é possível registrar novamente.");
    }

    /// <summary>
    /// Cria um ResourceBuilder compartilhado com nome do serviço e versão.
    /// </summary>
    private ResourceBuilder CreateSharedResourceBuilder()
    {
        return ResourceBuilder.CreateDefault()
            .AddService(serviceName: telemetrySettings.serviceName, serviceVersion: telemetrySettings.serviceVersion);
    }
}
