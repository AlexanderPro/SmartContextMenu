using System;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Hooks
{
    class KeyboardEventArgs : EventArgs
    {
        public MenuItem MenuItem { get; }

        public WindowSizeMenuItem WindowSizeMenuItem { get; }

        public bool Succeeded { get; set; }

        public KeyboardEventArgs(MenuItem menuItem)
        {
            MenuItem = menuItem;
        }

        public KeyboardEventArgs(WindowSizeMenuItem menuItem)
        {
            WindowSizeMenuItem = menuItem;
        }
    }
}
