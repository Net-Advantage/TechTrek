namespace Nabs.TechTrek.Tests.PersistenceTests;

public sealed class ApplicationContextTest : ScopedDependencyInversionTestBase
{
    public ApplicationContextTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TheScopedApplicationContext(bool withTenantFilter)
    {
        var tenantId = Guid.NewGuid();
        ApplicationContextFactory = () => new ApplicationContext()
            {
                TenantContext = new TenantContext()
                {
                    TenantId = tenantId,
                    WithTenantFilter = withTenantFilter
                }
            };

        var applicationContext = ServiceProvider.GetRequiredService<IApplicationContext>();
        Assert.NotNull(applicationContext);
        Assert.Equal(tenantId, applicationContext.TenantContext.TenantId);
        Assert.Equal(withTenantFilter, applicationContext.TenantContext.WithTenantFilter);
    }
}
