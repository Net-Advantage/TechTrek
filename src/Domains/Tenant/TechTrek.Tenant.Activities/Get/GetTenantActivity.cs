namespace TechTrek.Tenant.Activities.Get;

public sealed class GetTenantActivity
    : Activity<GetTenantState>
{
    public GetTenantActivity(TenantEntity tenantEntity)
    {
        State.Entity = tenantEntity;
        State.Dto = tenantEntity.ToDto();

        AddValidator<TenantValidation>(s => s.Dto!);
    }
}
