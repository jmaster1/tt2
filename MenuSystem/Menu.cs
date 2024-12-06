namespace MenuSystem;

public class Menu
{
    public static bool CLEAR_CONSSOLE = true;

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

    public Action? beforeDraw;

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

    public MenuSelection Run()
    {
        Exception? error = null;
        while (true)
        {
            try
            {
                DrawMenu(error);
                var choice = ProcessInput();
                var menuItem = choice.item;
                var userInput = choice.input;
                menuItem.MenuItemAction?.Invoke();
                menuItem.MenuItemInputAction?.Invoke(userInput);
                return choice;
            } catch(Exception ex)
            {
                error = ex;
            }

        }
    }

    private static string CreateDivider(int length, char symbol)
    {
        return new string(symbol, length);
    }

    private MenuSelection ProcessInput()
    {

        var userInput = "";
        do
        {            
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                throw new InvalidOperationException("Please choose an option!");
            }
            userInput = userInput.ToLower();
            foreach (var menuItem in MenuItems)
            {
                if (!userInput.StartsWith(menuItem.Shortcut, StringComparison.CurrentCultureIgnoreCase)) continue;
                return new MenuSelection(menuItem, userInput.Substring(menuItem.Shortcut.Length));
            }
            throw new InvalidOperationException("Please choose from what is available!");
        } while (true);
    }

    private void DrawMenu(Exception error)
    {
        if(CLEAR_CONSSOLE)
        {
            Console.Clear();
        }
        beforeDraw?.Invoke();
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);
        if(error != null)
        {
            Console.WriteLine("ERROR: " + error.Message);
            Console.WriteLine(_menuDivider);
        }
        foreach (var t in MenuItems)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine();
        Console.Write(">");
    }

    public Menu BeforeDraw(Action action)
    {
        beforeDraw = action;
        return this;
    }

    public void RunUntilReturnOrExit()
    {
        while(!Run().isReturnOrExit());
    }
}
