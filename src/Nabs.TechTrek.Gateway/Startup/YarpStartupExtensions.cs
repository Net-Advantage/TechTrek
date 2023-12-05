using Nabs.TechTrek.Gateway.Yarp;

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
				transformBuilderContext.AddDefaultAuthorisationPolicyTransformation(builder.Configuration);
			})
			.AddServiceDiscoveryDestinationResolver();

		return builder;
	}


}
