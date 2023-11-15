using System;

namespace SmartContextMenu.Hooks
{
    class HotKeyEventArgs : EventArgs
    {
        public string MenuItemId { get; }

        public bool Succeeded { get; set; }

        public HotKeyEventArgs(string menuItemId)
        {
            MenuItemId = menuItemId;
        }
    }
}
