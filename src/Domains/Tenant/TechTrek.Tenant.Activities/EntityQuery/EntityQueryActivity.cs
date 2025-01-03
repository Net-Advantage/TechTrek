
namespace TechTrek.Tenant.Activities.EntityQuery;

public sealed class EntityQueryActivity
    : Activity<EntityQueryState>
{
    public EntityQueryActivity(Guid tenantId)
    {
        State.TenantId = tenantId;

        AddUnitOfWork<EntityQueryUnitOfWork>();
    }
}
