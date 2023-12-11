using SmartContextMenu.Settings;

namespace SmartContextMenu
{
    class ContextMenuItemValue
    {
        public Window Window { get; set; }

        public MenuItem MenuItem { get; }

        public WindowSizeMenuItem WindowSizeMenuItem { get; }

        public MoveToMenuItem MoveToMenuItem { get; }

        public StartProgramMenuItem StartProgramMenuItem { get; }


        public ContextMenuItemValue(Window window, MenuItem menuItem)
        {
            Window = window;
            MenuItem = menuItem;
        }

        public ContextMenuItemValue(Window window, WindowSizeMenuItem windowSizeMenuItem)
        {
            Window = window;
            WindowSizeMenuItem = windowSizeMenuItem;
        }

        public ContextMenuItemValue(Window window, MoveToMenuItem moveToMenuItem)
        {
            Window = window;
            MoveToMenuItem = moveToMenuItem;
        }

        public ContextMenuItemValue(Window window, StartProgramMenuItem startProgramMenuItem)
        {
            Window = window;
            StartProgramMenuItem = startProgramMenuItem;
        }
    }
}
