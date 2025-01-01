using System.Security.Claims;

namespace Nabs.TechTrek.WebApi;

public sealed class RequestContext : IRequestContext
{
    public RequestContext(ClaimsPrincipal principal)
    {
        Principal = principal;
    }

    public Guid TenantId { get; set; } = Guid.Empty;
    public ClaimsPrincipal? Principal { get; set; }
    public TenantIsolationStrategy TenantIsolationStrategy { get; set; }
}
