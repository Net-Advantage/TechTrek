using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Nabs.TechTrek.Gateway.Yarp;

public static class YarpTransformationExtensions
{
	public static void AddDefaultAuthorisationPolicyTransformation(
		this TransformBuilderContext transformBuilderContext, IConfigurationRoot configurationRoot)
	{
		var bearerTokenSettingsSection = configurationRoot.GetRequiredSection("BearerTokenSettings");
		var bearerTokenSettings = new BearerTokenSettings();
		bearerTokenSettingsSection.Bind(bearerTokenSettings);

		var authorisationPolicy = transformBuilderContext.Route.AuthorizationPolicy;

		switch (authorisationPolicy)
		{
			case "JwtBearerPolicy":
				HandleJwtBearerPolicy(transformBuilderContext, bearerTokenSettings);
				break;
			case "Default":
				HandleOpenIdConnectPolicy(transformBuilderContext, bearerTokenSettings);
				break;
			default:
				break;
		}
	}

	private static void HandleJwtBearerPolicy(TransformBuilderContext transformBuilderContext, BearerTokenSettings bearerTokenSettings)
	{
		//throw new NotImplementedException();
	}

	private static void HandleOpenIdConnectPolicy(
				TransformBuilderContext transformBuilderContext,
						BearerTokenSettings bearerTokenSettings)
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


			string[] claimSubjects = ["emails", "name"];
			var claimsToForward = ticket.Principal!.Claims
				.Where(claim => claimSubjects.Contains(claim.Type))
				.ToArray();

			//TODO: DWS: Add more claims here.

			var bearerToken = new BearerTokenFactory(bearerTokenSettings)
				.GenerateTokenFromClaims(claimsToForward);

			transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
		});
	}
}
