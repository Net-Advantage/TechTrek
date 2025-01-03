using TechTrek.Tenant.Activities.Add;

namespace TechTrek.Tenant.Activities.Tests;

public sealed class AddTenantActivityUnitTests : BaseTest<PersistenceTestFixture>
{
    private readonly PersistenceTestFixture _testFixture;

    public AddTenantActivityUnitTests(PersistenceTestFixture testFixture)
    {
        _testFixture = testFixture;

        var dbContextFactory = _testFixture.ServiceProvider.GetRequiredService<IDbContextFactory<TenantDbContext>>();
    }

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
        await addTenantActivity.ExecuteAsync();

        // Assert
        addTenantActivity.State.Response.Should().NotBeNull();
        addTenantActivity.State.Response!.ResponseDto.Name.Should().Be("Test Tenant");
    }
}
