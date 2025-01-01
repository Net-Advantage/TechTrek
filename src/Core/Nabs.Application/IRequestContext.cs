using System.Security.Claims;

namespace Nabs.Application;

public interface IRequestContext
{
    Guid TenantId { get; set; }
    ClaimsPrincipal? Principal { get; set; }
    
    TenantIsolationStrategy TenantIsolationStrategy { get; set; }

    public void SetPrincipal(ClaimsPrincipal principal)
    {
        Principal = principal;
    }

    public void SetTenantId(Guid tenantId)
    {
        TenantId = tenantId;
    }
}
