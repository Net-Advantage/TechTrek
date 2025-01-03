
namespace TechTrek.Tenant.Activities.Update;

public sealed class UpdateTenantActivity
    : Activity<UpdateTenantState>
{
    public UpdateTenantActivity(Dtos.Tenant tenant)
    {
        State.In = tenant;

        AddValidator<UpdateTenantValidation>(s => s.In);
        AddMapper<UpdateTenantMapper>();
        AddValidator<UpdateTenantEntityValidation>(s => s.Entity!);
        AddAction(Finalise);
    }

    public void Finalise()
    {
        var dto = State.Entity!.ToDto();
        State.Out = dto;
    }
}
