﻿using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;
using SmartContextMenu.Extensions;
using SmartContextMenu.Hooks;
using SmartContextMenu.Native;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Forms
{
    public partial class MainForm : Form
    {
        private static User32.WinEventDelegate _winEventProc;

        private ApplicationSettings _settings;
        private AboutForm _aboutForm;
        private ApplicationSettingsForm _settingsForm;
        private KeyboardHook _keyboardHook;
        private MouseHook _mouseHook;
        private IntPtr _hWinEventHookDestroy;
        private IntPtr _hWinEventHookMinimize;
        private IntPtr _hWinEventHookForeground;
        private IntPtr _dimHandle;
        private readonly ContextMenuStrip _menu;
        private readonly List<DimForm> _dimForms;
        private readonly IDictionary<IntPtr, Window> _windows;
        private readonly SystemTrayMenu _systemTrayMenu;

        public MainForm(ApplicationSettings settings, params Window[] windows)
        {
            InitializeComponent();
            _settings = settings;
            _systemTrayMenu = new SystemTrayMenu();
            _menu = new ContextMenuStrip();
            _dimHandle = IntPtr.Zero;
            _dimForms = new List<DimForm>();
            _windows = new Dictionary<IntPtr, Window>();
            foreach (var window in windows)
            {
                _windows.Add(window.Handle, window);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);

            using var process = Process.GetCurrentProcess();
            using var mainModule = process.MainModule;
            
            _keyboardHook = new KeyboardHook(_settings.MenuItems, mainModule.ModuleName);
            _keyboardHook.MenuItemHooked += MenuItemHooked;
            _keyboardHook.WindowSizeMenuItemHooked += WindowSizeMenuItemHooked;
            _keyboardHook.StartProgramMenuItemHooked += StartProgramMenuItemHooked;
            _keyboardHook.EscKeyHooked += EscKeyHooked;
            _keyboardHook.Start();

            _mouseHook = new MouseHook(_settings.Key1, _settings.Key2, _settings.Key3, _settings.Key4, _settings.MouseButton, mainModule.ModuleName);
            _mouseHook.Hooked += MouseHooked;
            _mouseHook.ClickHooked += ClickHooked;
            _mouseHook.Start();

            _winEventProc = new User32.WinEventDelegate(WinEventProc);
            _hWinEventHookDestroy = User32.SetWinEventHook(Constants.EVENT_OBJECT_DESTROY, Constants.EVENT_OBJECT_DESTROY, IntPtr.Zero, _winEventProc, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);
            _hWinEventHookMinimize = User32.SetWinEventHook(Constants.EVENT_SYSTEM_MINIMIZESTART, Constants.EVENT_SYSTEM_MINIMIZESTART, IntPtr.Zero, _winEventProc, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);
            _hWinEventHookForeground = User32.SetWinEventHook(Constants.EVENT_SYSTEM_FOREGROUND, Constants.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _winEventProc, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);

            if (_settings.ShowSystemTrayIcon)
            {
                var manager = new LanguageManager(_settings.LanguageName);
                _systemTrayMenu.MenuItemAutoStartClick += SystemTrayMenuItemAutoStartClick;
                _systemTrayMenu.MenuItemSettingsClick += SystemTrayMenuItemSettingsClick;
                _systemTrayMenu.MenuItemAboutClick += SystemTrayMenuItemAboutClick;
                _systemTrayMenu.MenuItemExitClick += SystemTrayMenuItemExitClick;
                _systemTrayMenu.MenuItemHideClick += SystemTrayMenuItemHideClick;
                _systemTrayMenu.MenuItemClickThroughClick += SystemTrayMenuItemClickThroughClick;
                _systemTrayMenu.MenuItemTransparencyClick += SystemTrayMenuItemTransparencyClick;
                _systemTrayMenu.Build(_settings);
                _systemTrayMenu.CheckMenuItemAutoStart(AutoStarter.IsEnabled(AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyLocation));
            }

            BringToFront();
            Activate();
        }

        protected override void OnClosed(EventArgs e)
        {
            User32.UnhookWinEvent(_hWinEventHookDestroy);
            User32.UnhookWinEvent(_hWinEventHookMinimize);
            User32.UnhookWinEvent(_hWinEventHookForeground);

            foreach (Window window in _windows.Values)
            {
                window.Dispose();
            }

            ContextMenuManager.Release(_menu, MenuItemClick);
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

            ContextMenuManager.Release(_menu, MenuItemClick);
            var manager = new LanguageManager(_settings.LanguageName);
            var window = _windows.ContainsKey(parentHandle) ? _windows[parentHandle] : new Window(parentHandle, manager);
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

            ContextMenuManager.Release(_menu, MenuItemClick);
            var manager = new LanguageManager(_settings.LanguageName);
            var window = _windows.ContainsKey(parentHandle) ? _windows[parentHandle] : new Window(parentHandle, manager);
            MenuItemClick(window, e.WindowSizeMenuItem);
            e.Succeeded = true;
        });

        private void StartProgramMenuItemHooked(object sender, KeyboardEventArgs e) => Invoke((MethodInvoker)delegate
        {
            var handle = User32.GetForegroundWindow();
            var parentHandle = WindowUtils.GetParentWindow(handle);
            ContextMenuManager.Release(_menu, MenuItemClick);
            var manager = new LanguageManager(_settings.LanguageName);
            var window = _windows.ContainsKey(parentHandle) ? _windows[parentHandle] : new Window(parentHandle, manager);
            MenuItemClick(window, e.StartProgramMenuItem);
            e.Succeeded = true;
        });

        private void EscKeyHooked(object sender, KeyboardEventArgs e) => Invoke((MethodInvoker)delegate
        {
            if (_menu.Visible)
            {
                ContextMenuManager.Release(_menu, MenuItemClick);
                UpdateDimWindows();
                e.Succeeded = true;
            }
        });

        private void MouseHooked(object sender, EventArgs e) => BeginInvoke((MethodInvoker)delegate
        {
            var cursorPosition = Cursor.Position;
            var handle = User32.WindowFromPoint(new Native.Structs.Point(cursorPosition.X, cursorPosition.Y));
            var parentHandle = WindowUtils.GetParentWindow(handle);
            if (parentHandle == IntPtr.Zero || handle == _menu.Handle || WindowUtils.IsDesktopWindow(parentHandle) || (_dimHandle != IntPtr.Zero && _dimForms.Any(x => x.Handle == parentHandle)))
            {
                return;
            }

            ContextMenuManager.Release(_menu, MenuItemClick);
            var manager = new LanguageManager(_settings.LanguageName);
            var window = _windows.ContainsKey(parentHandle) ? _windows[parentHandle] : new Window(parentHandle, manager);
            ContextMenuManager.Build(_menu, _settings, window, _dimHandle, MenuItemClick);
            User32.SetForegroundWindow(new HandleRef(_menu, _menu.Handle));
            _menu.Show(cursorPosition);
        });

        private void ClickHooked(object sender, EventArgs e) => BeginInvoke((MethodInvoker)delegate
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
                    ContextMenuManager.Release(_menu, MenuItemClick);
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
                case MenuItemName.AlignCenterHorizontally:
                case MenuItemName.AlignCenterVertically:
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
                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
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
                        var manager = new LanguageManager(_settings.LanguageName);
                        var sizeForm = new SizeForm(manager, window);
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
                                window.SetSize(sizeForm.WindowWidth == null ? null : (sizeForm.WindowWidth + margins.Left + margins.Right),
                                               sizeForm.WindowHeight == null ? null : (sizeForm.WindowHeight + margins.Top + margins.Bottom),
                                               sizeForm.WindowLeft,
                                               sizeForm.WindowTop);
                            }
                            else
                            {
                                window.SetSize(sizeForm.WindowWidth == null ? null : (sizeForm.WindowWidth + (window.Size.Width - window.ClientSize.Width)),
                                               sizeForm.WindowHeight == null ? null : (sizeForm.WindowHeight + (window.Size.Height - window.ClientSize.Height)),
                                               sizeForm.WindowLeft,
                                               sizeForm.WindowTop);
                            }
                        }
                    }
                    break;

                case MenuItemName.TransparencyCustom:
                    {
                        var manager = new LanguageManager(_settings.LanguageName);
                        var opacityForm = new TransparencyForm(manager, window);
                        var result = opacityForm.ShowDialog(window.Win32Window);
                        if (result == DialogResult.OK)
                        {
                            window.SetTransparency(opacityForm.WindowTransparency);
                            if (!_windows.ContainsKey(window.Handle))
                            {
                                _windows.Add(window.Handle, window);
                            }
                        }
                    }
                    break;

                case MenuItemName.AlignCustom:
                    {
                        var manager = new LanguageManager(_settings.LanguageName);
                        var positionForm = new PositionForm(manager, window);
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
                        Task.Factory.StartNew(() =>
                        {
                            var windowDetails = window.GetWindowInfo();
                            BeginInvoke((MethodInvoker)delegate
                            {
                                var manager = new LanguageManager(_settings.LanguageName);
                                var infoForm = new InformationForm(manager, windowDetails);
                                infoForm.Show(window.Win32Window);
                            });
                        });
                    }
                    break;

                case MenuItemName.ChangeTitle:
                    {
                        var manager = new LanguageManager(_settings.LanguageName);
                        var titleForm = new TitleForm(manager);
                        titleForm.Title = window.GetWindowText();
                        var result = titleForm.ShowDialog(window.Win32Window);
                        
                        if (result == DialogResult.OK)
                        {
                            window.SetWindowText(titleForm.Title);
                        }
                    }
                    break;

                case MenuItemName.AlwaysOnTop:
                    {
                        window.MakeAlwaysOnTop(!window.AlwaysOnTop);
                    }
                    break;

                case MenuItemName.RollUp:
                    {
                        window.RollUpDown();
                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.AeroGlass:
                    {
                        window.AeroGlass();
                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.Borderless:
                    {
                        if (window.IsBorderless)
                        {
                            window.RestoreBorder();
                        }
                        else
                        {
                            window.MakeBorderless();
                            if (!_windows.ContainsKey(window.Handle))
                            {
                                _windows.Add(window.Handle, window);
                            }
                        }
                    }
                    break;

                case MenuItemName.Dimmer:
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            if (_dimHandle != window.Handle)
                            {
                                _dimHandle = window.Handle;
                                User32.SetForegroundWindow(_dimHandle);
                                ShowDimWindows();
                                UpdateDimWindows();
                            }
                            else
                            {
                                HideDimWindows();
                            }
                        });
                    }
                    break;

                case MenuItemName.Hide:
                    {
                        if (window.IsVisible)
                        {
                            User32.ShowWindow(window.Handle, (int)WindowShowStyle.Hide);
                            if (!_windows.ContainsKey(window.Handle))
                            {
                                _windows.Add(window.Handle, window);
                            }
                        }
                        else
                        {
                            User32.ShowWindow(window.Handle, (int)WindowShowStyle.Show);
                        }
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
                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.SendToBottom:
                    {
                        window.SendToBottom();
                    }
                    break;

                case MenuItemName.SaveScreenshot:
                    {
                        var manager = new LanguageManager(_settings.LanguageName);
                        var result = WindowUtils.PrintWindow(window.Handle, out var bitmap);
                        if (!result || !WindowUtils.IsCorrectScreenshot(window.Handle, bitmap))
                        {
                            WindowUtils.CaptureWindow(window.Handle, false, out bitmap);
                        }

                        var dialog = new SaveFileDialog
                        {
                            OverwritePrompt = true,
                            ValidateNames = true,
                            Title = manager.GetString("save_screenshot_title"),
                            FileName = manager.GetString("save_screenshot_filename"),
                            DefaultExt = manager.GetString("save_screenshot_default_ext"),
                            RestoreDirectory = false,
                            Filter = manager.GetString("save_screenshot_filter")
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

                            Task.Factory.StartNew(() =>
                            {
                                bitmap.Save(dialog.FileName, imageFormat);
                            });
                        }
                    }
                    break;

                case MenuItemName.CopyScreenshot:
                    {
                        var result = WindowUtils.PrintWindow(window.Handle, out var bitmap);
                        if (!result || !WindowUtils.IsCorrectScreenshot(window.Handle, bitmap))
                        {
                            WindowUtils.CaptureWindow(window.Handle, false, out bitmap);
                        }

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
                        Task.Factory.StartNew(() =>
                        {
                            SystemUtils.RunAs("explorer.exe", "/select, " + window.Process.GetMainModuleFileName(), true);
                        });
                    }
                    break;

                case MenuItemName.ChangeIcon:
                    {
                        try
                        {
                            var manager = new LanguageManager(_settings.LanguageName);
                            var dialog = new OpenFileDialog
                            {
                                ValidateNames = true,
                                DefaultExt = manager.GetString("icon_default_ext"),
                                RestoreDirectory = false,
                                Filter = manager.GetString("icon_filter")
                            };

                            if (dialog.ShowDialog(window.Win32Window) == DialogResult.OK)
                            {
                                window.ChangeIcon(dialog.FileName);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
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

                case MenuItemName.SystemMenuRestore:
                    {
                        window.Restore();
                    }
                    break;

                case MenuItemName.SystemMenuMinimize:
                    {
                        window.Minimize();
                    }
                    break;

                case MenuItemName.SystemMenuMaximize:
                    {
                        window.Maximize();
                    }
                    break;

                case MenuItemName.SystemMenuClose:
                    {
                        window.Close();
                    }
                    break;

                case MenuItemName.SystemMenuAvailable:
                    {
                        if (window.IsSystemMenu)
                        {
                            window.HideSystemMenu();
                        }
                        else
                        {
                            window.ShowSystemMenu();
                        }
                    }
                    break;

                case MenuItemName.MinimizeToSystemtray:
                    {
                        window.MinimizeToSystemTray();

                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.MinimizeAlwaysToSystemtray:
                    {
                        window.IsMinimizeAlwaysToSystemtray = !window.IsMinimizeAlwaysToSystemtray;

                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.SuspendToSystemtray:
                    {
                        window.MinimizeToSystemTray();
                        Thread.Sleep(100);
                        window.Suspend();

                        if (!_windows.ContainsKey(window.Handle))
                        {
                            _windows.Add(window.Handle, window);
                        }
                    }
                    break;

                case MenuItemName.MinimizeOtherWindows:
                case MenuItemName.CloseOtherWindows:
                    {
                        Task.Factory.StartNew(() =>
                        {
                            User32.EnumWindows((IntPtr handle, int lParam) =>
                            {
                                if (handle != IntPtr.Zero && handle != window.Handle && WindowUtils.IsAltTabWindow(handle))
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
                        });
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
                window.SetSize(menuItem.Width == null ? null : (menuItem.Width + margins.Left + margins.Right),
                               menuItem.Height == null ? null : (menuItem.Height + margins.Top + margins.Bottom),
                               menuItem.Left,
                               menuItem.Top);
            }
            else
            {
                window.SetSize(menuItem.Width == null ? null : (menuItem.Width + (window.Size.Width - window.ClientSize.Width)),
                               menuItem.Height == null ? null : (menuItem.Height + (window.Size.Height - window.ClientSize.Height)),
                               menuItem.Left,
                               menuItem.Top);
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
                var manager = new LanguageManager(_settings.LanguageName);
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

                    var parameterForm = new ParameterForm(manager, parameterName);
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
                    Task.Factory.StartNew(() =>
                    {
                        SystemUtils.RunAs(menuItem.FileName, arguments, menuItem.ShowWindow, menuItem.UseWindowWorkingDirectory ? Path.GetDirectoryName(processPath) : null);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SystemTrayMenuItemAutoStartClick(object sender, EventArgs e)
        {
            var keyName = AssemblyUtils.AssemblyProductName;
            var assemblyLocation = AssemblyUtils.AssemblyLocation;
            var autoStartEnabled = AutoStarter.IsEnabled(keyName, assemblyLocation);
            if (autoStartEnabled)
            {
                AutoStarter.Disable(keyName);
            }
            else
            {
                AutoStarter.Enable(keyName, assemblyLocation);
            }
            ((ToolStripMenuItem)sender).Checked = !autoStartEnabled;
        }

        private void SystemTrayMenuItemAboutClick(object sender, EventArgs e)
        {
            if (_aboutForm == null || _aboutForm.IsDisposed || !_aboutForm.IsHandleCreated)
            {
                var manager = new LanguageManager(_settings.LanguageName);
                _aboutForm = new AboutForm(manager);
            }
            _aboutForm.Show();
            _aboutForm.Activate();
        }

        private void SystemTrayMenuItemSettingsClick(object sender, EventArgs e)
        {
            if (_settingsForm == null || _settingsForm.IsDisposed || !_settingsForm.IsHandleCreated)
            {
                _settingsForm = new ApplicationSettingsForm(_settings);
                _settingsForm.OkClick += (sender, e) => 
                {
                    _settings = e.Entity;
                    _keyboardHook.MenuItems = _settings.MenuItems;
                    _mouseHook.Key1 = _settings.Key1;
                    _mouseHook.Key2 = _settings.Key2;
                    _mouseHook.Key3 = _settings.Key3;
                    _mouseHook.Key4 = _settings.Key4;
                    _mouseHook.MouseButton = _settings.MouseButton;
                    var manager = new LanguageManager(_settings.LanguageName);
                    _systemTrayMenu.RefreshLanguage(manager);
                    UpdateDimWindowsColor();

                    ApplicationSettingsFile.Save(_settings);
                };
            }

            _settingsForm.Show();
            _settingsForm.Activate();
        }

        private void SystemTrayMenuItemHideClick(object sender, EventArgs e)
        {
            foreach (var window in _windows.Values)
            {
                if (!window.IsVisible)
                {
                    User32.ShowWindow(window.Handle, (int)WindowShowStyle.Show);
                }
            }
        }

        private void SystemTrayMenuItemTransparencyClick(object sender, EventArgs e)
        {
            foreach (var window in _windows.Values)
            {
                window.RestoreTransparency();
            }
        }

        private void SystemTrayMenuItemClickThroughClick(object sender, EventArgs e)
        {
            foreach (var window in _windows.Values)
            {
                var isClickThrough = window.IsClickThrough;
                if (isClickThrough)
                {
                    window.ClickThrough(!isClickThrough);
                }
            }
        }

        private void SystemTrayMenuItemExitClick(object sender, EventArgs e) => Close();

        private void ShowDimWindows()
        {
            if (_dimForms.Any())
            {
                return;
            }

            var opacity = WindowUtils.TransparencyToOpacity(_settings.Dimmer.Transparency);
            var color = ColorTranslator.FromHtml(_settings.Dimmer.Color);
            foreach (var screen in Screen.AllScreens)
            {
                var dimForm = new DimForm(color, opacity)
                {
                    Left = screen.Bounds.Left,
                    Top = screen.Bounds.Top
                };
                dimForm.Click += DimFormClick;
                dimForm.DoubleClick += DimFormClick;
                dimForm.MouseClick += DimFormClick;
                dimForm.MouseDoubleClick += DimFormClick;
                dimForm.Show();
                _dimForms.Add(dimForm);
            }
        }

        private void DimFormClick(object sender, EventArgs e)
        {
            UpdateDimWindows();
        }

        private void HideDimWindows()
        {
            _dimForms.ForEach(w => w.Close());
            _dimForms.Clear();
            _dimHandle = IntPtr.Zero;
        }

        private void UpdateDimWindows()
        {
            if (_dimHandle != IntPtr.Zero)
            {
                foreach (var dimForm in _dimForms)
                {
                    dimForm.Show();
                    User32.PostMessage(dimForm.Handle, Constants.WM_SYSCOMMAND, Constants.SC_MAXIMIZE, 0);
                    User32.SetWindowPos(dimForm.Handle, User32.HWND_TOP, 0, 0, 0, 0, Constants.SWP_NOMOVE | Constants.SWP_NOSIZE | Constants.SWP_NOACTIVATE);
                    User32.SetWindowPos(dimForm.Handle, _dimHandle, 0, 0, 0, 0, Constants.SWP_NOMOVE | Constants.SWP_NOSIZE | Constants.SWP_NOACTIVATE);
                }
            }
        }

        private void UpdateDimWindowsColor()
        {
            if (_dimHandle != IntPtr.Zero)
            {
                foreach (var dimForm in _dimForms)
                {
                    dimForm.BackColor = ColorTranslator.FromHtml(_settings.Dimmer.Color);
                    dimForm.Opacity = WindowUtils.TransparencyToOpacity(_settings.Dimmer.Transparency);
                }
            }
        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == Constants.EVENT_SYSTEM_MINIMIZESTART && idObject == Constants.OBJID_WINDOW)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    if (_dimHandle != IntPtr.Zero && _dimHandle == hwnd)
                    {
                        foreach (var dimForm in _dimForms)
                        {
                            dimForm.Hide();
                        }
                    }

                    if (_windows.TryGetValue(hwnd, out var window) && window.IsMinimizeAlwaysToSystemtray && !window.ExistSystemTrayIcon)
                    {
                        window.MoveToSystemTray();
                    }
                });
            }

            if (eventType == Constants.EVENT_SYSTEM_FOREGROUND && idObject == Constants.OBJID_WINDOW)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    if (_dimHandle != IntPtr.Zero && _dimHandle == hwnd)
                    {
                        UpdateDimWindows();
                    }
                });
            }

            if (eventType == Constants.EVENT_OBJECT_DESTROY && idObject == Constants.OBJID_WINDOW)  
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    if (_dimHandle != IntPtr.Zero && _dimHandle == hwnd)
                    {
                        HideDimWindows();
                        _dimHandle = IntPtr.Zero;
                    }

                    if (_windows.TryGetValue(hwnd, out var window))
                    {
                        window.Dispose();
                        _windows.Remove(hwnd);
                    }
                });
            }
        }
    }
}
