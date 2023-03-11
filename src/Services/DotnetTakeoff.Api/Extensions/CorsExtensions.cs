namespace DotnetTakeoff.Api.Extensions;

internal static class CorsExtensions
{
    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        var origins = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(',');

        builder.Services.AddCors(setup =>
        {
            setup.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod();

                if (origins is { Length: > 0 } && origins[0] != "*")
                {
                    policy.WithOrigins(origins);
                }
                else
                {
                    policy.AllowAnyOrigin();
                }
            });
        });

        return builder;
    }
}
