namespace RetailSample.ScenarioUnitTests;

public class RegistrationScenarioTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task RunTest(int index)
    {
        _ = index;
        // Arrange
        var activity = new RegistrationActivity();


        // Act
        await activity.RunAsync();

        // Assert
        activity.ValidationResult.IsValid.Should().BeTrue();
    }
}