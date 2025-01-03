namespace TechTrek.Tenant.Activities.EntityQuery;

public class EntityQueryUnitOfWork(
    IDbContextFactory<TenantDbContext> dbContextFactory)
    : IUnitOfWork<EntityQueryState>
{
    private readonly IDbContextFactory<TenantDbContext> _dbContextFactory = dbContextFactory;

    public async Task RunAsync(EntityQueryState activityState)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        activityState.Entity = await dbContext.TenantEntities
            .FindAsync(activityState.TenantId);
    }
}