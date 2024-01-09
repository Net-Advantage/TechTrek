using Nabs.TechTrek.Core.ApplicationContext.Abstractions;

namespace Nabs.TechTrek.PersistenceCli;

public static class Helpers
{
    public static Func<IServiceProvider, IApplicationContext> CreateApplicationContextFactory(TenantIsolationStrategy isolation, Guid tenantId)
    {
        return (sp) =>
        {
            return new ApplicationContext()
            {
                TenantContext = new TenantContext()
                {
                    TenantId = tenantId
                },
                TenantIsolationStrategy = isolation
            };
        };
    }
}