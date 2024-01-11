
namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationStateCalculator : ActivityStateCalculator<RegistrationActivityState>
{
    public override Task RunAsync(RegistrationActivityState activityState)
    {

        ActivityState = activityState  with
        { 
            ProcessedOn = DateTime.UtcNow
        };

        return Task.CompletedTask;
    }
}
