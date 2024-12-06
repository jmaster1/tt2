namespace MenuSystem;

public class MenuItem
{
    private string _title = default!;
    private string _shortcut = default!;

    public Func<string>? MenuItemAction { get; set; }
    public Func<string, string>? MenuItemInputAction { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Title cannot be empty!");
            }
            _title = value;
        }
    }

    public string Shortcut
    {
        get => _shortcut;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
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