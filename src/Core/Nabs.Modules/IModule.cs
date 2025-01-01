namespace Nabs.Modules;

public interface IModule
{
    string Name { get; set; }
    string FullName { get; set; }
    string Description { get; set; }
    string Version { get; set; }


    string Initialize();
}
