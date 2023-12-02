namespace Nabs.TechTrek.Persistence;

public abstract class TechTrekDbContext : DbContext
{
    protected TechTrekDbContext(
        DbContextOptions options,
        IApplicationContext applicationContext)
        : base(options)
    {
        ApplicationContext = applicationContext;
    }

    protected IApplicationContext ApplicationContext { get; }

    public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();
    public DbSet<WeatherForecastCommentEntity> WeatherForecastComments => Set<WeatherForecastCommentEntity>();
}

public sealed class TechTrekDedicatedTenantDbContext(
    DbContextOptions<TechTrekDedicatedTenantDbContext> options,
    IApplicationContext applicationContext) 
    : TechTrekDbContext(options, applicationContext)
{
}

public sealed class TechTrekSharedTenantDbContext : TechTrekDbContext, ITenantableDbContext
{
    public TechTrekSharedTenantDbContext(
        DbContextOptions<TechTrekSharedTenantDbContext> options,
        IApplicationContext applicationContext)
        : base(options, applicationContext)
    {
        if(ApplicationContext.TenantContext.TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("TenantId is not set.");
        }

        if(ApplicationContext.TenantIsolationStrategy is not TenantIsolationStrategy.SharedShared)
        {
            throw new InvalidOperationException("TenantIsolationStrategy is not SharedShared.");
        }

        TenantId = applicationContext.TenantContext.TenantId;
    }

    public Guid TenantId { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddTenantEntityQueryFilter(this);
            }
        }
    }

    public override int SaveChanges()
    {
        SetTenantId();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTenantId();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetTenantId()
    {
        var entries = ChangeTracker.Entries()
                        .Where(e => e.Entity is ITenantEntity && 
                                    e.State == EntityState.Added || 
                                    e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Property(nameof(TenantId)).CurrentValue = ApplicationContext.TenantContext.TenantId;
        }
    }
}