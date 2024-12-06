namespace MenuSystem;


public class Menu
{
    private string MenuHeader { get; set; }
    private readonly string _menuDivider;
    private List<MenuItem> MenuItems { get; set; }
    
    // TODO: validate menu items for shortcut conflict!
    private MenuItem _menuItemExit = new MenuItem()
    {
        Shortcut = "E",
        Title = "Exit"
    };
    private MenuItem _menuItemReturn = new MenuItem()
    {
        Shortcut = "R",
        Title = "Return"
    };
    private MenuItem _menuItemReturnMain = new MenuItem()
    {
        Shortcut = "M",
        Title = "return to Main menu"
    };
    private EMenuLevel MenuLevel { get; set; }
    public void SetMenuItemAction(string shortCut, Func<string> action)
    {
        var menuItem = MenuItems.Single(m => m.Shortcut == shortCut);
        menuItem.MenuItemAction = action;
    }

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
        Console.Clear();
        do
        {
            var menuItem = DisplayMenuUserChoice();
            var menuReturnValue = "";
            if (menuItem.MenuItemAction != null)
            {
                menuReturnValue = menuItem.MenuItemAction();
                return menuReturnValue;
            }

            if (menuItem.Shortcut == _menuItemReturn.Shortcut)
            {
                return menuItem.Shortcut;
            }
            
            if (menuItem.Shortcut == _menuItemExit.Shortcut || menuReturnValue == _menuItemExit.Shortcut)
            {
                return _menuItemExit.Shortcut;
            }

            if ((menuItem.Shortcut == _menuItemReturnMain.Shortcut || menuReturnValue == _menuItemReturnMain.Shortcut) && MenuLevel != EMenuLevel.Main)
            {
                return _menuItemReturnMain.Shortcut;
            }
            
        } while (true);
    }

    private static string CreateDivider(int length, char symbol)
    {
        return new string(symbol, length);
    }

    private MenuItem DisplayMenuUserChoice()
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
                    if (menuItem.Shortcut.ToLower() != userInput) continue;

                    return menuItem;
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