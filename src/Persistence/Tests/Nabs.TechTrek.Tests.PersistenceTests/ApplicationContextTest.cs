namespace Nabs.TechTrek.Tests.PersistenceTests;

public sealed class ApplicationContextTest : ScopedDependencyInversionTestBase
{
    public ApplicationContextTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData(TenantIsolationStrategy.SharedShared)]
    [InlineData(TenantIsolationStrategy.SharedDedicated)]
    public void TheScopedApplicationContext(TenantIsolationStrategy tenantIsolationStrategy)
    {
        var tenantId = Guid.NewGuid();
        ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = tenantIsolationStrategy,
            TenantContext = new TenantContext()
            {
                TenantId = tenantId
            }
        };

        var applicationContext = ServiceProvider.GetRequiredService<IApplicationContext>();
        Assert.NotNull(applicationContext);
        Assert.Equal(tenantId, applicationContext.TenantContext.TenantId);
        Assert.Equal(tenantIsolationStrategy, applicationContext.TenantIsolationStrategy);
    }
}
