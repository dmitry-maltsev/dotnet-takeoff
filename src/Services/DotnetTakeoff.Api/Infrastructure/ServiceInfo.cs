using System.Reflection;

namespace DotnetTakeoff.Api.Infrastructure;

internal static class ServiceInfo
{
    public const string ServiceName = "DotnetTakeoff.API";

    public const string ServiceDescription = "Dotnet Takeoff API";

    public static string ServiceVersion => Assembly
        .GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
        !.InformationalVersion;
}
