using DotnetTakeoff.Api.Infrastructure;

namespace DotnetTakeoff.Api.Application.System;

internal static class GetVersion
{
    public static IEndpointRouteBuilder MapGetVersion(this IEndpointRouteBuilder routes)
    {
        var version = new
        {
            ServiceInfo.ServiceName,
            ServiceInfo.ServiceVersion
        };

        routes.MapGet("/version", () => Results.Ok(version))
            .ExcludeFromDescription();

        return routes;
    }
}
