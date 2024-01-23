using Microsoft.Extensions.DependencyInjection;
using Nabs.Tests.DatabaseTests;
using Xunit.Abstractions;

namespace RetailSample.ScenarioUnitTests;

public sealed class RetailSampleDatabaseTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		base.ConfigureServices(services);
	}
}