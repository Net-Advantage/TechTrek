namespace TechTrek.Tenant.Activities.Tests;

public sealed class TenantPersistenceUnitTests(
    PersistenceTestFixture testFixture) 
    : BaseTest<PersistenceTestFixture>
{
    private readonly PersistenceTestFixture _testFixture = testFixture;

    [Fact]
    public void TenantDbContextFactoryTest()
    {
        // Arrange
        var dbContextFactory = _testFixture.ServiceProvider
            .GetRequiredService<IDbContextFactory<TenantDbContext>>();

        // Act
        // Assert
        dbContextFactory.Should().NotBeNull();
    }

    [Fact]
    public void TenantDbContextTest()
    {
        // Arrange
        var dbContextFactory = _testFixture.ServiceProvider
            .GetRequiredService<IDbContextFactory<TenantDbContext>>();

        // Act
        var dbContext = dbContextFactory.CreateDbContext();

        // Assert
        dbContext.Should().NotBeNull();
        dbContext.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
    }
}
