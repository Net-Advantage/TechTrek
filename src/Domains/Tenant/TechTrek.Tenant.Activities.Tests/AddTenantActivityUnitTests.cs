namespace TechTrek.Tenant.Activities.Tests;

public sealed class AddTenantActivityUnitTests
{
    [Fact]
    public async Task AddTenantActivityTest()
    {
        // Arrange
        var requestDto = new Dtos.AddTenant
        {
            Name = "Test Tenant"
        };
        var addTenantActivity = new AddTenantActivity(requestDto);

        // Act
        var result = await addTenantActivity.ExecuteAsync();

        // Assert
        result.Value.Out.Should().NotBeNull();
        result.Value.Out!.Name.Should().Be("Test Tenant");
    }
}
