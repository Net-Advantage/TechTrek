namespace Nabs.TechTrek.Tests.PersistenceTests;

public abstract class DatabaseTestBase 
    : IClassFixture<DatabaseFixture>, IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;

    protected DatabaseTestBase(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        Fixture = fixture;
        Fixture.CreateScope(testOutputHelper);
    }

    protected DatabaseFixture Fixture { get; private set; }

    public Task InitializeAsync()
    {
        BeforeTestRun();
        return Task.CompletedTask;
    }

    public void WriteLine(string message)
    {
        _testOutputHelper.WriteLine(message);
    }

    protected virtual void BeforeTestRun()
    {
        
    }

    public Task DisposeAsync()
    {
        Fixture = null!;

        return Task.CompletedTask;
    }
}
