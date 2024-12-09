using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : AbstractRepositoryJson<GameConfiguration>, IConfigRepository
{
    public List<string> GetConfigurationNames()
    {
        CheckAndCreateInitialConfig();
        return ListNames();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        return GetByName(name);
    }

    public void SaveConfiguration(GameConfiguration gameConfig)
    {
        Save(gameConfig, gameConfig.Name);
    }
    
    private void CheckAndCreateInitialConfig()
    {
        var names = ListNames();
        if (names.Count != 0) return;
        var hardcodedRepo = new ConfigRepositoryPredefined();
        var configurationNames = hardcodedRepo.GetConfigurationNames();
        foreach (var gameConfig in configurationNames.Select(name => hardcodedRepo.GetConfigurationByName(name)))
        {
            SaveConfiguration(gameConfig);
        }
    }

    protected override string GetExtension()
    { 
        return ".config.json";
    }
}
