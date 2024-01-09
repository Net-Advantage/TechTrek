using System.Text;

namespace Nabs.TechTrek.PersistenceCli;

public static class DataLoaderExtensions
{

    public static T LoadResource<T>(this string resourceFileName) 
        where T : class
    {
        var items = GetJson<T>(x => x.EndsWith(resourceFileName));
        return items;
    }
    
    private static T GetJson<T>(Func<string, bool> predicate) where T : class
    {
        var assembly = typeof(DataLoaderExtensions).Assembly;
        var resourceFileName = assembly.GetManifestResourceNames()
            .FirstOrDefault(predicate) 
            ?? throw new InvalidOperationException("Resource file not found.");

        var resourceStream = assembly.GetManifestResourceStream(resourceFileName);

        using var reader = new StreamReader(resourceStream!, Encoding.UTF8);
        var text = reader.ReadToEnd();

        var result = DefaultJsonSerializer.Deserialize<T>(text);
        return result!;
    }
}