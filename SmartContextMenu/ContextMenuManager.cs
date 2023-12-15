using System;
using System.Linq;
using System.Windows.Forms;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;

namespace SmartContextMenu
{
    static class ContextMenuManager
    {
        public static void Build(ContextMenuStrip menu, ApplicationSettings settings, Window window, EventHandler onClick)
        {
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
                    menuItem.Tag = new ContextMenuItemValue(window, item);
                    menuItem.Click += onClick;
                    SetChecked(menuItem, window, item);
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
                    var subMenu = new ToolStripMenuItem(groupTitle);
                    subMenu.Tag = new ContextMenuItemValue(window, item);
                    menu.Items.Add(subMenu);

                    if (item.Name.ToLower() == MenuItemName.Size)
                    {
                        foreach (var windowSizeItem in settings.MenuItems.WindowSizeItems)
                        {
                            var menuItem = new ToolStripMenuItem(windowSizeItem.Title);
                            menuItem.ShortcutKeyDisplayString = windowSizeItem.ToString();
                            menuItem.Tag = new ContextMenuItemValue(window, windowSizeItem);
                            menuItem.Click += onClick;
                            SetChecked(menuItem, window, windowSizeItem);
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    if (item.Name.ToLower() == MenuItemName.MoveTo)
                    {
                        foreach (var moveToMenuItem in moveToMenuItems)
                        {
                            var title = $"{manager.GetString("monitor")}{moveToMenuItem.MonitorIndex}";
                            var menuItem = new ToolStripMenuItem(title);
                            menuItem.Tag = new ContextMenuItemValue(window, moveToMenuItem);
                            menuItem.Click += onClick;
                            SetChecked(menuItem, window, moveToMenuItem);
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    if (item.Name.ToLower() == MenuItemName.StartProgram)
                    {
                        foreach (var startProgramItem in settings.MenuItems.StartProgramItems)
                        {
                            var menuItem = new ToolStripMenuItem(startProgramItem.Title);
                            menuItem.Tag = new ContextMenuItemValue(window, startProgramItem);
                            menuItem.Click += onClick;
                            subMenu.DropDownItems.Add(menuItem);
                        }
                    }

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item && subItem.Show)
                        {
                            var title = GetTransparencyTitle(manager, subItem);
                            title ??= manager.GetString(subItem.Name);
                            var menuItem = new ToolStripMenuItem(title);
                            menuItem.ShortcutKeyDisplayString = subItem.ToString();
                            menuItem.Tag = new ContextMenuItemValue(window, subItem);
                            menuItem.Click += onClick;
                            SetChecked(menuItem, window, subItem);
                            subMenu.DropDownItems.Add(menuItem);
                        }

                        if (subItem.Type == MenuItemType.Separator && subItem.Show)
                        {
                            subMenu.DropDownItems.Add(new ToolStripSeparator());
                        }
                    }
                }
            }
        }

        public static void Release(ContextMenuStrip menu, EventHandler onClick)
        {
            menu.Hide();

            var dropDownMenuItems = menu.Items.OfType<ToolStripMenuItem>().SelectMany(x => x.DropDownItems.Cast<ToolStripItem>()).ToArray();
            foreach (var menuItem in dropDownMenuItems)
            {
                menuItem.Click -= onClick;
                menuItem.Dispose();
            }

            var menuItems = menu.Items.Cast<ToolStripItem>().ToArray();
            foreach (var menuItem in menuItems)
            {
                menuItem.Click -= onClick;
                menuItem.Dispose();
            }

            menu.Items.Clear();
        }

        private static void SetChecked(ToolStripMenuItem toolStripMenuItem, Window window, Settings.MenuItem menuItem)
        {
            switch (menuItem.Name)
            {
                case MenuItemName.AlwaysOnTop:
                    {
                        toolStripMenuItem.Checked = window.AlwaysOnTop;
                    }
                    break;

                case MenuItemName.RollUp:
                    {
                        toolStripMenuItem.Checked = window.IsRollUp;
                    }
                    break;

                case MenuItemName.AeroGlass:
                    {
                        toolStripMenuItem.Checked = window.IsAeroGlass;
                    }
                    break;

                case MenuItemName.HideForAltTab:
                    {
                        toolStripMenuItem.Checked = window.IsExToolWindow;
                    }
                    break;

                case MenuItemName.DisableMinimizeButton:
                    {
                        toolStripMenuItem.Checked = window.IsDisabledMinimizeButton;
                    }
                    break;

                case MenuItemName.DisableMaximizeButton:
                    {
                        toolStripMenuItem.Checked = window.IsDisabledMaximizeButton;
                    }
                    break;

                case MenuItemName.DisableCloseButton:
                    {
                        toolStripMenuItem.Checked = window.IsDisabledCloseButton;
                    }
                    break;

                case MenuItemName.TransparencyOpaque:
                case MenuItemName.Transparency10:
                case MenuItemName.Transparency20:
                case MenuItemName.Transparency30:
                case MenuItemName.Transparency40:
                case MenuItemName.Transparency50:
                case MenuItemName.Transparency60:
                case MenuItemName.Transparency70:
                case MenuItemName.Transparency80:
                case MenuItemName.Transparency90:
                case MenuItemName.TransparencyInvisible:
                    {
                        toolStripMenuItem.Checked = menuItem.Name == EnumUtils.GetTransparencyMenuItemName(window.Transparency);
                    }
                    break;

                case MenuItemName.PriorityRealTime:
                case MenuItemName.PriorityHigh:
                case MenuItemName.PriorityAboveNormal:
                case MenuItemName.PriorityNormal:
                case MenuItemName.PriorityBelowNormal:
                case MenuItemName.PriorityIdle:
                    {
                        toolStripMenuItem.Checked = menuItem.Name == EnumUtils.GetPriorityMenuItemName(window.ProcessPriority);
                    }
                    break;
            }
        }

        private static void SetChecked(ToolStripMenuItem toolStripMenuItem, Window window, WindowSizeMenuItem menuItem)
        {
            var size = window.Size;
            toolStripMenuItem.Checked = menuItem.Width == size.Width && menuItem.Height == size.Height;
        }

        private static void SetChecked(ToolStripMenuItem toolStripMenuItem, Window window, MoveToMenuItem menuItem)
        {
            var screenFromHandle = Screen.FromHandle(window.Handle);
            var screen = Screen.AllScreens.Select((x, i) => new { Index = i + 1, Item = x }).FirstOrDefault(x => x.Item.Equals(screenFromHandle));
            toolStripMenuItem.Checked = menuItem.MonitorIndex == screen?.Index;
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
