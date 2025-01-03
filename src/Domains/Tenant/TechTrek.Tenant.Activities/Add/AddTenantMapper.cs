namespace TechTrek.Tenant.Activities.Add;

internal class AddTenantMapper : IMapper<AddTenantState>
{
    public void Map(AddTenantState state)
    {
        var dto = state.In!;
        state.Entity = new TenantEntity
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
    }
}
