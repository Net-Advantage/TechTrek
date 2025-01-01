using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabs.Application;

namespace TechTrek.Tenant.Persistence;

public static class DependencyInversionExtensions
{
    public static IServiceCollection AddTenantPersistence(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContextFactory<TenantDbContext>((sp, options) =>
        {
            var persistenceSection = configuration.GetRequiredSection("Persistence:TenantDb");
            var requestContext = sp.GetRequiredService<IRequestContext>();
            var databaseName = persistenceSection["DatabaseName"];
            if (requestContext.TenantIsolationStrategy == TenantIsolationStrategy.SharedShared)
            {
                databaseName += "SharedShared";
            }
            else
            {
                databaseName += $"{requestContext.TenantIsolationStrategy}_{requestContext.TenantId}";
            }

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = databaseName,
                TrustServerCertificate = true,
                ConnectTimeout = 30
            };

            var serverName = persistenceSection["ServerName"];
            if (!string.IsNullOrWhiteSpace(serverName))
            {
                sqlConnectionStringBuilder.DataSource = serverName;
            }

            var connectionString = sqlConnectionStringBuilder.ToString();
            options.UseSqlServer(connectionString);

        }, ServiceLifetime.Scoped);

        return services;
    }
}
