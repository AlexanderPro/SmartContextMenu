using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;
using SmartContextMenu.Extensions;
using SmartContextMenu.Hooks;
using SmartContextMenu.Native;

namespace SmartContextMenu.Forms
{
    public partial class MainForm : Form
    {
        private ApplicationSettings _settings;
        private readonly SystemTrayMenu _systemTrayMenu;
        private AboutForm _aboutForm;
        private ApplicationSettingsForm _settingsForm;
        private KeyboardHook _keyboardHook;
        private MouseHook _mouseHook;
        private ContextMenuStrip _menu;

        public MainForm(ApplicationSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            _systemTrayMenu = new SystemTrayMenu();
            _menu = new ContextMenuStrip();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);

            using var process = Process.GetCurrentProcess();
            using var mainModule = process.MainModule;
            
            _keyboardHook = new KeyboardHook(mainModule.ModuleName);
            _keyboardHook.Hooked += KeyboardHooked;
            _keyboardHook.Hooked += KeyboardEscHooked;
            if (_settings.MenuItems.Items.Flatten(x => x.Items).Any(x => x.Type == MenuItemType.Item && x.Key3 != VirtualKey.None && x.Show) ||
                _settings.MenuItems.WindowSizeItems.Any(x => x.Key3 != VirtualKey.None))
            {
                _keyboardHook.Start(_settings.MenuItems);
            }

            _mouseHook = new MouseHook(mainModule.ModuleName);
            _mouseHook.Hooked += MouseHooked;
            if (_settings.MouseButton != MouseButton.None)
            {
                _mouseHook.Start(_settings.Key1, _settings.Key2, _settings.Key3, _settings.Key4, _settings.MouseButton);
            }

            if (_settings.ShowSystemTrayIcon)
            {
                _systemTrayMenu.MenuItemAutoStartClick += MenuItemAutoStartClick;
                _systemTrayMenu.MenuItemSettingsClick += MenuItemSettingsClick;
                _systemTrayMenu.MenuItemAboutClick += MenuItemAboutClick;
                _systemTrayMenu.MenuItemExitClick += MenuItemExitClick;
                _systemTrayMenu.Build(_settings);
                _systemTrayMenu.CheckMenuItemAutoStart(AutoStarter.IsAutoStartByRegisterEnabled(AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyLocation));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_menu.Items.Count > 0 && !_menu.IsDisposed)
            {
                ContextMenuManager.Release(_menu);
            }

            _systemTrayMenu?.Dispose();
            _keyboardHook?.Dispose();
            _mouseHook?.Dispose();
            
            base.OnClosed(e);
        }

        private void MouseHooked(object sender, EventArgs<Tuple<Native.Structs.Point, bool>> e) => BeginInvoke((MethodInvoker)delegate
        {
            if (e.Entity.Item2)
            {
                var handle = User32.WindowFromPoint(e.Entity.Item1);
                var parentHandle = WindowUtils.GetParentWindow(handle);
                if (parentHandle == IntPtr.Zero)
                {
                    return;
                }

                if (_menu.Items.Count > 0 && !_menu.IsDisposed)
                {
                    ContextMenuManager.Release(_menu);
                }

                var window = new Window(parentHandle);
                _menu = ContextMenuManager.Build(_settings, window, MenuItemClick);
                _menu.Show(Cursor.Position);
            }
            else
            {
                if (!_menu.IsDisposed)
                {
                    ContextMenuManager.Release(_menu);
                }
            }
        });

        private void KeyboardHooked(object sender, HotKeyEventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
        });

        private void KeyboardEscHooked(object sender, HotKeyEventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
            if (_menu.Items.Count > 0 && !_menu.IsDisposed)
            {
                ContextMenuManager.Release(_menu);
            }
        });

        private void MenuItemClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem toolStripMenuItem)
            {
                if (toolStripMenuItem.Tag is Settings.MenuItem menuItem)
                {
                    switch (menuItem.Name)
                    {
                    }
                }

                if (toolStripMenuItem.Tag is WindowSizeMenuItem windowSizeMenuItem)
                {
                }

                if (toolStripMenuItem.Tag is MoveToMenuItem moveToMenuItem)
                {
                }

                if (toolStripMenuItem.Tag is StartProgramMenuItem startProgramMenuItem)
                {
                }
            }
        }


        private void MenuItemAutoStartClick(object sender, EventArgs e)
        {
            var keyName = AssemblyUtils.AssemblyProductName;
            var assemblyLocation = AssemblyUtils.AssemblyLocation;
            var autoStartEnabled = AutoStarter.IsAutoStartByRegisterEnabled(keyName, assemblyLocation);
            if (autoStartEnabled)
            {
                AutoStarter.UnsetAutoStartByRegister(keyName);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.UnsetAutoStartByScheduler(keyName);
                }
            }
            else
            {
                AutoStarter.SetAutoStartByRegister(keyName, assemblyLocation);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.SetAutoStartByScheduler(keyName, assemblyLocation);
                }
            }
            ((ToolStripMenuItem)sender).Checked = !autoStartEnabled;
        }

        private void MenuItemAboutClick(object sender, EventArgs e)
        {
            if (_aboutForm == null || _aboutForm.IsDisposed || !_aboutForm.IsHandleCreated)
            {
                _aboutForm = new AboutForm(_settings);
            }
            _aboutForm.Show();
            _aboutForm.Activate();
        }

        private void MenuItemSettingsClick(object sender, EventArgs e)
        {
            if (_settingsForm == null || _settingsForm.IsDisposed || !_settingsForm.IsHandleCreated)
            {
                _settingsForm = new ApplicationSettingsForm(_settings);
                _settingsForm.OkClick += (sender, e) => 
                {
                    _settings = e.Entity;
                    _systemTrayMenu.RefreshLanguage(_settings);

                    _keyboardHook.Stop();
                    if (_settings.MenuItems.Items.Flatten(x => x.Items).Any(x => x.Type == MenuItemType.Item && x.Key3 != VirtualKey.None && x.Show) ||
                        _settings.MenuItems.WindowSizeItems.Any(x => x.Key3 != VirtualKey.None))
                    {
                        _keyboardHook.Start(_settings.MenuItems);
                    }

                    _mouseHook.Stop();
                    if (_settings.MouseButton != MouseButton.None)
                    {
                        _mouseHook.Start(_settings.Key1, _settings.Key2, _settings.Key3, _settings.Key4, _settings.MouseButton);
                    }

                    ApplicationSettingsFile.Save(_settings);
                };
            }

            _settingsForm.Show();
            _settingsForm.Activate();
        }

        private void MenuItemExitClick(object sender, EventArgs e) => Close();
    }
}
