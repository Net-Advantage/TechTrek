namespace Nabs.TechTrek.Persistence.Tenantable;

internal interface ITenantableDbContext
{
    Guid TenantId { get; }
}
