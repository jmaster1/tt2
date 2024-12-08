using System.Text.Json;

namespace DAL;

public static class JsonStringSerializer
{
    private static JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    
    public static T? FromString<T>(string jsonStr)
    {
        return JsonSerializer.Deserialize<T>(jsonStr);
    }

    public static string ToString<T>(T entity)
    {
        return JsonSerializer.Serialize(entity, _jsonOptions);
    }
}
