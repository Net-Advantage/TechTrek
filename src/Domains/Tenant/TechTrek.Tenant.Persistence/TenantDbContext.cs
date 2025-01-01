using Nabs.Application;

namespace TechTrek.Tenant.Persistence;

public sealed class TenantDbContext(
    DbContextOptions<TenantDbContext> dbContextOptions,
    IRequestContext requestContext) 
    : BaseDbContext(dbContextOptions, requestContext)
{
    public DbSet<TenantEntity> TenantEntities => Set<TenantEntity>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantDbContext).Assembly);
    }
}
