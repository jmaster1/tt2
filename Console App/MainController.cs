using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class MainController(IConfigRepository configRepository, ConfigController configController)
    : AbstractController
{
    public void Run()
    {
        Menu(Header,
            MenuItem("N", "New game", NewGameSelectConfig),
            MenuItem("D", "New default game", NewGameDefaultConfig),
            MenuItem("A", "Add configuration", new ConfigEditController(
                configRepository, new GameConfiguration()).Run),
            MenuItem("E", "Edit configuration", () =>
            {
                var config = configController.SelectConfig();
                if (config != default)
                {
                    new ConfigEditController(configRepository, config).Run();
                }
            })
        ).RunUntilExit();
    }

    void NewGame(GameConfiguration config)
    {
        var gameInstance = new TicTacTwoBrain(config);
        var gameController = new GameController(gameInstance);
        gameController.GameLoop();
    }

    void NewGameSelectConfig()
    {
        GameConfiguration config = configController.SelectConfig();
        NewGame(config);
    }

    void NewGameDefaultConfig()
    {
        NewGame(configRepository.GetConfigurationByName(
            configRepository.GetConfigurationNames()[0]));
    }
}
