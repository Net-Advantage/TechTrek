namespace Nabs.TechTrek.TenantPersistence;

public sealed class TechTrekTenantDbContext(DbContextOptions<TechTrekTenantDbContext> options,
    IApplicationContext applicationContext)
    : BaseDbContext(options)
{
    private readonly IApplicationContext _applicationContext = applicationContext;

    public DbSet<TenantEntity> TenantEntities => Set<TenantEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Must call the base
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechTrekTenantDbContext).Assembly);
    }
}
