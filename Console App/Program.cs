
using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;
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
    menuItem("2", "New game", () => NewGame(configController.SelectConfig()))
);

while (!"E".Equals(mainMenu.Run()));

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
    Console.Clear();
    Console.WriteLine();
    ConsoleUI.Visualizer.DrawBoard(gameInstance);
    Console.WriteLine("Give me coordinates <x> <y>:");
    Console.Write(">");
    var input = Console.ReadLine()!;
    var inputSplit = input.Split(",");
    var inputX = int.Parse(inputSplit[0]);
    var inputY = int.Parse(inputSplit[1]);
    gameInstance.MakeMove(inputX, inputY);
    // loop
    // draw the board again
    // ask input again, validate input
    // is game over?
    
    return "";


}