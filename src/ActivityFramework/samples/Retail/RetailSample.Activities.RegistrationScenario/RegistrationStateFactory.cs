namespace RetailSample.Activities.RegistrationScenario;

public sealed class RegistrationStateFactory()
    : ActivityStateFactory<RegistrationActivityState>()
{
    public override Task RunAsync()
    {
        ActivityState = new RegistrationActivityState()
        {
            Id = Guid.NewGuid()
        };

        return Task.CompletedTask;
    }
}
