using DotnetTakeoff.Api.Infrastructure;
using DotnetTakeoff.Api.Infrastructure.Logging;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Debugging;
using Serilog.Enrichers.Span;

namespace DotnetTakeoff.Api.Extensions;

internal static class LoggingExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        SelfLog.Enable(Console.Error);

        builder.Host.UseSerilog((ctx, services, logConfig) =>
        {
            logConfig.ReadFrom.Configuration(ctx.Configuration);

            logConfig
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", ServiceInfo.ServiceName)
                .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Machine", Environment.MachineName)
                .Enrich.WithSpan(new SpanOptions { IncludeOperationName = true })
                .Enrich.With<EventTypeEnricher>();

            logConfig.WriteTo.Console();

            if (ctx.HostingEnvironment.IsDevelopment())
            {
                logConfig.UseSeq(ctx.Configuration);
            }
            else
            {
                logConfig.UseApplicationInsights(services);
            }
        });

        return builder;
    }

    public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (context, httpContext) =>
            {
                context.Set("Host", httpContext.Request.Host.Value);
                context.Set("Scheme", httpContext.Request.Scheme);
                context.Set("ClientIP", httpContext.GetIpAddress());
                context.Set("UserAgent", httpContext.GetUserAgent());

                if (httpContext.Request.QueryString.HasValue)
                {
                    context.Set("QueryString", httpContext.Request.QueryString.Value);
                }

                context.Set("ContentType", httpContext.Response.ContentType);
            };
        });

        app.Use((context, next) =>
        {
            var diagnosticContext = context.RequestServices.GetService<IDiagnosticContext>();
            diagnosticContext?.Set("RequestStart", DateTime.UtcNow.ToString("O"));

            return next(context);
        });

        return app;
    }

    private static LoggerConfiguration UseApplicationInsights(this LoggerConfiguration logConfig, IServiceProvider services)
    {
        var telemetryConfig = services.GetRequiredService<TelemetryConfiguration>();
        logConfig.WriteTo.ApplicationInsights(telemetryConfig, TelemetryConverter.Traces);

        return logConfig;
    }

    private static LoggerConfiguration UseSeq(this LoggerConfiguration logConfig, IConfiguration configuration)
    {
        var seqUrl = configuration["SEQ_URL"];

        if (!string.IsNullOrEmpty(seqUrl))
        {
            logConfig.WriteTo.Seq(seqUrl);
        }

        return logConfig;
    }
}
