using TechTrek.Tenant.Persistence.Entities;

namespace TechTrek.Tenant.Persistence.Tests;

public class EntitiesUnitTests
{
    [Fact]
    public void TenantAnemicTest()
    {
        // Arrange
        var tenant = new TenantEntity();

        // Act


        // Assert
        tenant.Should().NotBeNull();
    }
}
