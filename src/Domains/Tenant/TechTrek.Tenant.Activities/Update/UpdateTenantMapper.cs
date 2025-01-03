namespace TechTrek.Tenant.Activities.Update;

internal sealed class UpdateTenantMapper : IMapper<UpdateTenantState>
{
    public void Map(UpdateTenantState state)
    {
        var dto = state.In;

        state.Entity = new TenantEntity
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}
