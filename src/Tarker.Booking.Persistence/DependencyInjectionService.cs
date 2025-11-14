using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tarker.Booking.Application.DataBase;
using Tarker.Booking.Persistence.DataBase;

namespace Tarker.Booking.Persistence
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<DataBaseService>(opt =>
            {
                opt.UseSqlServer(configuration["SQLConnectionString"]);
            });

            services.AddScoped<IDataBaseService, DataBaseService>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
            {
                string tenantId = Environment.GetEnvironmentVariable("tenantId") ?? string.Empty;
                string clientId = Environment.GetEnvironmentVariable("clientId") ?? string.Empty;
                string clientSecret = Environment.GetEnvironmentVariable("clientSecret") ?? string.Empty;

                var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                var azureKeyVoultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(tokenCredential);

                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(new Dictionary<string,
                    SqlColumnEncryptionKeyStoreProvider>(1, StringComparer.OrdinalIgnoreCase)
                {
                    { SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVoultProvider }
                });
            }
            else
            {
                var azureKeyVoultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(
                        new ManagedIdentityCredential()
                    );

                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(new Dictionary<string,
                    SqlColumnEncryptionKeyStoreProvider>(1, StringComparer.OrdinalIgnoreCase)
                {
                    { SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVoultProvider }
                });
            }

            return services;
        }
    }
}
