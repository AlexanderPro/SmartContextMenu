using System;
using System.Linq;
using System.Windows.Forms;
using SmartContextMenu.Settings;

namespace SmartContextMenu
{
    static class ContextMenuManager
    {
        public static ContextMenuStrip Build(ApplicationSettings settings, Window window, EventHandler onClick)
        {
            var menu = new ContextMenuStrip();
            menu.Tag = window;
            var manager = new LanguageManager(settings.LanguageName);
            var moveToMenuItems = SystemUtils.GetMonitors().Select((x, i) => new MoveToMenuItem(i + 1, x)).ToList();

            foreach (var item in settings.MenuItems.Items)
            {
                if (item.Type == MenuItemType.Item && item.Show)
                {
                    var title = GetTransparencyTitle(manager, item);
                    title ??= manager.GetString(item.Name);
                    var menuItem = new ToolStripMenuItem(title);
                    menuItem.ShortcutKeyDisplayString = item.ToString();
                    menuItem.Tag = item;
                    menuItem.Click += onClick;
                    menu.Items.Add(menuItem);
                }

                if (item.Type == MenuItemType.Separator && item.Show)
                {
                    menu.Items.Add(new ToolStripSeparator());
                }

                if (item.Type == MenuItemType.Group && item.Show)
                {
                    var groupTitle = GetTransparencyTitle(manager, item);
                    groupTitle ??= manager.GetString(item.Name);
                    var subMenu = new ToolStripMenuItem();
                    subMenu.Tag = item;
                    menu.Items.Add(subMenu);

                    if (item.Name.ToLower() == MenuItemName.Size)
                    {
                        foreach (var windowSizeItem in settings.MenuItems.WindowSizeItems)
                        {
                            var menuItem = new ToolStripMenuItem(windowSizeItem.Title);
                            menuItem.ShortcutKeyDisplayString = windowSizeItem.ToString();
                            menuItem.Tag = windowSizeItem;
                            menuItem.Click += onClick;
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    if (item.Name.ToLower() == MenuItemName.MoveTo)
                    {
                        foreach (var moveToMenuItem in moveToMenuItems)
                        {
                            var title = $"{manager.GetString("monitor")}{moveToMenuItem.MonitorId}";
                            var menuItem = new ToolStripMenuItem(title);
                            menuItem.Tag = moveToMenuItem;
                            menuItem.Click += onClick;
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    if (item.Name.ToLower() == MenuItemName.StartProgram)
                    {
                        foreach (var startProgramItem in settings.MenuItems.StartProgramItems)
                        {
                            var menuItem = new ToolStripMenuItem(startProgramItem.Title);
                            menuItem.Tag = startProgramItem;
                            menuItem.Click += onClick;
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item && subItem.Show)
                        {
                            var title = GetTransparencyTitle(manager, item);
                            title ??= manager.GetString(item.Name);
                            var menuItem = new ToolStripMenuItem(title);
                            menuItem.ShortcutKeyDisplayString = item.ToString();
                            menuItem.Tag = subItem;
                            menuItem.Click += onClick;
                            subMenu.DropDownItems.Add(menuItem);
                        }

                        if (subItem.Type == MenuItemType.Separator && subItem.Show)
                        {
                            subMenu.DropDownItems.Add(new ToolStripSeparator());
                        }
                    }
                }
            }

            return menu;
        }

        public static void Release(ContextMenuStrip menu)
        {
        }

        private static string GetTransparencyTitle(LanguageManager manager, Settings.MenuItem item) => item.Name switch
        {
            MenuItemName.TransparencyOpaque => $"0%{manager.GetString(item.Name)}",
            MenuItemName.Transparency10 => "10%",
            MenuItemName.Transparency20 => "20%",
            MenuItemName.Transparency30 => "30%",
            MenuItemName.Transparency40 => "40%",
            MenuItemName.Transparency50 => "50%",
            MenuItemName.Transparency60 => "60%",
            MenuItemName.Transparency70 => "70%",
            MenuItemName.Transparency80 => "80%",
            MenuItemName.Transparency90 => "90%",
            MenuItemName.TransparencyInvisible => $"100%{manager.GetString(item.Name)}",
            _ => null
        };
    }
}
