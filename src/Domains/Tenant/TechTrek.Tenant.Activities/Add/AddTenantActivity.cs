namespace TechTrek.Tenant.Activities.Add;

public sealed class AddTenantActivity
    : Activity<AddTenantState>
{
    public AddTenantActivity(AddTenant addTenant)
    {
        State.In = addTenant;

        AddValidator<AddTenantValidation>(s => s.In!);
        AddMapper<AddTenantMapper>();
        AddAction(Finalise);
    }

    public void Finalise()
    {
        State.Out = State.Entity!.ToDto();
    }
}
