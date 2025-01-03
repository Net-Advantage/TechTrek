namespace TechTrek.Tenant.Activities.EntityPersist;

public sealed class EntityPersistUnityOfWork(
    IDbContextFactory<TenantDbContext> dbContextFactory)
    : IUnitOfWork<EntityPersistState>
{
    private readonly IDbContextFactory<TenantDbContext> _dbContextFactory = dbContextFactory;

    public async Task RunAsync(EntityPersistState activityState)
    {
        if (activityState.Entity is null)
        {
            throw new ArgumentNullException(nameof(activityState.Entity));
        }

        await using var dbContext = _dbContextFactory.CreateDbContext();

        if(activityState.Entity.Id == Guid.Empty)
        {
            dbContext.TenantEntities.Add(activityState.Entity);
        }
        else
        {
            dbContext.TenantEntities.Update(activityState.Entity);
        }

        await dbContext.SaveChangesAsync();
    }
}