
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
    menuItem("2", "New game", () => NewGame(configController.SelectConfig())),
    menuItem("3", "New default game", NewGameDefault)
);

string NewGameDefault()
{
    NewGame(configRepository.GetConfigurationByName(
            configRepository.GetConfigurationNames()[0]));
    return "";
}

while (!SHORTCUT_EXIT.Equals(mainMenu.Run()));

return;
//=========================================================

string DummyMethod()
{
    Console.Clear();
    Console.Write("Just press any key to get out of here!");
    Console.ReadKey();
    return "foobar";
}


string NewGame(GameConfiguration config)
{
    if(config == default)
    {
        return "R";
    }
    var gameInstance = new TicTacTwoBrain(config);
    var gameController = new GameController(gameInstance);
    return gameController.gameLoop();
    
    // loop
    // draw the board again
    // ask input again, validate input
    // is game over?
    
    return "";


}