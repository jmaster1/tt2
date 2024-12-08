
using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;
using Console_App;

IConfigRepository configRepository = new ConfigRepositoryJson();
var configController = new ConfigController(configRepository);

var mainMenu = menu("TIC-TAC-TWO",
    menuItem("1", "Configurations",
        subMenu("TIC-TAC-TWO Options",
            menuItem("X", "X starts", DummyMethod),
            menuItem("O", "O starts", DummyMethod)
        )
    ),
    menuItem("2", "New game", NewGameSelectConfig),
    menuItem("3", "New default game", NewGameDefaultConfig)
);

void DummyMethod()
{
    throw new NotImplementedException();
}

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

mainMenu.RunUntilReturnOrExit();
