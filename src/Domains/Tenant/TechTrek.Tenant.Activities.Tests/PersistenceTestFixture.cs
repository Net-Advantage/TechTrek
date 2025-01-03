using Microsoft.Extensions.Configuration;
using Nabs.Application;

namespace TechTrek.Tenant.Activities.Tests;

public sealed class PersistenceTestFixture : BaseTestFixture
{
    protected override void ConfigureServices(
        IServiceCollection services,
        IConfigurationRoot configurationRoot)
    {
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddTenantPersistence(configurationRoot);
    }
}
