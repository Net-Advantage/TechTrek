namespace Nabs.Persistence;

public abstract class BaseDbContext(
    DbContextOptions options,
    IRequestContext requestContext) 
    : DbContext(options)
{
    public IRequestContext RequestContext { get; } = requestContext;
}