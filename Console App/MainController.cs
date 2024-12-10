using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class MainController(
    IConfigRepository configRepository, 
    IGameRepository gameRepository)
    : AbstractController
{
    private readonly TicTacTwoBrain _gameInstance = new();

    private readonly ConfigController _configController = new(configRepository);
    
    public void Run()
    {
        Menu(Header,
            MenuItem("N", "New game (select config)", OmNewGameSelectConfig),
            MenuItem("D", "New game (default config)", OnNewGameDefaultConfig),
            MenuItem("L", "Load last saved game", OnLoadGame),
            MenuItem("A", "Add configuration", OnAddConfiguration),
            MenuItem("C", "Edit configuration", OnEditConfiguration)
        ).RunUntilExit();
    }

    private void OnLoadGame()
    {
        var snapshot = gameRepository.LoadLastSnapshot();
        _gameInstance.LoadSnapshot(snapshot);
        new GameController(_gameInstance, gameRepository).GameLoop();
    }

    private void OnEditConfiguration()
    {
        var config = _configController.SelectConfig();
        if (config != default)
        {
            new ConfigEditController(configRepository, config).Run();
        }
    }

    private void OnAddConfiguration()
    {
        new ConfigEditController(configRepository, new GameConfiguration()).Run();
    }

    private void NewGame(GameConfiguration config)
    {
        _gameInstance.LoadConfig(config);
        var gameController = new GameController(_gameInstance, gameRepository);
        gameController.GameLoop();
    }

    private void OmNewGameSelectConfig()
    {
        var config = _configController.SelectConfig();
        NewGame(config);
    }

    private void OnNewGameDefaultConfig()
    {
        NewGame(configRepository.GetConfigurationByName(
            configRepository.GetConfigurationNames()[0]));
    }
}
