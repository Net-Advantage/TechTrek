using Microsoft.Extensions.DependencyInjection;
using Nabs.Tests;
using Nabs.Tests.DatabaseTests;
using Xunit.Abstractions;

namespace RetailSample.ScenarioUnitTests;

public sealed class NewUserWorkflowTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.AddUserUserManagementWorkflows();
		services.AddRetailSamplePersistence(ConfigurationRoot);
	}
}

public sealed class NewUserWorkflowUnitTests(
	ITestOutputHelper testOutputHelper,
	NewUserWorkflowTestFixture testFixture)
	: DatabaseTestBase<NewUserWorkflowTestFixture>(testOutputHelper, testFixture)
{
	private NewUserWorkflow _workflow = default!;

	protected override Task StartTest()
	{
		var workflowParameters = new NewUserWorkflowParameters
		{
			UserId = new Guid("f8df990e-48c4-426c-ae29-832b1dbc33d1")
		};

		_workflow = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<NewUserWorkflow>();

		NewUserWorkflowRepository workflowRepository = default!;
		_workflow = new NewUserWorkflow(workflowParameters, workflowRepository);

		return Task.CompletedTask;
	}

	[Fact]
	public async Task Run_ReturnsIsValidFalse()
	{
		// Arrange
		var activity = _workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();
		
		// Assert Arranged
		var changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().BeEmpty();

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.Should().BeNull();

		activity.ActivityState.Should().BeNull();
		activity.ValidationResult.Should().BeNull();
		activity.HasStateChanged.Should().BeFalse();
		activity.InitialActivityState.Should().BeNull();

		// Act
		await _workflow.RunAsync();

		// Assert
		changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().HaveCount(1);

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.IsValid.Should().BeFalse();

		activity.ActivityState.Should().NotBeNull();
		activity.HasStateChanged.Should().BeTrue();
		activity.ValidationResult.IsValid.Should().BeFalse();
	}

	[Fact]
	public async Task RunUpdate_ReturnsIsValidTrue()
	{
		// Arrange
		var activity = _workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();

		// Assert Arranged
		var changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().BeEmpty();

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.Should().BeNull();

		activity.ActivityState.Should().BeNull();
		activity.ValidationResult.Should().BeNull();
		activity.HasStateChanged.Should().BeFalse();
		activity.InitialActivityState.Should().BeNull();

		// Act
		await _workflow.RunAsync();

		activity.ActivityState = activity.ActivityState with
		{
			Username = "joe@joesengineering.com",
			FirstName = "Joe",
			LastName = "Soap"
		};

		await activity.RunAsync();

		// Assert
		changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().HaveCount(1);

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.IsValid.Should().BeTrue();

		activity.ActivityState.Should().NotBeNull();
		activity.HasStateChanged.Should().BeTrue();
		activity.ValidationResult.IsValid.Should().BeTrue();
	}

	[Fact]
	public async Task RunUpdateAfterStateChange_ReturnsIsValidTrue()
	{
		// Arrange
		var activity = _workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();

		// Assert Arranged
		var changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().BeEmpty();

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.Should().BeNull();

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
		await _workflow.RunAsync();

		// Assert
		changedActivityStates = _workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().HaveCount(1);

		_workflow.Activities.Should().HaveCount(1);
		_workflow.WorkflowState.Should().NotBeNull();
		_workflow.ValidationResult.IsValid.Should().BeTrue();

		activity.ActivityState.Should().NotBeNull();
		activity.HasStateChanged.Should().BeTrue();
		activity.ValidationResult.IsValid.Should().BeTrue();
	}
}
