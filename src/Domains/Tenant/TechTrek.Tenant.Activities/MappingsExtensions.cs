namespace TechTrek.Tenant.Activities;

public static class MappingsExtensions
{
    public static Dtos.Tenant ToDto(this TenantEntity entity)
    {
        return new Dtos.Tenant
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static TenantEntity ToEntity(this AddTenant dto)
    {
        return new TenantEntity
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
    }

    public static TenantEntity ToEntity(this Dtos.Tenant dto)
    {
        return new TenantEntity
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}
