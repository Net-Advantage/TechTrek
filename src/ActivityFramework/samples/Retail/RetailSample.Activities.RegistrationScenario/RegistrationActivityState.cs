namespace RetailSample.Activities.RegistrationScenario;

public sealed record RegistrationActivityState : ActivityState
{
    public DateTime ProcessedOn { get; set; }
}
