using Microsoft.EntityFrameworkCore;

namespace Nabs.TechTrek.Persistence;

public abstract class TechTrekDbContextBase : DbContext
{
    protected TechTrekDbContextBase(DbContextOptions options) : base(options)
    {

    }
}
