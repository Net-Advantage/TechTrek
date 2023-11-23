namespace Nabs.TechTrek.Persistence;

public class TechTrekDbContext : DbContext
{
    private readonly IApplicationContext _applicationContext;

    public TechTrekDbContext(
        DbContextOptions options,
        IApplicationContext applicationContext) : base(options)
    {
        _applicationContext = applicationContext;
    }

    public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();
    public DbSet<WeatherForecastCommentEntity> WeatherForecastComments => Set<WeatherForecastCommentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherForecastCommentEntity>()
            .HasQueryFilter(a => 
                !_applicationContext.TenantContext.WithTenantFilter
                || (_applicationContext.TenantContext.WithTenantFilter &&
                _applicationContext.TenantContext.TenantId == a.TenantId));

    }
}
