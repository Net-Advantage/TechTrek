namespace TechTrek.Tenant.Activities;

public sealed class GetTenantActivity(
    IDbContextFactory<TenantDbContext> dbContextFactory)
{
    private readonly IDbContextFactory<TenantDbContext> _dbContextFactory = dbContextFactory;

    public async Task<Dtos.Tenant> ExecuteAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();
        var entity = await dbContext.TenantEntities.FindAsync(tenantId, cancellationToken);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Tenant with id {tenantId} not found.");
        }

        return entity.ToDto();
    }
}
