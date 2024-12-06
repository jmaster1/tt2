namespace MenuSystem
{
    public static class MenuBuilder
    {
        public static MenuItem menuItem(string shortcut, string title, Func<string> action)
        {
            return new MenuItem()
            {
                Shortcut = shortcut,
                Title = title,
                MenuItemAction = action
            };
        }

        public static MenuItem menuItem(string shortcut, string title, Menu submenu)
        {
            return menuItem(shortcut, title, submenu.Run);
        }

        public static Menu menu(string header, params MenuItem[] items)
        {
            return new Menu(EMenuLevel.Main, header, [.. items]);
        }

        public static Menu subMenu(string header, params MenuItem[] items)
        {
            return new Menu(EMenuLevel.Secondary, header, [.. items]);
        }
    }
}
