namespace RetailSample.ScenarioUnitTests;

public sealed class NewUserWorkflowUnitTests
{
    [Fact]
    public async Task Run_ReturnsIsValidFalse()
    {
        // Arrange
        var workflow = new NewUserWorkflow();
        var activity = workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();
        
        // Assert Arranged
        workflow.ChangedActivityStates.Should().BeEmpty();
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.Should().BeNull();

        activity.ActivityState.Should().BeNull();
        activity.ValidationResult.Should().BeNull();
        activity.HasStateChanged.Should().BeFalse();
        activity.InitialActivityState.Should().BeNull();

        // Act
        await workflow.RunAsync();

        // Assert
        workflow.ChangedActivityStates.Should().HaveCount(1);
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.IsValid.Should().BeFalse();

        activity.ActivityState.Should().NotBeNull();
        activity.HasStateChanged.Should().BeTrue();
        activity.ValidationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task RunUpdate_ReturnsIsValidTrue()
    {
        // Arrange
        var workflow = new NewUserWorkflow();
        var activity = workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();

        // Assert Arranged
        workflow.ChangedActivityStates.Should().BeEmpty();
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.Should().BeNull();

        activity.ActivityState.Should().BeNull();
        activity.ValidationResult.Should().BeNull();
        activity.HasStateChanged.Should().BeFalse();
        activity.InitialActivityState.Should().BeNull();

        // Act
        await workflow.RunAsync();

        activity.ActivityState = activity.ActivityState with
        {
            Username = "joe@joesengineering.com",
            FirstName = "Joe",
            LastName = "Soap"
        };

        await activity.RunAsync();

        // Assert
        workflow.ChangedActivityStates.Should().HaveCount(1);
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.IsValid.Should().BeTrue();

        activity.ActivityState.Should().NotBeNull();
        activity.HasStateChanged.Should().BeTrue();
        activity.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task RunUpdateAfterStateChange_ReturnsIsValidTrue()
    {
        // Arrange
        var workflow = new NewUserWorkflow();
        var activity = workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();

        // Assert Arranged
        workflow.ChangedActivityStates.Should().BeEmpty();
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.Should().BeNull();

        activity.ActivityState.Should().BeNull();
        activity.ValidationResult.Should().BeNull();
        activity.HasStateChanged.Should().BeFalse();
        activity.InitialActivityState.Should().BeNull();

        // Act
        var registrationActivityState = new RegistrationActivityState()
        {
            Id = new Guid("f8df990e-48c4-426c-ae29-832b1dbc33d1"),
            Username = "joe@joesengineering.com",
            FirstName = "Joe",
            LastName = "Soap"
        };
        activity.InitialiseState(registrationActivityState);
        await workflow.RunAsync();

        // Assert
        workflow.ChangedActivityStates.Should().HaveCount(1);
        workflow.Activities.Should().HaveCount(1);
        workflow.WorkflowState.Should().NotBeNull();
        workflow.ValidationResult.IsValid.Should().BeTrue();

        activity.ActivityState.Should().NotBeNull();
        activity.HasStateChanged.Should().BeTrue();
        activity.ValidationResult.IsValid.Should().BeTrue();
    }
}
