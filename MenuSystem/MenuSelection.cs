namespace MenuSystem;

public class MenuSelection(MenuItem item, string input, Action<string> messageConsumer)
{
    private readonly string[] _tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    public MenuItem Item => item;

    public bool IsExit()
    {
        return item.Shortcut.Equals(Menu.ShortcutExit);
    }
    
    public string GetString(int i)
    {
        return _tokens[i];
    }

    public int GetInt(int i)
    {
        return int.Parse(GetString(i));
    }

    public char GetChar(int i)
    {
        return GetString(i)[0];
    }

    public void AddMessage(string message)
    {
        messageConsumer(message);
    }
}
