namespace MenuSystem;

public class Menu
{
    public static bool ClearConssole = true;

    public const string ShortcutExit = "E";

    private string MenuHeader { get; set; }
    
    private readonly string _menuDivider;
    
    private List<MenuItem> MenuItems { get; }
    
    private readonly List<string> _messages = [];

    private readonly MenuItem _menuItemExit = new()
    {
        Shortcut = ShortcutExit,
        Title = "Exit"
    };

    private Action? _beforeDraw;

    public Menu(string menuHeader, List<MenuItem> menuItems, char dividerSymbol = '=')
    {
        if (string.IsNullOrWhiteSpace(menuHeader))
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
                var menuItem = choice.Item;
                menuItem.MenuItemAction?.Invoke();
                menuItem.MenuItemInputAction?.Invoke(choice);
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
        {            
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                throw new InvalidOperationException("Please choose an option!");
            }
            userInput = userInput.ToLower();
            foreach (var menuItem in MenuItems.Where(menuItem => userInput.StartsWith(menuItem.Shortcut[..1], 
                         StringComparison.CurrentCultureIgnoreCase)))
            {
                return new MenuSelection(menuItem, 
                    userInput[1..].Trim(), 
                    (message) => _messages.Add(message));
            }
            throw new InvalidOperationException("Please choose from what is available!");
        }
    }

    private void DrawMenu(Exception? error)
    {
        if(ClearConssole)
        {
            Console.Clear();
        }

        try
        {
            _beforeDraw?.Invoke();
        }
        catch (Exception any)
        {
            Console.WriteLine("DRAW ERROR: " + any);
            Console.WriteLine(_menuDivider);
        }
        
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);
        
        if(error != null)
        {
            Console.WriteLine("ERROR: " + error);
            Console.WriteLine(_menuDivider);
        }
        
        _messages.ForEach(message =>
        {
            Console.WriteLine("MESSAGE: " + message);
            Console.WriteLine(_menuDivider);
        });
        _messages.Clear();
        
        foreach (var t in MenuItems)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine();
        Console.Write(">");
    }

    public Menu BeforeDraw(Action action)
    {
        _beforeDraw = action;
        return this;
    }

    public void RunUntilExit()
    {
        while(!Run().IsExit())
        {
        }
    }
}
