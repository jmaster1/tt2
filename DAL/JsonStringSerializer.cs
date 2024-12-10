using System.Text.Json;

namespace DAL;

public static class JsonStringSerializer
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    
    public static T? FromString<T>(string jsonStr)
    {
        return JsonSerializer.Deserialize<T>(jsonStr);
    }

    public static string ToString<T>(T entity)
    {
        return JsonSerializer.Serialize(entity, JsonOptions);
    }
}
