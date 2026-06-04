using Npgsql;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Exporter;

namespace InventorySystem.Configs;

public static class OpentelemetryConfig
{
    public static IServiceCollection AddOpenTelemetryConfig(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder logging)
    {
        services.AddOpenTelemetry()
        .WithTracing(tracing => tracing //tracing
            .AddAspNetCoreInstrumentation() // Automatic tracking request HTTP in
            .AddHttpClientInstrumentation() // Automatic tracking request HTTP out
            .AddNpgsql() // Automatic tracking query to database
            .AddOtlpExporter(ops =>
            {
                ops.Endpoint = new Uri(configuration["Otel:Exporter:Otlp:Endpoint"]!);
                ops.Protocol = OtlpExportProtocol.HttpProtobuf;
            }))
        .WithMetrics(metrics => metrics // metrics
            .AddAspNetCoreInstrumentation() // Automatic tracking request HTTP in
            .AddHttpClientInstrumentation() // Automatic tracking request HTTP out
            .AddRuntimeInstrumentation() // Automatic tracking resources CPU, memory, dll
            .AddOtlpExporter(ops =>
            {
                ops.Endpoint = new Uri(configuration["Otel:Exporter:Otlp:Endpoint"]!);
                ops.Protocol = OtlpExportProtocol.HttpProtobuf;
            }));

        logging.AddOpenTelemetry(logging => // logging
        {
            logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration["Otel:ServiceName"]!));
            logging.AddOtlpExporter(ops =>
                {
                    ops.Endpoint = new Uri(configuration["Otel:Exporter:Otlp:Endpoint"]!);
                    ops.Protocol = OtlpExportProtocol.HttpProtobuf;
                });
        });
        return services;
    }
}