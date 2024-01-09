using System.Text.Json;

namespace Nabs.TechTrek;

public static class DefaultJsonSerializer
{
    public static JsonSerializerOptions JsonSerializerOptions { get; } = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };


    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions)!;
    }

    public static void Serialize<T>(T value)
    {
        JsonSerializer.Serialize(value, JsonSerializerOptions);
    }
}
