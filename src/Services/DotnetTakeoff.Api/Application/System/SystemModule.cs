namespace DotnetTakeoff.Api.Application.System;

internal static class SystemModule
{
    public static IEndpointRouteBuilder MapSystemRoutes(this IEndpointRouteBuilder routes)
    {
        routes.MapGetVersion();

        return routes;
    }
}
