using System.Text.Json;

namespace Xsort.Infrastructure.FileStorage;

public abstract class JsonFileStorage
{
    public static T? Load<T>(string path) where T : class
    {
        return !File.Exists(path) 
            ? null 
            : JsonSerializer.Deserialize<T>(File.ReadAllText(path));
    }

    public static void Save<T>(string path, T data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(path, json);
    }
}