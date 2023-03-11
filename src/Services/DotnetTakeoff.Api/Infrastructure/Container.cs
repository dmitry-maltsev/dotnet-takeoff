using DotnetTakeoff.Api.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DotnetTakeoff.Api.Infrastructure;

internal static class Container
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext(builder.Configuration);

        return builder;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DotnetTakeoff");

        services.AddDbContext<DotnetTakeoffContext>(options =>
        {
            options
                .UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
    }
}
