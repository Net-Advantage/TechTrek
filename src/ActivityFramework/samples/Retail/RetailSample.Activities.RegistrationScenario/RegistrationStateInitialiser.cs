namespace RetailSample.Activities.RegistrationScenario;

public sealed class RegistrationStateInitialiser()
    : ActivityStateInitialiser<RegistrationActivityState>()
{
    public override Task<RegistrationActivityState> RunAsync()
    {
        var result = new RegistrationActivityState()
        {
            Id = Guid.NewGuid()
        };

        return Task.FromResult(result);
    }
}
