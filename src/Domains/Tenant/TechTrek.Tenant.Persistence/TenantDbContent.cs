using Nabs.Application;

namespace TechTrek.Tenant.Persistence;

public sealed class TenantDbContent(
    DbContextOptions<TenantDbContent> dbContextOptions,
    IApplicationContext applicationContext) 
    : BaseDbContext(dbContextOptions, applicationContext)
{
    public DbSet<TenantEntity> TenantEntities => Set<TenantEntity>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantDbContent).Assembly);
    }
}
