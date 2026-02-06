using System;
using System.Collections.Generic;
using SmartContextMenu.Extensions;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    public class MenuItem : ICloneable
    {
        public MenuItemType Type { get; set; }

        public string Name { get; set; }

        public bool Show { get; set; }

        public KeyboardShortcut Shortcut { get; set; }

        public IList<MenuItem> Items { get; set; }

        public MenuItem()
        {
            Type = MenuItemType.Item;
            Name = string.Empty;
            Show = true;
            Shortcut = new KeyboardShortcut();
            Items = new List<MenuItem>();
        }

        public object Clone()
        {
            var menuItemClone = (MenuItem)MemberwiseClone();
            menuItemClone.Shortcut = (KeyboardShortcut)Shortcut.Clone();
            menuItemClone.Items = new List<MenuItem>();
            foreach (var item in Items)
            {
                menuItemClone.Items.Add((MenuItem)item.Clone());
            }
            return menuItemClone;
        }
    }
}