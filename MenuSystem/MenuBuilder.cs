namespace MenuSystem
{
    public static class MenuBuilder
    {
        public static MenuItem MenuItem(string shortcut,string title, Action action)
        {
            return new MenuItem()
            {
                Shortcut = shortcut,
                Title = title,
                MenuItemAction = action
            };
        }
        
        public static MenuItem MenuItem(string shortcut, Func<string> titleFunc, Action action)
        {
            return new MenuItem()
            {
                Shortcut = shortcut,
                TitleFunc = titleFunc,
                MenuItemAction = action
            };
        }

        public static MenuItem MenuItem(string shortcut, string title, Action<MenuSelection> actionWithInput)
        {
            return new MenuItem()
            {
                Shortcut = shortcut,
                Title = title,
                MenuItemInputAction = actionWithInput
            };
        }
        
        public static MenuItem MenuItem(string shortcut, Func<string> titleFunc, Action<MenuSelection> actionWithInput)
        {
            return new MenuItem()
            {
                Shortcut = shortcut,
                TitleFunc = titleFunc,
                MenuItemInputAction = actionWithInput
            };
        }

        public static MenuItem MenuItem(string shortcut, string title, Menu submenu)
        {
            return MenuItem(shortcut, title, () => submenu.Run());
        }

        public static Menu Menu(string header, params MenuItem[] items)
        {
            return new Menu(header, [.. items]);
        }
    }
}
