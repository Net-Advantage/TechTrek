namespace Nabs.TechTrek.Persistence;

public abstract class TenantableDbContext : DbContext, ITenantableDbContext
{
    protected TenantableDbContext(
               DbContextOptions options,
               IApplicationContext applicationContext)
        : base(options)
    {
        ApplicationContext = applicationContext;

        TenantId = applicationContext.TenantContext.TenantId;
    }

    protected IApplicationContext ApplicationContext { get; }

    public Guid TenantId { get; }

    public DbSet<TenantEntity> Tenants => Set<TenantEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddTenantEntityQueryFilter(this);
            }

            if(typeof(TenantEntity) == entityType.ClrType)
            {
                Expression<Func<TenantEntity, bool>> filter = entity =>
                    EF.Property<Guid>(entity, "Id") == TenantId;

                entityType.SetQueryFilter(filter);
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
        if(entries.Any())
        {
            if (ApplicationContext.TenantContext.TenantId == Guid.Empty)
            {
                throw new InvalidOperationException("TenantId is not set.");
            }
        }

        foreach (var entry in entries)
        {
            entry.Property(nameof(TenantId)).CurrentValue = ApplicationContext.TenantContext.TenantId;
        }
    }
}


public class TechTrekDbContext(
    DbContextOptions<TechTrekDbContext> options,
    IApplicationContext applicationContext) 
    : TenantableDbContext(options, applicationContext)
{
    public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();
    public DbSet<WeatherForecastCommentEntity> WeatherForecastComments => Set<WeatherForecastCommentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Must call the base
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechTrekDbContext).Assembly);
    }
}
