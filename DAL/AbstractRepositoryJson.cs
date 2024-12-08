using System.Text.Json;

namespace DAL;

public abstract class AbstractRepositoryJson<T>
{
    public static string BasePath = 
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + 
        Path.DirectorySeparatorChar + 
        "tic-tac-two" + 
        Path.DirectorySeparatorChar;

    private JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

    public abstract string GetExtension();

    public List<string> ListNames()
    {
        CheckAndCreateBasePath();
        return Directory
            .GetFiles(BasePath, "*" + GetExtension())
            .Select(fullFileName =>
                Path.GetFileNameWithoutExtension(
                    Path.GetFileNameWithoutExtension(fullFileName)
                )
            )
            .ToList();
    }

    public T? GetByName(string name)
    {
        CheckAndCreateBasePath();
        var jsonStr = File.ReadAllText(BasePath + name + GetExtension());
        var entity = JsonSerializer.Deserialize<T>(jsonStr);
        return entity;
    }

    public void Save(T entity, string name)
    {
        CheckAndCreateBasePath();
        var jsonStr = JsonSerializer.Serialize(entity, jsonOptions);
        File.WriteAllText(BasePath + name + GetExtension(), jsonStr);
    }


    private void CheckAndCreateBasePath()
    {
        if (!Directory.Exists(BasePath))
        {
            Directory.CreateDirectory(BasePath);
        }
    }
}