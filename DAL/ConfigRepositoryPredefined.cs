using GameBrain;

namespace DAL;

public class ConfigRepositoryPredefined : IConfigRepository
{
    private readonly List<GameConfiguration> _gameConfigurations =
    [
        new()
        {
            Name = "Classical"
        },
        new()
        {
            Name = "Big board",
            BoardWidth = 10,
            BoardHeight = 10,
            GridWidth = 4,
            GridHeight = 4,
            GridX = 3,
            GridY = 3,
            WinSequence = 4,
            MovePieceAfterNMoves = 4,
            PlayerPieceCount = 16
        },
    ];

    public List<string> GetConfigurationNames()
    {
        return _gameConfigurations
            .OrderBy(x=> x.Name)
            .Select(config => config.Name)
            .ToList();
    }
    
    public GameConfiguration GetConfigurationByName(string name)
    {
        return _gameConfigurations
            .Single(c => c.Name == name);
    }

    public void SaveConfiguration(GameConfiguration gameConfig)
    {
        throw new NotImplementedException();
    }
}