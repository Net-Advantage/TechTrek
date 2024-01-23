namespace Nabs.Core.Application.Abstractions;

public sealed class ApplicationContext : IApplicationContext
{
    public ApplicationContext()
    {
        TenantContext = new TenantContext();
        UserContext = new UserContext();
    }

    public TenantIsolationStrategy TenantIsolationStrategy { get; init; }
    public ITenantContext TenantContext { get; init; }
    public IUserContext UserContext { get; init; }

    public bool IsTenant(Guid tenantId)
    {
        return TenantContext.TenantId == tenantId;
    }
}

public sealed class TenantContext : ITenantContext
{
    public Guid TenantId { get; init; }
}

public sealed class UserContext : IUserContext
{
    public Guid UserId { get; init; }
}


public interface IApplicationContext
{
    TenantIsolationStrategy TenantIsolationStrategy { get; init; }

    ITenantContext TenantContext { get; init; }
    IUserContext UserContext { get; init; }

    public bool IsTenant(Guid tenantId);
}

public interface ITenantContext
{
    Guid TenantId { get; init; }
}

public interface IUserContext
{
    Guid UserId { get; init; }
}