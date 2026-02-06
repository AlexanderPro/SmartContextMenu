using System;

namespace SmartContextMenu.Settings
{
    public class WindowSizeMenuItem : ICloneable
    {
        public MenuItemType Type { get; set; }

        public string Title { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public KeyboardShortcut Shortcut { get; set; }

        public WindowSizeMenuItem()
        {
            Type = MenuItemType.Item;
            Title = string.Empty;
            Left = null;
            Top = null;
            Width = null;
            Height = null;
            Shortcut = new KeyboardShortcut();
        }

        public object Clone()
        {
            var menuItemClone = (WindowSizeMenuItem)MemberwiseClone();
            menuItemClone.Shortcut = (KeyboardShortcut)Shortcut.Clone();
            return menuItemClone;
        }
    }
}
