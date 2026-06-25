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


        public ContextMenuItemValue(MenuItem menuItem)
        {
            MenuItem = menuItem;
        }

        public ContextMenuItemValue(WindowSizeMenuItem windowSizeMenuItem)
        {
            WindowSizeMenuItem = windowSizeMenuItem;
        }

        public ContextMenuItemValue(MoveToMenuItem moveToMenuItem)
        {
            MoveToMenuItem = moveToMenuItem;
        }

        public ContextMenuItemValue(StartProgramMenuItem startProgramMenuItem)
        {
            StartProgramMenuItem = startProgramMenuItem;
        }
    }
}
