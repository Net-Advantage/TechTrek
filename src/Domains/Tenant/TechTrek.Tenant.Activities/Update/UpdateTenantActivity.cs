
namespace TechTrek.Tenant.Activities.Update;

public sealed class UpdateTenantActivity
    : Activity<UpdateTenantState>
{
    public UpdateTenantActivity(Dtos.Tenant tenant)
    {
        State.Request = new Request<Dtos.Tenant>(tenant);

        AddValidator<UpdateTenantValidation>(s => s.Request!);
        AddStep(MapEntity);
        AddValidator<UpdateTenantEntityValidation>(s => s.Entity!);
        AddStep(Finalise);
    }

    private void MapEntity()
    {
        State.Entity = State.Request!.RequestDto.ToEntity();
    }

    public void Finalise()
    {
        var dto = State.Entity!.ToDto();
        State.Response = new Response<Dtos.Tenant>(dto);
    }
}
