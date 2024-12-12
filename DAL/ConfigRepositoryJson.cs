using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : AbstractRepositoryJson<GameConfiguration>, IConfigRepository
{
    public List<string> GetConfigurationNames()
    {
        CheckAndCreateInitialConfig();
        return ListNames();
    }

    public GameConfiguration Load(string name)
    {
        return LoadByName(name);
    }

    public void Save(GameConfiguration gameConfig)
    {
        Save(gameConfig, gameConfig.Name);
    }
    
    private void CheckAndCreateInitialConfig()
    {
        var names = ListNames();
        if (names.Count != 0) return;
        var hardcodedRepo = new ConfigRepositoryPredefined();
        var configurationNames = hardcodedRepo.GetConfigurationNames();
        foreach (var gameConfig in configurationNames.Select(name => hardcodedRepo.Load(name)))
        {
            Save(gameConfig);
        }
    }

    protected override string GetExtension()
    { 
        return ".config.json";
    }
}
