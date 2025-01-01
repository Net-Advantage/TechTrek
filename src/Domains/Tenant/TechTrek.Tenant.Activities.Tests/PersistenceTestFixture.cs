using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabs.Application;
using Nabs.Tests;
using TechTrek.Tenant.Persistence;

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
