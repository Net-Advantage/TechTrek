namespace RetailSample.Activities.RegistrationScenario;

public sealed record RegistrationActivityState : ActivityState
{
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime ProcessedOn { get; set; }
}
