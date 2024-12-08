using GameBrain;

namespace DAL;

public class ConfigRepository : IConfigRepository
{
    private List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical"
        },
        new GameConfiguration()
        {
            Name = "Big board",
            BoardSize = new System.Drawing.Size(10, 10),
            GridSize = new System.Drawing.Size(4, 4),
            GridPos = new System.Drawing.Point(2, 2),
            WinSequence = 4,
            MovePieceAfterNMoves = 4
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