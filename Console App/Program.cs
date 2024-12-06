 
using DAL;
using GameBrain;
using MenuSystem;
using MenuItem = MenuSystem.MenuItem;

var configRepository = new ConfigRepository();
var deepMenu = new Menu(EMenuLevel.Deep, "Tic-Tac-Two DEEP", [
    new MenuItem()
    {
        Shortcut = "Y",
        Title = "YYYYYYY",
        MenuItemAction = DummyMethod
    }
]);
var optionsMenu = new Menu(EMenuLevel.Secondary,
    "TIC-TAC-TWO Options", menuItems: [
    new MenuItem()
    {
        Shortcut = "X",
        Title = "X starts",
        MenuItemAction = deepMenu.Run
    }, new MenuItem()
    {
    Shortcut = "O",
    Title = "O starts",
    MenuItemAction = DummyMethod
    }
]);
var mainMenu = new Menu(EMenuLevel.Main, "TIC-TAC-TWO", [
    new MenuItem()
    {
        Shortcut = "1",
        Title = "Options",
        MenuItemAction = optionsMenu.Run
    },

    new MenuItem()
    {
        Shortcut = "2",
        Title = "New Game",
        MenuItemAction = NewGame
    }
]);
mainMenu.Run();

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