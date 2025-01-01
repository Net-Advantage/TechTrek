namespace TechTrek.Tenant.Activities;

public class RequestContext : IRequestContext
{
    public Guid TenantId { get; set; } = Guid.Empty;
    public ClaimsPrincipal? Principal { get; set; }
    public TenantIsolationStrategy TenantIsolationStrategy { get; set; }
}
