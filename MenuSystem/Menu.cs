namespace MenuSystem;

public class Menu
{
    public static readonly string SHORTCUT_EXIT = "E";
    public static readonly string SHORTCUT_RETURN = "R";
    public static readonly string SHORTCUT_RETURN_MAIN = "M";

    private string MenuHeader { get; set; }
    private readonly string _menuDivider;
    private List<MenuItem> MenuItems { get; set; }

    // TODO: validate menu items for shortcut conflict!
    private MenuItem _menuItemExit = new()
    {
        Shortcut = SHORTCUT_EXIT,
        Title = "Exit"
    };
    private MenuItem _menuItemReturn = new()
    {
        Shortcut = SHORTCUT_RETURN,
        Title = "Return"
    };
    private MenuItem _menuItemReturnMain = new()
    {
        Shortcut = SHORTCUT_RETURN_MAIN,
        Title = "return to Main menu"
    };
    private EMenuLevel MenuLevel { get; set; }

    public Menu(EMenuLevel menuLevel, string menuHeader, List<MenuItem> menuItems,
        char dividerSymbol = '=')
    {
        if (String.IsNullOrWhiteSpace(menuHeader))
        {
            throw new ApplicationException("Menu header cannot be null or empty.");
        }
        MenuHeader = menuHeader;
        
        if (menuItems == null || menuItems.Count == 0)
        {
            throw new ApplicationException("Menu items cannot be null or empty.");
        }
        
        var dividerLength = menuHeader.Length;
        _menuDivider = CreateDivider(dividerLength, dividerSymbol);
        MenuItems = menuItems;
        MenuLevel = menuLevel;
        
        if (MenuLevel != EMenuLevel.Main)
        {
            MenuItems.Add(_menuItemReturn);
        }
        
        if (MenuLevel == EMenuLevel.Deep)
        {
            MenuItems.Add(_menuItemReturnMain);
        }

        MenuItems.Add(_menuItemExit);

    }

    public string Run()
    {
        //Console.Clear();
        do
        {
            var choice = DisplayMenuUserChoice();
            var menuItem = choice.Item1;
            var userInput = choice.Item2;
            if (menuItem.MenuItemAction != null)
            {
                return menuItem.MenuItemAction();
            }
            if (menuItem.MenuItemInputAction != null)
            {
                return menuItem.MenuItemInputAction(userInput);
            }

            if (menuItem.Shortcut == _menuItemReturn.Shortcut ||
                menuItem.Shortcut == _menuItemReturnMain.Shortcut ||
                menuItem.Shortcut == _menuItemExit.Shortcut)
            {
                return menuItem.Shortcut;
            }
            
        } while (true);
    }

    private static string CreateDivider(int length, char symbol)
    {
        return new string(symbol, length);
    }

    private Tuple<MenuItem, string> DisplayMenuUserChoice()
    {

        var userInput = "";
        do
        {
            Console.WriteLine();
            DrawMenu();
            
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.Clear();
                Console.WriteLine("Please choose an option!");
            }
            else
            {
                userInput = userInput.ToLower();
                foreach (var menuItem in MenuItems)
                {
                    if (!userInput.StartsWith(menuItem.Shortcut.ToLower())) continue;

                    return new Tuple<MenuItem, string>(menuItem, userInput.Substring(menuItem.Shortcut.Length));
                }
                Console.Clear();
                Console.WriteLine("Please choose from what is available!");
            }
        } while (true);
    }

    private void DrawMenu()
    {
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);

        foreach (var t in MenuItems)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine();
        Console.Write(">");
    }
}