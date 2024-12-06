namespace MenuSystem;


public class MenuSelection
{
    public readonly MenuItem item;

    public readonly string input;

    public MenuSelection(MenuItem item, string input)
    {
        this.item = item;
        this.input = input;
    }

    public bool isExit()
    {
        return item.Shortcut.Equals(Menu.SHORTCUT_EXIT);
    }
}
