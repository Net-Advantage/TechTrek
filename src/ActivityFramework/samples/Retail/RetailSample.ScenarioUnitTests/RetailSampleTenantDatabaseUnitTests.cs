using Nabs.Tests.DatabaseTests;
using Xunit.Abstractions;

namespace RetailSample.ScenarioUnitTests;

public class RetailSampleTenantDatabaseUnitTests(
	ITestOutputHelper testOutputHelper,
	RetailSampleDatabaseTestFixture testFixture)
	: DatabaseTestBase<RetailSampleDatabaseTestFixture>(testOutputHelper, testFixture)
{

}
