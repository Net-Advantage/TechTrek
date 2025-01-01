using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Nabs.Modules.ApiModules;

public sealed class ModuleLoader : IModuleLoader
{
    private string _modulesPath;
    private List<Assembly> _assemblies = [];
    private List<IModule> _loadedModules = [];

    public ModuleLoader()
    {
        _modulesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Modules");
    }

    public ModuleLoader(string modulesPath)
    {
        _modulesPath = modulesPath;
    }

    public void Initialize()
    {
        var moduleFolders = Directory.GetDirectories(_modulesPath);
        foreach (var moduleFolder in moduleFolders)
        {
            var moduleDllFolder = Path.Combine(moduleFolder, "net472"); // Assuming this is the correct folder name for .NET Framework 4.7.2
            var moduleDllPath = Directory.GetFiles(moduleDllFolder, "Module*.dll").FirstOrDefault();
            if (moduleDllPath == null)
            {
                Console.WriteLine($"No DLL found in {moduleDllFolder}");
                continue;
            }

            try
            {
                var assembly = Assembly.LoadFrom(moduleDllPath);
                _assemblies.Add(assembly);

                // Create a new service provider for each assembly to avoid dependency conflicts
                var services = new ServiceCollection();
                services.Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo<IModule>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

                var provider = services.BuildServiceProvider();
                var plugins = provider.GetServices<IModule>();
                _loadedModules.AddRange(plugins);

                Console.WriteLine($"Loaded module from {moduleDllPath}: {plugins.Count()} plugins found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load or initialize the module from {moduleDllPath}. Error: {ex.Message}");
            }
        }

        // Here you can use loadedModules for whatever purpose you intended with your plugins.
        Console.WriteLine($"Total plugins loaded: {_loadedModules.Count}");

        foreach (var plugin in _loadedModules)
        {
            var version = plugin.Initialize();
            Console.WriteLine($"Plugin: {plugin.Name} - Version: {version}");
        }
    }
}
