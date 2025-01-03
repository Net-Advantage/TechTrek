namespace TechTrek.Tenant.Activities.Add;

public sealed class AddTenantActivity
    : Activity<AddTenantState>
{
    public AddTenantActivity(AddTenant addTenant)
    {
        State.Request = new Request<AddTenant>(addTenant);

        AddValidator<AddTenantValidation>(s => s.Request!.RequestDto);
        AddStep(Finalise);
    }

    public void Finalise()
    {
        State.Entity = State.Request!.RequestDto.ToEntity();
        State.Response = new Response<Dtos.Tenant>(State.Entity!.ToDto());
    }
}
