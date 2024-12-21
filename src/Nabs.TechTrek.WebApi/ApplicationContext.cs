using Nabs.Application;
using System.Security.Claims;

namespace Nabs.TechTrek.WebApi;

public sealed class ApplicationContext : IApplicationContext
{
    public ApplicationContext(ClaimsPrincipal principal)
    {
        Principal = principal;
    }

    public ClaimsPrincipal Principal { get; set; }
}