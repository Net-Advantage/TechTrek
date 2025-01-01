namespace TechTrek.Tenant.Activities;

public class AddTenantActivity(
    IDbContextFactory<TenantDbContext> dbContextFactory)
{
    private readonly IDbContextFactory<TenantDbContext> _dbContextFactory = dbContextFactory;

    public async Task<Dtos.Tenant> ExecuteAsync(Dtos.Tenant tenant, CancellationToken cancellationToken)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();
        var entity = tenant.ToEntity();
        dbContext.TenantEntities.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }

}
