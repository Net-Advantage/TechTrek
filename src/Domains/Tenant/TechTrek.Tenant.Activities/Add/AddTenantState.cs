namespace TechTrek.Tenant.Activities.Add;

public sealed class AddTenantState 
    : IActivityState
{
    public AddTenant? In { get; set; }
    public Dtos.Tenant? Out { get; set; }

    internal TenantEntity? Entity { get; set; }
}
