using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;
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
            _mouseHook.ClickHooked += ClickHooked;
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
            if (parentHandle == IntPtr.Zero || WindowUtils.IsDesktopWindow(parentHandle))
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
            if (parentHandle == IntPtr.Zero || WindowUtils.IsDesktopWindow(parentHandle))
            {
                return;
            }

            ContextMenuManager.Release(_menu);
            var window = new Window(parentHandle);
            MenuItemClick(window, e.WindowSizeMenuItem);
            e.Succeeded = true;
        });

        private void EscKeyHooked(object sender, KeyboardEventArgs e) => Invoke((MethodInvoker)delegate
        {
            if (_menu.Visible)
            {
                ContextMenuManager.Release(_menu);
                e.Succeeded = true;
            }
        });

        private void MouseHooked(object sender, Hooks.MouseEventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
            var handle = User32.WindowFromPoint(e.Point);
            var parentHandle = WindowUtils.GetParentWindow(handle);
            if (parentHandle == IntPtr.Zero || WindowUtils.IsDesktopWindow(parentHandle))
            {
                return;
            }

            ContextMenuManager.Release(_menu);
            var window = new Window(parentHandle);
            ContextMenuManager.Build(_menu, _settings, window, MenuItemClick);
            _menu.Show(Cursor.Position);
        });

        private void ClickHooked(object sender, Hooks.MouseEventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
            if (_menu.Visible)
            {
                var cursorPosition = Cursor.Position;
                var isCursorOverMenu = _menu.RectangleToScreen(_menu.ClientRectangle).Contains(cursorPosition);
                var isCursorOverSubMenu = false;
                if (!isCursorOverMenu)
                {
                    foreach (ToolStripItem item in _menu.Items)
                    {
                        if (item is ToolStripMenuItem toolStripMenuItem)
                        {
                            foreach (ToolStripItem subItem in toolStripMenuItem.DropDownItems)
                            {
                                if (subItem.Owner is ToolStripDropDownMenu subMenu && subMenu.Visible)
                                {
                                    isCursorOverSubMenu = subMenu.RectangleToScreen(subMenu.ClientRectangle).Contains(cursorPosition);
                                    if (isCursorOverSubMenu)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (isCursorOverSubMenu)
                            {
                                break;
                            }
                        }
                    }
                }

                if (!isCursorOverMenu && !isCursorOverSubMenu)
                {
                    ContextMenuManager.Release(_menu);
                }
            }
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

                case MenuItemName.SizeCustom:
                    {
                        var sizeForm = new SizeForm(_settings, window);
                        var result = sizeForm.ShowDialog(window.Win32Window);
                        if (result == DialogResult.OK)
                        {
                            window.ShowNormal();

                            if (_settings.Sizer == WindowSizerType.WindowWithMargins)
                            {
                                window.SetSize(sizeForm.WindowWidth, sizeForm.WindowHeight, sizeForm.WindowLeft, sizeForm.WindowTop);
                            }
                            else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
                            {
                                var margins = window.GetSystemMargins();
                                window.SetSize(sizeForm.WindowWidth + margins.Left + margins.Right, sizeForm.WindowHeight + margins.Top + margins.Bottom, sizeForm.WindowLeft, sizeForm.WindowTop);
                            }
                            else
                            {
                                window.SetSize(sizeForm.WindowWidth + (window.Size.Width - window.ClientSize.Width), sizeForm.WindowHeight + (window.Size.Height - window.ClientSize.Height), sizeForm.WindowLeft, sizeForm.WindowTop);
                            }
                        }
                    }
                    break;

                case MenuItemName.TransparencyCustom:
                    {
                        var opacityForm = new TransparencyForm(_settings, window);
                        var result = opacityForm.ShowDialog(window.Win32Window);
                        if (result == DialogResult.OK)
                        {
                            window.SetTransparency(opacityForm.WindowTransparency);
                        }
                    }
                    break;

                case MenuItemName.AlignCustom:
                    {
                        var positionForm = new PositionForm(_settings, window);
                        var result = positionForm.ShowDialog(window.Win32Window);

                        if (result == DialogResult.OK)
                        {
                            window.ShowNormal();
                            window.SetPosition(positionForm.WindowLeft, positionForm.WindowTop);
                        }
                    }
                    break;

                case MenuItemName.Information:
                    {
                        var infoForm = new InformationForm(_settings, window.GetWindowInfo());
                        infoForm.Show(window.Win32Window);
                    }
                    break;

                case MenuItemName.AlwaysOnTop:
                    {
                        window.MakeAlwaysOnTop(!window.AlwaysOnTop);
                    }
                    break;

                case MenuItemName.HideForAltTab:
                    {
                        window.HideForAltTab(!window.IsExToolWindow);
                    }
                    break;

                case MenuItemName.ClickThrough:
                    {
                        window.ClickThrough(!window.IsClickThrough);
                    }
                    break;

                case MenuItemName.SendToBottom:
                    {
                        window.SendToBottom();
                    }
                    break;

                case MenuItemName.SaveScreenshot:
                    {
                        var languageManager = new LanguageManager(_settings.LanguageName);
                        var bitmap = WindowUtils.PrintWindow(window.Handle);
                        var dialog = new SaveFileDialog
                        {
                            OverwritePrompt = true,
                            ValidateNames = true,
                            Title = languageManager.GetString("save_screenshot_title"),
                            FileName = languageManager.GetString("save_screenshot_filename"),
                            DefaultExt = languageManager.GetString("save_screenshot_default_ext"),
                            RestoreDirectory = false,
                            Filter = languageManager.GetString("save_screenshot_filter")
                        };

                        if (dialog.ShowDialog(window.Win32Window) == DialogResult.OK)
                        {
                            var fileExtension = Path.GetExtension(dialog.FileName).ToLower();
                            var imageFormat = fileExtension == ".bmp" ? ImageFormat.Bmp :
                                fileExtension == ".gif" ? ImageFormat.Gif :
                                fileExtension == ".jpeg" ? ImageFormat.Jpeg :
                                fileExtension == ".png" ? ImageFormat.Png :
                                fileExtension == ".tiff" ? ImageFormat.Tiff :
                                fileExtension == ".wmf" ? ImageFormat.Wmf : ImageFormat.Bmp;

                            bitmap.Save(dialog.FileName, imageFormat);
                        }
                    }
                    break;

                case MenuItemName.CopyScreenshot:
                    {
                        var bitmap = WindowUtils.PrintWindow(window.Handle);
                        Clipboard.SetImage(bitmap);
                    }
                    break;

                case MenuItemName.CopyWindowText:
                    {
                        var text = window.ExtractText();
                        if (!string.IsNullOrEmpty(text))
                        {
                            Clipboard.SetText(text);
                        }
                    }
                    break;

                case MenuItemName.CopyWindowTitle:
                    {
                        var text = window.GetWindowText();
                        if (!string.IsNullOrEmpty(text))
                        {
                            Clipboard.SetText(text);
                        }
                    }
                    break;

                case MenuItemName.CopyFullProcessPath:
                    {
                        var path = window.Process?.GetMainModuleFileName();
                        if (!string.IsNullOrEmpty(path))
                        {
                            Clipboard.SetText(path);
                        }
                    }
                    break;

                case MenuItemName.ClearСlipboard:
                    {
                        Clipboard.Clear();
                    }
                    break;

                case MenuItemName.OpenFileInExplorer:
                    {
                        SystemUtils.RunAs("explorer.exe", "/select, " + window.Process.GetMainModuleFileName(), true);
                    }
                    break;

                case MenuItemName.DisableMinimizeButton:
                    {
                        window.DisableMinimizeButton(!window.IsDisabledMinimizeButton);
                    }
                    break;

                case MenuItemName.DisableMaximizeButton:
                    {
                        window.DisableMaximizeButton(!window.IsDisabledMaximizeButton);
                    }
                    break;

                case MenuItemName.DisableCloseButton:
                    {
                        window.DisableCloseButton(!window.IsDisabledCloseButton);
                    }
                    break;

                case MenuItemName.MinimizeOtherWindows:
                case MenuItemName.CloseOtherWindows:
                    {
                        User32.EnumWindows((IntPtr handle, int lParam) =>
                        {
                            if (handle != IntPtr.Zero && handle != Handle && handle != window.Handle && WindowUtils.IsAltTabWindow(handle))
                            {
                                if (menuItem.Name == MenuItemName.CloseOtherWindows)
                                {
                                    User32.PostMessage(handle, Constants.WM_CLOSE, 0, 0);
                                }
                                else
                                {
                                    User32.PostMessage(handle, Constants.WM_SYSCOMMAND, Constants.SC_MINIMIZE, 0);
                                }
                            }
                            return true;
                        }, 0);
                    }
                    break;
            }
        }

        private void MenuItemClick(Window window, WindowSizeMenuItem menuItem)
        {
            window.ShowNormal();
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
