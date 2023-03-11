using DotnetTakeoff.Api.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotnetTakeoff.Api.Extensions;

internal static class OpenApiExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = ServiceInfo.ServiceDescription,
            Version = "v1"
        }));
        builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

        return builder;
    }

    public static WebApplication UseSwaggerExplorer(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/swagger/v1/swagger.json", ServiceInfo.ServiceDescription);
            o.RoutePrefix = string.Empty;
        });

        return app;
    }
}
