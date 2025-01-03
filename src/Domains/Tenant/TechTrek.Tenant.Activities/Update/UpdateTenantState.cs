namespace TechTrek.Tenant.Activities.Update;

public sealed class UpdateTenantState
        : IActivityState
{
    public Dtos.Tenant In { get; set; } = default!;
    public Dtos.Tenant? Out { get; set; }
    internal TenantEntity? Entity { get; set; }
}