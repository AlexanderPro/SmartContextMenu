using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;

namespace SmartContextMenu
{
    class SystemTrayMenu : IDisposable
    {
        private readonly ContextMenuStrip _systemTrayMenu;
        private readonly ToolStripMenuItem _menuItemAutoStart;
        private readonly ToolStripMenuItem _menuItemRestore;
        private readonly ToolStripMenuItem _menuItemSettings;
        private readonly ToolStripMenuItem _menuItemAbout;
        private readonly ToolStripMenuItem _menuItemExit;
        private readonly ToolStripMenuItem _menuItemHide;
        private readonly ToolStripMenuItem _menuItemTransparency;
        private readonly ToolStripMenuItem _menuItemClickThrough;
        private readonly ToolStripSeparator _menuItemSeparator1;
        private readonly ToolStripSeparator _menuItemSeparator2;
        private readonly NotifyIcon _icon;
        private bool _isBuilt;

        public event EventHandler MenuItemAutoStartClick;
        public event EventHandler MenuItemSettingsClick;
        public event EventHandler MenuItemAboutClick;
        public event EventHandler MenuItemExitClick;
        public event EventHandler MenuItemHideClick;
        public event EventHandler MenuItemTransparencyClick;
        public event EventHandler MenuItemClickThroughClick;

        public SystemTrayMenu()
        {
            _menuItemAutoStart = new ToolStripMenuItem();
            _menuItemRestore = new ToolStripMenuItem();
            _menuItemSettings = new ToolStripMenuItem();
            _menuItemAbout = new ToolStripMenuItem();
            _menuItemSeparator1 = new ToolStripSeparator();
            _menuItemSeparator2 = new ToolStripSeparator();
            _menuItemExit = new ToolStripMenuItem();
            _menuItemHide = new ToolStripMenuItem();
            _menuItemTransparency = new ToolStripMenuItem();
            _menuItemClickThrough = new ToolStripMenuItem();
            var components = new Container();
            _systemTrayMenu = new ContextMenuStrip(components);
            _icon = new NotifyIcon(components);
            _isBuilt = false;
        }

