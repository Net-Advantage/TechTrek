
namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationStateTransformer() 
    : ActivityStateTransformer<RegistrationActivityState>()
{
    
    public override Task<RegistrationActivityState> RunAsync(RegistrationActivityState activityState)
    {
        var result = activityState with
        { 
            ProcessedOn = DateTime.UtcNow
        };

        return Task.FromResult(result);
    }
}
