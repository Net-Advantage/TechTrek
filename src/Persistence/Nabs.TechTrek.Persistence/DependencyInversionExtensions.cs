using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nabs.TechTrek.Persistence;

public static class DependencyInversionExtensions
{
    //TODO: DWS: This needs to be set up properly
    const string _testConnectionString = "Server=localhost,14331;Database={0};User Id=sa;Password=Password123;TrustServerCertificate=True;";
    
    //TODO: DWS: This assumes azure Sql we will need to provide various options for clouds other than Azure.
    const string _envConnectionString = "Server=tcp:{0};Database={1};Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContextFactory<TechTrekDbContext>((sp, options) =>
        {
            var applicationContext = sp.GetRequiredService<IApplicationContext>();
            var databaseName = $"TechTrekDb_";
            if (applicationContext.TenantIsolationStrategy == TenantIsolationStrategy.SharedShared)
            {
                databaseName += "SharedShared";
            }
            else
            {
                databaseName += $"{applicationContext.TenantIsolationStrategy}_{applicationContext.TenantContext.TenantId}";
            }

            var connectionString = string.Empty;
            var serverName = configuration.GetConnectionString("TechTrekDbServerName");
            if (string.IsNullOrWhiteSpace(serverName))
            {
                connectionString = string.Format(_testConnectionString, databaseName);
            }
            else
            {
                connectionString = string.Format(_envConnectionString, serverName, databaseName);
            }

            options.UseSqlServer(connectionString);

        });

        return services;
    }
}
