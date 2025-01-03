namespace TechTrek.Tenant.Activities.Get;

public class GetTenantState : IActivityState
{
    public Dtos.Tenant? Out { get; set; }
    internal TenantEntity? Entity { get; set; }
    internal Dtos.Tenant? Dto { get; set; }
}
