using RetailSample.Activities.RegistrationScenario.ActivityFeatures;
using RetailSample.Activities.RegistrationScenario.ActivityStates;

namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationActivity : Activity<RegistrationActivityState>
{
    public RegistrationActivity()
    {
        AddFactory<RegistrationStateFactory>();
    }


    public override async Task RunAsync()
    {
        if(Factory is not null)
        {
            await Factory.RunAsync();
        }
    }
}
