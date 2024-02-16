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
				// Intentionally left blank
				break;
		}
	}

	private static void HandleJwtBearerPolicy(
		TransformBuilderContext transformBuilderContext,
		BearerTokenSettings bearerTokenSettings)
	{
		transformBuilderContext.AddRequestTransform(async transformContext =>
		{
			string[] claimSubjects = [ClaimTypes.NameIdentifier];
			await CreateAuthorisationHeader(transformContext, bearerTokenSettings, JwtBearerDefaults.AuthenticationScheme, claimSubjects);
		});
	}

	private static void HandleOpenIdConnectPolicy(
		TransformBuilderContext transformBuilderContext,
		BearerTokenSettings bearerTokenSettings)
	{
		transformBuilderContext.AddRequestTransform(async transformContext =>
		{
			string[] claimSubjects = ["emails", "name"];
			await CreateAuthorisationHeader(transformContext, bearerTokenSettings, OpenIdConnectDefaults.AuthenticationScheme, claimSubjects);
		});
	}

	private static async Task CreateAuthorisationHeader(
		RequestTransformContext transformContext,
		BearerTokenSettings bearerTokenSettings,
		string authenticationScheme,
		string[] claimSubjects)
	{
		var ticket = await transformContext.HttpContext.AuthenticateAsync(authenticationScheme);
		var isAuthenticated = ticket.Principal?.Identity?.IsAuthenticated ?? false;

		if (!isAuthenticated)
		{
			var response = transformContext.HttpContext.Response;
			response.StatusCode = 401;
			return;
		}

		var claimsToForward = ticket.Principal!.Claims
			.Where(claim => claimSubjects.Contains(claim.Type))
			.ToArray();

		var bearerToken = new BearerTokenFactory(bearerTokenSettings)
			.GenerateTokenFromClaims(claimsToForward);

		transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
	}
}
