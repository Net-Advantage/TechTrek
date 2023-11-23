using System.Linq.Expressions;

namespace Nabs.TechTrek.Core.ApplicationContext.Abstractions;

public sealed class ApplicationContext : IApplicationContext
{
    public ApplicationContext()
    {
        TenantContext = new TenantContext();
        UserContext = new UserContext();
    }

    public ITenantContext TenantContext { get; init; }
    public IUserContext UserContext { get; init; }
}

public sealed class TenantContext : ITenantContext
{
    public Guid TenantId { get; init; }
    public bool WithTenantFilter { get; init; } = true;
}

public sealed class UserContext : IUserContext
{
    public Guid UserId { get; init; }
    public bool WithUserFilter { get; init; } = true;
}


public interface IApplicationContext
{
    ITenantContext TenantContext { get; init; }
    IUserContext UserContext { get; init; }
}

public interface ITenantContext
{
    Guid TenantId { get; init; }
    bool WithTenantFilter { get; init; }
}

public interface IUserContext
{
    Guid UserId { get; init; }
    bool WithUserFilter { get; init; }
}