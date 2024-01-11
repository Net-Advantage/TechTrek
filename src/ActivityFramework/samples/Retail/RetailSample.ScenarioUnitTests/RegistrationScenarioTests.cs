namespace RetailSample.ScenarioUnitTests;

public class RegistrationScenarioTests
{
    [Fact]
    public async Task RunTest()
    {
        // Arrange
        var activity = new RegistrationActivity();


        // Act
        await activity.RunAsync();

        // Assert
        activity.ActivityState.Should().NotBeNull();
        activity.ActivityState!.Id.Should().NotBeEmpty();
    }
}