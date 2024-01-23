using RetailSample.Activities.RegistrationScenario;

namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflow : Workflow<NewUserWorkflowState>
{
	private readonly NewUserWorkflowParameters _workflowParameters;
	private readonly NewUserWorkflowRepository _workflowRepository;

	public NewUserWorkflow(
		NewUserWorkflowParameters workflowParameters,
		NewUserWorkflowRepository workflowRepository)
	{
		_workflowParameters = workflowParameters;
		_workflowRepository = workflowRepository;

		AddActivity<RegistrationActivity>(RegistrationActivityPostProcessor);
	}

	protected override async Task OnDataLoadAsync()
	{
		WorkflowState = await _workflowRepository.Load(_workflowParameters);
	}

	protected override async Task OnDataPersistAsync()
	{
		if (WorkflowState == null)
		{
			return;
		}

		await _workflowRepository.Persist(WorkflowState);
	}

	private void RegistrationActivityPostProcessor(RegistrationActivity activity)
	{
		
	}
}


