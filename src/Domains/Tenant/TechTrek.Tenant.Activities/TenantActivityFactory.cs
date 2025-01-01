namespace TechTrek.Tenant.Activities;

public sealed class TenantActivityFactory(
    IDbContextFactory<TenantDbContext> dbContextFactory)
{
    private readonly IDbContextFactory<TenantDbContext> _dbContextFactory = dbContextFactory;

    public GetTenantActivity GetActivity()
    {
        return new GetTenantActivity(_dbContextFactory);
    }
}
