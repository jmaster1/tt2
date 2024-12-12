namespace MenuSystem;

public class MenuItem
{
    private string _title = default!;

    private string _shortcut = default!;

    public Action? MenuItemAction { get; set; }
    
    public Action<MenuSelection>? MenuItemInputAction { get; set; }

    public string Title
    {
        get => TitleFunc == null ? _title : TitleFunc.Invoke();
        set => _title = value;
    }
    
    public Func<string>? TitleFunc { get; set; }

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
