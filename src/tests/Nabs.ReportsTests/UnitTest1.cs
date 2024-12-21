using FluentAssertions;
using Nabs.TechTrek;

namespace Nabs.ReportsTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Strings.TechTrekPubSub.Should().BeEquivalentTo("pubsub");
        }

        [Fact]
        public void Test2()
        {
            var reversed = Strings.TechTrekPubSub.ReverseString();
            reversed.Should().BeEquivalentTo("busbup");
        }
    }
}