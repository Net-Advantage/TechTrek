using RetailSample.Workflows.UserManagementScenarios;

namespace RetailSample.ScenarioUnitTests;

public sealed class NewUserWorkflowUnitTests
{
    [Fact]
    public async Task RunTest()
    {
        // Arrange
        var workflow = new NewUserWorkflow();

        // Act
        await workflow.RunAsync();

        // Assert
        var activity = workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();
        activity.ActivityState.Should().NotBeNull();
        activity.HasStateChanged.Should().BeTrue();
        activity.ValidationResult.IsValid.Should().BeTrue();

        workflow.ChangedActivityStates.Should().HaveCount(1);
    }
}
