using GameBrain;

namespace DAL;

public class ConfigRepositoryPredefined : IConfigRepository
{
    private static readonly List<GameConfiguration> GameConfigurations =
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
        return GameConfigurations
            .OrderBy(x=> x.Name)
            .Select(config => config.Name)
            .ToList();
    }
    
    public GameConfiguration Load(string name)
    {
        return GameConfigurations
            .Single(c => c.Name == name);
    }

    public void Save(GameConfiguration gameConfig)
    {
        throw new NotImplementedException();
    }
}