using Xunit;

namespace Nabs.Tests;

public abstract class BaseTest<TTestFixture> : IClassFixture<TTestFixture>
    where TTestFixture : BaseTestFixture
{

}
