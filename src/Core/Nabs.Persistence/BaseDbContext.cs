namespace Nabs.Persistence;

public abstract class BaseDbContext(
    DbContextOptions options,
    IApplicationContext applicationContext) 
    : DbContext(options)
{
    public IApplicationContext ApplicationContext { get; } = applicationContext;
}