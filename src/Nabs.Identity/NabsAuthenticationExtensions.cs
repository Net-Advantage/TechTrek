using Microsoft.Extensions.Hosting;

namespace Nabs.Identity;

public static class NabsAuthenticationExtensions
{
	public static IHostApplicationBuilder AddNabsAuthentication(this IHostApplicationBuilder builder)
	{
		return builder;
	}
}
