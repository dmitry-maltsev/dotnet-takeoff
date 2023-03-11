using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace DotnetTakeoff.Api.Extensions;

internal static class ConfigurationExtensions
{
    public static WebApplicationBuilder AddSecrets(this WebApplicationBuilder builder)
    {
        var keyVaultUrl = builder.Configuration["AzureKeyVaultUrl"];

        var secretClient = new SecretClient(
            new Uri(keyVaultUrl!),
            new DefaultAzureCredential());

        builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

        return builder;
    }
}
