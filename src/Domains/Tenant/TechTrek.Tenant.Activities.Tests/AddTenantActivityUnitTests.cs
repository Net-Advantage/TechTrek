using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nabs.Tests;
using TechTrek.Tenant.Persistence;

namespace TechTrek.Tenant.Activities.Tests;


public sealed class AddTenantActivityUnitTests : BaseTest<PersistenceTestFixture>
{
    private readonly TenantActivityFactory _tenantActivityFactory;
    private readonly PersistenceTestFixture _testFixture;

    public AddTenantActivityUnitTests(PersistenceTestFixture testFixture)
    {
        _testFixture = testFixture;

        var dbContextFactory = _testFixture.ServiceProvider.GetRequiredService<IDbContextFactory<TenantDbContext>>();
        _tenantActivityFactory = new TenantActivityFactory(dbContextFactory);
    }

    [Fact]
    public void AddTenantActivityTest()
    {
        // Arrange


        // Act

        // Assert
        _tenantActivityFactory.Should().NotBeNull();
    }
}
