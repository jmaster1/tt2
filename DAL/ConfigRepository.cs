﻿using GameBrain;

namespace DAL;

public class ConfigRepository
{
    private List<GameConfiguration> _gameConfigurations = new List<GameConfiguration>()
    {
        new GameConfiguration()
        {
            Name = "Classical"
        },
        new GameConfiguration()
        {
            Name = "Big board",
            BoardSizeWidth = 10,
            BoardSizeHeight = 10,
            WinSequence = 4,
            MovePieceAfterNMoves = 0
        },
    };

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
}