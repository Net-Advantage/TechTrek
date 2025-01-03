namespace TechTrek.Tenant.Activities;

public static class MappingExtensions
{
    public static Dtos.Tenant ToDto(this TenantEntity entity)
    {
        return new Dtos.Tenant
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}
