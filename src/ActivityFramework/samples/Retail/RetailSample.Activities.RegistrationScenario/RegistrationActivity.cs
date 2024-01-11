namespace RetailSample.Activities.RegistrationScenario;

public class RegistrationActivity : Activity<RegistrationActivityState>
{
    public RegistrationActivity()
    {
        FeatureBuilder
            .AddFactory<RegistrationStateFactory>()
            .AddCalculator<RegistrationStateCalculator>()
            .AddValidator<RegistrationStateValidator>();
    }
}
