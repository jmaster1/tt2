namespace DAL;

public abstract class AbstractRepositoryJson<T>
{
    private readonly string _basePath = 
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + 
        Path.DirectorySeparatorChar + 
        "tic-tac-two" + 
        Path.DirectorySeparatorChar;

    protected abstract string GetExtension();

    protected List<string> ListNames()
    {
        CheckAndCreateBasePath();
        return Directory
            .GetFiles(_basePath, "*" + GetExtension())
            .Select(fullFileName =>
                Path.GetFileNameWithoutExtension(
                    Path.GetFileNameWithoutExtension(fullFileName)
                )
            )
            .ToList();
    }

    protected T? GetByName(string name)
    {
        CheckAndCreateBasePath();
        var jsonStr = File.ReadAllText(_basePath + name + GetExtension());
        var entity = JsonStringSerializer.FromString<T>(jsonStr);
        return entity;
    }

    protected void Save(T entity, string name)
    {
        CheckAndCreateBasePath();
        var jsonStr = JsonStringSerializer.ToString(entity);
        File.WriteAllText(_basePath + name + GetExtension(), jsonStr);
    }


    private void CheckAndCreateBasePath()
    {
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }
}
