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
            MenuItem("N", "New game (select config)", OmNewGameSelectConfig),
            MenuItem("D", "New game (default config)", OnNewGameDefaultConfig),
            MenuItem("A", "Add configuration", OnAddConfiguration),
            MenuItem("E", "Edit configuration", OnEditConfiguration)
        ).RunUntilExit();
    }

    private void OnEditConfiguration()
    {
        var config = configController.SelectConfig();
        if (config != default)
        {
            new ConfigEditController(configRepository, config).Run();
        }
    }

    private void OnAddConfiguration()
    {
        new ConfigEditController(configRepository, new GameConfiguration()).Run();
    }

    private static void NewGame(GameConfiguration config)
    {
        var gameInstance = new TicTacTwoBrain(config);
        var gameController = new GameController(gameInstance);
        gameController.GameLoop();
    }

    private void OmNewGameSelectConfig()
    {
        var config = configController.SelectConfig();
        NewGame(config);
    }

    private void OnNewGameDefaultConfig()
    {
        NewGame(configRepository.GetConfigurationByName(
            configRepository.GetConfigurationNames()[0]));
    }
}
