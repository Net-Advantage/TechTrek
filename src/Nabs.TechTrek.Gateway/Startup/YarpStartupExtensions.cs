using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;

namespace Nabs.TechTrek.Gateway.Startup;

public static class YarpStartupExtensions
{
	public static WebApplicationBuilder AddYarp(this WebApplicationBuilder builder)
	{
		builder.Services
			.AddReverseProxy()
			.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
			.AddTransforms(transformBuilderContext =>
			{
				var authorisationPolicy = transformBuilderContext.Route.AuthorizationPolicy;
				if (string.Equals("Default", authorisationPolicy))
				{
					transformBuilderContext.AddRequestTransform(async transformContext =>
					{
						// AuthN and AuthZ will have already been completed after request routing.
						var ticket = await transformContext.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

						var isAuthenticated = ticket.Principal?.Identity?.IsAuthenticated ?? false;

						// Reject invalid requests
						if (!isAuthenticated)
						{
							var response = transformContext.HttpContext.Response;
							response.StatusCode = 401;
							return;
						}
						var token = $"{ticket.Principal?.Identity?.Name}";

						transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
					});
				}
			})
			.AddServiceDiscoveryDestinationResolver();

		return builder;
	}


}
