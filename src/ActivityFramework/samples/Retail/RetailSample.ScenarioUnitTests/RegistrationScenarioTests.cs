namespace RetailSample.ScenarioUnitTests;

public class RegistrationScenarioTests
{
    [Fact]
    public async Task Run_Test()
    {
        // Arrange
        var activity = new RegistrationActivity();

        // Act
        activity.ActivityState.Should().BeNull();
        activity.ValidationResult.Should().BeNull();
        activity.HasStateChanged.Should().BeFalse();
        activity.InitialActivityState.Should().BeNull();

        await activity.RunAsync();

        // Asserts before update
        activity.ActivityState.Should().NotBeNull();
        activity.ValidationResult.IsValid.Should().BeFalse();
        activity.HasStateChanged.Should().BeTrue();
        activity.InitialActivityState.Should().NotBeNull();


        activity.ActivityState = activity.ActivityState with
        {
            Username = "joe@joesengineering.com",
            FirstName = "Joe",
            LastName = "Soap"
        };

        await activity.RunAsync();

        // Assert
        activity.ActivityState.Should().NotBeNull();
        activity.ValidationResult.IsValid.Should().BeTrue();
        activity.HasStateChanged.Should().BeTrue();
        activity.InitialActivityState.Should().NotBeNull();
    }
}