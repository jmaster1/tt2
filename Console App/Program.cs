
using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;
using static MenuSystem.Menu;
using Console_App;

IConfigRepository configRepository = new ConfigRepository();
var configController = new ConfigController(configRepository);

var mainMenu = menu("TIC-TAC-TWO",
    menuItem("1", "Options",
        subMenu("TIC-TAC-TWO Options",
            menuItem("X", "X starts", DummyMethod),
            menuItem("O", "O starts", DummyMethod)
        )
    ),
    menuItem("2", "New game", NewGameSelectConfig),
    menuItem("3", "New default game", NewGameDefaultConfig)
);

void NewGame(GameConfiguration config)
{
    var gameInstance = new TicTacTwoBrain(config);
    var gameController = new GameController(gameInstance);
    gameController.gameLoop();
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

while (!SHORTCUT_EXIT.Equals(mainMenu.Run().item.Shortcut));

return;
//=========================================================

void DummyMethod()
{
    Console.Clear();
    Console.Write("Just press any key to get out of here!");
    Console.ReadKey();
}


