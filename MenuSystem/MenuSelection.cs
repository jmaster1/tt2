namespace MenuSystem;

public class MenuSelection(MenuItem item, string input)
{
    public readonly MenuItem Item = item;

    public readonly string Input = input;
    
    private readonly string[] _tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    public bool IsExit()
    {
        return Item.Shortcut.Equals(Menu.ShortcutExit);
    }

    public int GetInt(int i)
    {
        return int.Parse(_tokens[i]);
    }

    public string GetString(int i)
    {
        return _tokens[i];
    }
}
