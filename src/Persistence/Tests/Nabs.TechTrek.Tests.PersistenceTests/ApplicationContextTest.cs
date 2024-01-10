namespace Nabs.TechTrek.Tests.PersistenceTests;

public sealed class ApplicationContextTest(
    ITestOutputHelper testOutputHelper, 
    DatabaseFixture fixture) 
    : DatabaseTestBase(testOutputHelper, fixture)
{
    [Theory]
    [InlineData(TenantIsolationStrategy.SharedShared)]
    [InlineData(TenantIsolationStrategy.SharedDedicated)]
    public void TheScopedApplicationContext(TenantIsolationStrategy tenantIsolationStrategy)
    {
        var tenantId = Guid.NewGuid();
        Fixture.ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = tenantIsolationStrategy,
            TenantContext = new TenantContext()
            {
                TenantId = tenantId
            }
        };

        var applicationContext = Fixture.ServiceScope.ServiceProvider.GetRequiredService<IApplicationContext>();
        Assert.NotNull(applicationContext);
        Assert.Equal(tenantId, applicationContext.TenantContext.TenantId);
        Assert.Equal(tenantIsolationStrategy, applicationContext.TenantIsolationStrategy);
    }
}
