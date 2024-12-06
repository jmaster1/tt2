 
using DAL;
using GameBrain;
using MenuSystem;
using MenuItem = MenuSystem.MenuItem;
using static MenuSystem.MenuBuilder;

var configRepository = new ConfigRepository();

menu("TIC-TAC-TWO",
    menuItem("1", "Options", 
        subMenu("TIC-TAC-TWO Options",
            menuItem("X", "X starts", DummyMethod),
            menuItem("O", "O starts", DummyMethod)
        )
    ),
    menuItem("2", "New game", NewGame)
).Run();

return;
//=========================================================

string DummyMethod()
{
    Console.Clear();
    Console.Write("Just press any key to get out of here!");
    Console.ReadKey();
    return "foobar";
}


string NewGame()
{
    var configMenuItems = new List<MenuItem>();

    for (var i = 0; i < configRepository.GetConfigurationNames().Count; i++)
    {
        var returnValue = i.ToString();
        configMenuItems.Add(new MenuItem()
        {
            Title = configRepository.GetConfigurationNames()[i],
            Shortcut = (i+1).ToString(),
            MenuItemAction = () => returnValue
        });
    }
    var configMenu = new Menu(EMenuLevel.Secondary,
        "Tic-Tac-Two - choose configuration",
        configMenuItems, true, '+');
    
    var chosenConfigShortcut = configMenu.Run();
    
    if (!int.TryParse(chosenConfigShortcut, out var configNo))
    {
        return chosenConfigShortcut;
    }
    var chosenConfig = configRepository.GetConfigurationByName(
        configRepository.GetConfigurationNames()[configNo]
        );
    
    var gameInstance = new TicTacTwoBrain(chosenConfig);
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