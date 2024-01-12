
namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationActivity 
	: Activity<RegistrationActivityState, RegistrationStateInitialiser, RegistrationStateValidator>
{
	public RegistrationActivity()
	{
		AddBehaviour(new RegistrationStateTransformer(), ThenDoThis);
	}

	private void ThenDoThis()
	{
		Console.WriteLine($"Then do this: {ActivityState.ProcessedOn}");
	}
}
