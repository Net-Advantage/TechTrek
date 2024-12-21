namespace TechTrek.Tenant.Activities.Tests;

public class AddTenantActivityUnitTests
{
    [Fact]
    public void AddTenantActivityTest()
    {
        // Arrange
        var addTenantActivity = new AddTenantActivity();

        // Act

        // Assert
        addTenantActivity.Should().NotBeNull();
    }
}
