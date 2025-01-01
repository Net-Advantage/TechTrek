namespace Nabs.Modules.ApiModules.Tests
{
    public class ModuleLoaderUnitTests
    {
        [Fact]
        public void DefaultModuleLoaderTest()
        {
            // Arrange
            var moduleLoader = new ModuleLoader();

            // Act


            // Assert
            moduleLoader.Should().NotBeNull();
        }

        [Fact]
        public void SpecificModuleLoaderTest()
        {
            // Arrange
            var moduleFolder = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
            var moduleLoader = new ModuleLoader(moduleFolder);

            // Act


            // Assert
            moduleLoader.Should().NotBeNull();
        }
    }
}