        public void Build(ApplicationSettings settings)
        {
            if (_isBuilt)
            {
                return;
            }

            var manager = new LanguageManager(settings.LanguageName);

            _menuItemAutoStart.Name = "miAutoStart";
            _menuItemAutoStart.Size = new Size(175, 22);
            _menuItemAutoStart.Text = manager.GetString("mi_auto_start");
            _menuItemAutoStart.Click += (sender, e) => MenuItemAutoStartClick?.Invoke(sender, e);

            _menuItemSettings.Name = "miSettings";
            _menuItemSettings.Size = new Size(175, 22);
            _menuItemSettings.Font = new Font(_menuItemSettings.Font.Name, _menuItemSettings.Font.Size, FontStyle.Bold);
            _menuItemSettings.Text = manager.GetString("mi_settings");
            _menuItemSettings.Click += (sender, e) => MenuItemSettingsClick?.Invoke(sender, e);

            _menuItemAbout.Name = "miAbout";
            _menuItemAbout.Size = new Size(175, 22);
            _menuItemAbout.Text = manager.GetString("mi_about");
            _menuItemAbout.Click += (sender, e) => MenuItemAboutClick?.Invoke(sender, e);

            _menuItemSeparator1.Name = "miSeparator1";
            _menuItemSeparator1.Size = new Size(172, 6);

            _menuItemSeparator2.Name = "miSeparator2";
            _menuItemSeparator2.Size = new Size(172, 6);

            _menuItemExit.Name = "miExit";
            _menuItemExit.Size = new Size(175, 22);
            _menuItemExit.Text = manager.GetString("mi_exit");
            _menuItemExit.Click += (sender, e) => MenuItemExitClick?.Invoke(sender, e);

            var hideAny = settings.MenuItems.Items.Any(x => x.Type == MenuItemType.Item && x.Name == MenuItemName.Hide && x.Show);
            var clickThroughAny = settings.MenuItems.Items.Any(x => x.Type == MenuItemType.Item && x.Name == MenuItemName.ClickThrough && x.Show);
            var transparencyAny = settings.MenuItems.Items.Any(x => x.Type == MenuItemType.Group && x.Name == MenuItemName.Transparency && x.Show);

            if (hideAny || clickThroughAny || transparencyAny)
            {
                _menuItemRestore.Name = "miRestore";
                _menuItemRestore.Size = new Size(175, 22);
                _menuItemRestore.Text = manager.GetString("mi_restore_windows");

                if (hideAny)
                {
                    _menuItemHide.Name = "miHide";
                    _menuItemHide.Size = new Size(175, 22);
                    _menuItemHide.Text = manager.GetString("hide");
                    _menuItemHide.Click += (sender, e) => MenuItemHideClick?.Invoke(sender, e);
                    _menuItemRestore.DropDownItems.Add(_menuItemHide);
                }

                if (clickThroughAny)
                {
                    _menuItemClickThrough.Name = "miClickThrough";
                    _menuItemClickThrough.Size = new Size(175, 22);
                    _menuItemClickThrough.Text = manager.GetString("click_through");
                    _menuItemClickThrough.Click += (sender, e) => MenuItemClickThroughClick?.Invoke(sender, e);
                    _menuItemRestore.DropDownItems.Add(_menuItemClickThrough);
                }

                if (transparencyAny)
                {
                    _menuItemTransparency.Name = "miTransparency";
                    _menuItemTransparency.Size = new Size(175, 22);
                    _menuItemTransparency.Text = manager.GetString("transparency");
                    _menuItemTransparency.Click += (sender, e) => MenuItemTransparencyClick?.Invoke(sender, e);
                    _menuItemRestore.DropDownItems.Add(_menuItemTransparency);
                }

                _systemTrayMenu.Items.AddRange(new ToolStripItem[] { _menuItemAutoStart, _menuItemSeparator1, _menuItemRestore, _menuItemSettings, _menuItemAbout, _menuItemSeparator2, _menuItemExit });
            }
            else
            {
                _systemTrayMenu.Items.AddRange(new ToolStripItem[] { _menuItemAutoStart, _menuItemSeparator1, _menuItemSettings, _menuItemAbout, _menuItemSeparator2, _menuItemExit });
            }

            _systemTrayMenu.Name = "systemTrayMenu";
            _systemTrayMenu.Size = new Size(176, 80);

            _icon.ContextMenuStrip = _systemTrayMenu;
            _icon.Icon = Properties.Resources.SmartContextMenu;
            _icon.Text = AssemblyUtils.AssemblyTitle;
            _icon.Visible = true;
            _icon.DoubleClick += (sender, e) => MenuItemSettingsClick?.Invoke(sender, e);

            _isBuilt = true;
        }

        public void RefreshLanguage(LanguageManager manager)
        {
            _menuItemAutoStart.Text = manager.GetString("mi_auto_start");
            _menuItemSettings.Text = manager.GetString("mi_settings");
            _menuItemAbout.Text = manager.GetString("mi_about");
            _menuItemExit.Text = manager.GetString("mi_exit");
            _menuItemRestore.Text = manager.GetString("mi_restore_windows");
            _menuItemHide.Text = manager.GetString("hide");
            _menuItemClickThrough.Text = manager.GetString("click_through");
            _menuItemTransparency.Text = manager.GetString("transparency");
        }

        public void CheckMenuItemAutoStart(bool check)
        {
            _menuItemAutoStart.Checked = check;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuItemAutoStart?.Dispose();
                _menuItemSettings?.Dispose();
                _menuItemAbout?.Dispose();
                _menuItemExit?.Dispose();
                _menuItemRestore?.Dispose();
                _menuItemHide?.Dispose();
                _menuItemClickThrough?.Dispose();
                _menuItemTransparency?.Dispose();
                _menuItemSeparator1?.Dispose();
                _menuItemSeparator2?.Dispose();
                _systemTrayMenu?.Dispose();
                _icon.Visible = false;
                _icon.Dispose();
            }
        }

        ~SystemTrayMenu()
        {
            Dispose(false);
        }
    }
}