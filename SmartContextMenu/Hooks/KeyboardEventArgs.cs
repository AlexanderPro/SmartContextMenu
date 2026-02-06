using System;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Hooks
{
    class KeyboardEventArgs : EventArgs
    {
        public MenuItem MenuItem { get; }

        public WindowSizeMenuItem WindowSizeMenuItem { get; }

        public StartProgramMenuItem StartProgramMenuItem { get; }

        public bool NextMonitor { get; set; }

        public bool PreviousMonitor { get; set; }

        public bool Succeeded { get; set; }

        public KeyboardEventArgs()
        {
        }

        public KeyboardEventArgs(MenuItem menuItem)
        {
            MenuItem = menuItem;
        }

        public KeyboardEventArgs(WindowSizeMenuItem menuItem)
        {
            WindowSizeMenuItem = menuItem;
        }

        public KeyboardEventArgs(StartProgramMenuItem menuItem)
        {
            StartProgramMenuItem = menuItem;
        }
    }
}
