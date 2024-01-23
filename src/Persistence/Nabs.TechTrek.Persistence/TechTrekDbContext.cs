namespace Nabs.TechTrek.Persistence;

public class TechTrekDbContext(
    DbContextOptions<TechTrekDbContext> options,
    IApplicationContext applicationContext) 
    : TenantableDbContext<TenantEntity>(options, applicationContext)
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
