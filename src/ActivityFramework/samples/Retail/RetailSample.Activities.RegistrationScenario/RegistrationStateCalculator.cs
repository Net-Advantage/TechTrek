
namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationStateCalculator(
    RegistrationActivityState activityState) 
    : ActivityStateCalculator<RegistrationActivityState>(activityState)
{
    public override Task RunAsync()
    {
        ActivityState = ActivityState with
        { 
            ProcessedOn = DateTime.UtcNow
        };

        return Task.CompletedTask;
    }
}
