using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.IO;
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
            _keyboardHook.MenuItemHooked += MenuItemHooked;
            _keyboardHook.WindowSizeMenuItemHooked += WindowSizeMenuItemHooked;
            _keyboardHook.EscKeyHooked += EscKeyHooked;
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
            ContextMenuManager.Release(_menu);
            _systemTrayMenu?.Dispose();
            _keyboardHook?.Dispose();
            _mouseHook?.Dispose();

            base.OnClosed(e);
        }

        private void MenuItemHooked(object sender, KeyboardEventArgs e) => Invoke((MethodInvoker)delegate
        {
            var handle = User32.GetForegroundWindow();
            var parentHandle = WindowUtils.GetParentWindow(handle);
            if (parentHandle == IntPtr.Zero)
            {
                return;
            }

            ContextMenuManager.Release(_menu);
            var window = new Window(parentHandle);
            MenuItemClick(window, e.MenuItem);
            e.Succeeded = true;
        });

        private void WindowSizeMenuItemHooked(object sender, KeyboardEventArgs e) => Invoke((MethodInvoker)delegate
        {
            var handle = User32.GetForegroundWindow();
            var parentHandle = WindowUtils.GetParentWindow(handle);
            if (parentHandle == IntPtr.Zero)
            {
                return;
            }

            ContextMenuManager.Release(_menu);
            var window = new Window(parentHandle);
            MenuItemClick(window, e.WindowSizeMenuItem);
            e.Succeeded = true;
        });

        private void EscKeyHooked(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
        {
            ContextMenuManager.Release(_menu);
        });

        private void MouseHooked(object sender, Hooks.MouseEventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
            var handle = User32.WindowFromPoint(e.Point);
            var parentHandle = WindowUtils.GetParentWindow(handle);
            if (parentHandle == IntPtr.Zero)
            {
                return;
            }

            ContextMenuManager.Release(_menu);
            var window = new Window(parentHandle);
            _menu = ContextMenuManager.Build(_settings, window, MenuItemClick);
            _menu.Show(Cursor.Position);
        });

        private void MenuItemClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.Tag is ContextMenuItemValue itemValue)
            {
                if (itemValue.MenuItem != null)
                {
                    MenuItemClick(itemValue.Window, itemValue.MenuItem);
                }

                if (itemValue.WindowSizeMenuItem != null)
                {
                    MenuItemClick(itemValue.Window, itemValue.WindowSizeMenuItem);
                }

                if (itemValue.MoveToMenuItem != null)
                {
                    MenuItemClick(itemValue.Window, itemValue.MoveToMenuItem);
                }

                if (itemValue.StartProgramMenuItem != null)
                {
                    MenuItemClick(itemValue.Window, itemValue.StartProgramMenuItem);
                }
            }
        }

        private void MenuItemClick(Window window, Settings.MenuItem menuItem)
        {
            switch (menuItem.Name)
            {
                case MenuItemName.AlignTopLeft:
                case MenuItemName.AlignTopCenter:
                case MenuItemName.AlignTopRight:
                case MenuItemName.AlignMiddleLeft:
                case MenuItemName.AlignMiddleCenter:
                case MenuItemName.AlignMiddleRight:
                case MenuItemName.AlignBottomLeft:
                case MenuItemName.AlignBottomCenter:
                case MenuItemName.AlignBottomRight:
                    {
                        window.SetAlignment(EnumUtils.GetAlignment(menuItem.Name));
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
                        window.SetTransparency(EnumUtils.GetTransparency(menuItem.Name));
                    }
                    break;

                case MenuItemName.PriorityRealTime:
                case MenuItemName.PriorityHigh:
                case MenuItemName.PriorityAboveNormal:
                case MenuItemName.PriorityNormal:
                case MenuItemName.PriorityBelowNormal:
                case MenuItemName.PriorityIdle:
                    {
                        window.SetPriority(EnumUtils.GetPriority(menuItem.Name));
                    }
                    break;

                case MenuItemName.AlwaysOnTop:
                    {
                        window.MakeAlwaysOnTop(!window.AlwaysOnTop);
                    }
                    break;
            }
        }

        private void MenuItemClick(Window window, WindowSizeMenuItem menuItem)
        {
            if (_settings.Sizer == WindowSizerType.WindowWithMargins)
            {
                window.SetSize(menuItem.Width, menuItem.Height, menuItem.Left, menuItem.Top);
            }
            else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
            {
                var margins = window.GetSystemMargins();
                window.SetSize(menuItem.Width + margins.Left + margins.Right, menuItem.Height + margins.Top + margins.Bottom, menuItem.Left, menuItem.Top);
            }
            else
            {
                window.SetSize(menuItem.Width + (window.Size.Width - window.ClientSize.Width), menuItem.Height + (window.Size.Height - window.ClientSize.Height), menuItem.Left, menuItem.Top);
            }
        }

        private void MenuItemClick(Window window, MoveToMenuItem menuItem)
        {
            window.MoveToMonitor(menuItem.MonitorHandle);
        }

        private void MenuItemClick(Window window, StartProgramMenuItem menuItem)
        {
            try
            {
                var arguments = menuItem.Arguments;
                var argumentParameters = arguments.GetParams(menuItem.BeginParameter, menuItem.EndParameter);
                var allParametersInputed = true;
                var processPath = window.Process?.GetMainModuleFileName() ?? string.Empty;
                foreach (var parameter in argumentParameters)
                {
                    var parameterName = parameter.TrimStart(menuItem.BeginParameter).TrimEnd(menuItem.EndParameter);
                    if (string.Compare(parameterName, StartProgramMenuItem.PARAMETER_PROCESS_ID, true) == 0)
                    {
                        arguments = arguments.Replace(parameter, window.Process?.Id.ToString() ?? string.Empty);
                        continue;
                    }

                    if (string.Compare(parameterName, StartProgramMenuItem.PARAMETER_PROCESS_NAME, true) == 0)
                    {
                        arguments = arguments.Replace(parameter, Path.GetFileName(processPath));
                        continue;
                    }

                    if (string.Compare(parameterName, StartProgramMenuItem.PARAMETER_WINDOW_TITLE, true) == 0)
                    {
                        arguments = arguments.Replace(parameter, window.GetWindowText());
                        continue;
                    }

                    var parameterForm = new ParameterForm(_settings, parameterName);
                    var result = parameterForm.ShowDialog(window.Win32Window);

                    if (result == DialogResult.OK)
                    {
                        arguments = arguments.Replace(parameter, parameterForm.ParameterValue);
                    }
                    else
                    {
                        allParametersInputed = false;
                        break;
                    }
                }

                if (allParametersInputed)
                {
                    SystemUtils.RunAs(menuItem.FileName, arguments, menuItem.ShowWindow, menuItem.UseWindowWorkingDirectory ? Path.GetDirectoryName(processPath) : null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
