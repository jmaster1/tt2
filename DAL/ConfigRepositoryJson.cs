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
        List<string> names = ListNames();
        if (names.Count == 0)
        {
            var hardcodedRepo = new ConfigRepositoryPredefined();
            var configurationNames = hardcodedRepo.GetConfigurationNames();
            foreach (var name in configurationNames)
            {
                var gameConfig = hardcodedRepo.GetConfigurationByName(name);
                SaveConfiguration(gameConfig);
            }
        }
    }

    protected override string GetExtension()
    {
        return ".config.json";
    }
}