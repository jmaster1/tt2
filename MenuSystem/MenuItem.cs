namespace MenuSystem;

public class MenuItem
{
    private string _title = default!;
    
    private Func<string>? _titleFunc;
    
    private string _shortcut = default!;

    public Action? MenuItemAction { get; set; }
    
    public Action<MenuSelection>? MenuItemInputAction { get; set; }

    public string Title
    {
        get => _titleFunc == null ? _title : _titleFunc.Invoke();
        set
        {
            _title = value;
        }
    }
    
    public Func<string>? TitleFunc
    {
        get => _titleFunc;
        set
        {
            _titleFunc = value;
        }
    }

    public string Shortcut
    {
        get => _shortcut;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Shortcut cannot be empty!");
            }
            _shortcut = value;
        }
    }

    public override string ToString()
    {
        return Shortcut + " - " + Title;
    }
}