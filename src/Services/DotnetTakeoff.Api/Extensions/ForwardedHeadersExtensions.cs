using Microsoft.AspNetCore.HttpOverrides;

namespace DotnetTakeoff.Api.Extensions;

internal static class ForwardedHeadersExtensions
{
    public static WebApplicationBuilder AddForwardedHeaders(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return builder;
    }
}
