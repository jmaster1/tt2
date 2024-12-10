namespace MenuSystem;

public class MenuSelection(MenuItem item, string input, Action<string> messageConsumer)
{
    public readonly MenuItem Item = item;

    public readonly string Input = input;
    
    private readonly string[] _tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    private readonly Action<string> _messageConsumer = messageConsumer;
    
    public bool IsExit()
    {
        return Item.Shortcut.Equals(Menu.ShortcutExit);
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
