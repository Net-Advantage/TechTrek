using System.Security.Claims;

namespace Nabs.Application;

public interface IApplicationContext
{
    ClaimsPrincipal Principal { get; set; }

    public void SetPrincipal(ClaimsPrincipal principal)
    {
        Principal = principal;
    }
}

