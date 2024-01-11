using RetailSample.Activities.RegistrationScenario.ActivityStates;

namespace RetailSample.Activities.RegistrationScenario.ActivityFeatures;

public sealed class RegistrationStateFactory
    : ActivityStateFactory<RegistrationActivityState>
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
