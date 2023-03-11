using Microsoft.Net.Http.Headers;

namespace DotnetTakeoff.Api.Extensions;

internal static class HttpContextExtensions
{
    public static string? GetIpAddress(this HttpContext httpContext)
    {
        var ipAddress = httpContext.Connection.RemoteIpAddress;
        if (ipAddress == null)
        {
            return null;
        }

        if (ipAddress.IsIPv4MappedToIPv6)
        {
            ipAddress = ipAddress.MapToIPv4();
        }

        return ipAddress.ToString();
    }

    public static string GetUserAgent(this HttpContext httpContext)
    {
        return httpContext.Request.Headers[HeaderNames.UserAgent].ToString();
    }
}
