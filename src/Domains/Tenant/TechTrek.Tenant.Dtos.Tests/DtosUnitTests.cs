namespace TechTrek.Tenant.Dtos.Tests
{
    public class DtosUnitTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var dto = new Tenant();

            // Act

            // Assert
            dto.Should().NotBeNull();
        }
    }
}
