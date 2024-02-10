using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.ComponentModel;
using SmartContextMenu.Native;
using SmartContextMenu.Native.Enums;
using SmartContextMenu.Native.Structs;
using SmartContextMenu.Extensions;
using SmartContextMenu.Utils;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Constants;

namespace SmartContextMenu
{
    class Window : IDisposable
    {
        private bool _isManaged;
        private bool _suspended;
        private bool _isLayered;
        private int _beforeRollupHeight;
        private int _defaultTransparency;
        private NotifyIcon _systemTrayIcon;
        private ToolStripMenuItem _menuItemRestore;
        private ToolStripMenuItem _menuItemClose;
        private ContextMenuStrip _systemTrayMenu;

        public IntPtr Handle { get; }

        public bool IsRollUp { get; private set; }

        public bool IsAeroGlass { get; private set; }

        public bool IsMinimizeAlwaysToSystemtray { get; set; }

        public Rect Size 
        {
            get
            {
                GetWindowRect(Handle, out var size);
                return size;
            }
        }

        public Rect ClientSize
        {
            get
            {
                GetClientRect(Handle, out var size);
                return size;
            }
        }

        public Rect SizeOnMonitor
        {
            get
            {
                var monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
                var monitorInfo = new MonitorInfo();
                monitorInfo.Init();
                GetMonitorInfo(monitorHandle, ref monitorInfo);

                var size = new Rect()
                {
                    Left = Size.Left - monitorInfo.rcWork.Left,
                    Right = Size.Right - monitorInfo.rcWork.Right,
                    Top = Size.Top - monitorInfo.rcWork.Top,
                    Bottom = Size.Bottom - monitorInfo.rcWork.Bottom
                };
                return size;
            }
        }

        public int ProcessId
        {
            get
            {
                GetWindowThreadProcessId(Handle, out var processId);
                return processId;
            }
        }

        public Process Process => SystemUtils.GetProcessByIdSafely(ProcessId);

        public Priority ProcessPriority => Process.GetPriority();

        public bool IsVisible => IsWindowVisible(Handle);

        public int Transparency
        {
            get
            {
                var style = GetWindowLong(Handle, GWL_EXSTYLE);
                var isLayeredWindow = (style & WS_EX_LAYERED) == WS_EX_LAYERED;
                if (!isLayeredWindow) return 0;
                GetLayeredWindowAttributes(Handle, out _, out var alpha, out _);
                var transparency = 100 - (int)Math.Round(100 * alpha / 255f, MidpointRounding.AwayFromZero);
                return transparency;
            }
        }

        public bool AlwaysOnTop => WindowUtils.IsAlwaysOnTop(Handle);

        public bool IsDisabledMinimizeButton => WindowUtils.IsDisabledMinimizeButton(Handle);

        public bool IsDisabledMaximizeButton => WindowUtils.IsDisabledMaximizeButton(Handle);

        public bool IsDisabledCloseButton
        {
            get
            {
                var systemMenuHandle = GetSystemMenu(Handle, false);
                var flags = GetMenuState(systemMenuHandle, SC_CLOSE, MF_BYCOMMAND);
                return flags != -1 && (flags & MF_DISABLED) != 0 && (flags & MF_GRAYED) != 0;
            }
        }

        public bool IsExToolWindow => WindowUtils.IsExToolWindow(Handle);

        public bool IsClickThrough => WindowUtils.IsClickThrough(Handle);

        public bool IsRestored => GetWindowPlacement(Handle).showCmd == ShowWindowCommands.SW_SHOWNORMAL;

        public bool IsMinimized => GetWindowPlacement(Handle).showCmd == ShowWindowCommands.SW_SHOWMINIMIZED;

        public bool IsMaximized => GetWindowPlacement(Handle).showCmd == ShowWindowCommands.SW_SHOWMAXIMIZED;

        public IntPtr Owner => GetWindow(Handle, GW_OWNER);
        
        public bool ExistSystemTrayIcon => _systemTrayIcon != null && _systemTrayIcon.Visible;

        public IWin32Window Win32Window => new Win32Window(Handle);

        public Window(IntPtr handle, LanguageManager languageManager)
        {
            Handle = handle;
            _isManaged = true;
            _beforeRollupHeight = Size.Height;
            _isLayered = false;
            _suspended = false;
            _defaultTransparency = Transparency;

            _menuItemRestore = new ToolStripMenuItem();
            _menuItemRestore.Size = new Size(175, 22);
            _menuItemRestore.Name = $"miRestore_{Handle}";
            _menuItemRestore.Text = languageManager.GetString("mi_restore");
            _menuItemRestore.Click += MenuItemRestoreClick;
            
            _menuItemClose = new ToolStripMenuItem();
            _menuItemClose.Size = new Size(175, 22);
            _menuItemClose.Name = $"miClose_{Handle}";
            _menuItemClose.Text = languageManager.GetString("mi_close");
            _menuItemClose.Click += MenuItemCloseClick;
        }

        ~Window()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isManaged)
            {
                RestoreFromSystemTray();
                _menuItemRestore?.Dispose();
                _menuItemClose?.Dispose();
                _systemTrayMenu?.Dispose();
            }
            _isManaged = false;
        }

        public override string ToString() => WindowUtils.GetWindowText(Handle);

        public string GetWindowText() => WindowUtils.GetWindowText(Handle);

        public string GetClassName() => WindowUtils.GetClassName(Handle);

        public string RealGetWindowClass() => WindowUtils.RealGetWindowClass(Handle);

        public WindowDetails GetWindowInfo()
        {
            var process = Process;
            var info = new WindowDetails();
            info.GetWindowText = GetWindowText();
            info.WM_GETTEXT = WindowUtils.GetWmGettext(Handle);
            info.GetClassName = GetClassName();
            info.RealGetWindowClass = RealGetWindowClass();
            info.Handle = Handle;
            info.ParentHandle = GetParent(Handle);
            info.Size = Size;
            info.ClientSize = ClientSize;
            info.FrameBounds = GetSystemMargins();
            info.ProcessId = ProcessId;
            info.ThreadId = WindowUtils.GetThreadId(Handle);
            info.GWL_STYLE = GetWindowLong(Handle, GWL_STYLE);
            info.GWL_EXSTYLE = GetWindowLong(Handle, GWL_EXSTYLE);
            info.GWL_ID = GetWindowLong(Handle, GWL_ID);
            info.GWL_USERDATA = GetWindowLong(Handle, GWL_USERDATA);
            info.GCL_STYLE = GetClassLong(Handle, GCL_STYLE);
            info.GCL_WNDPROC = GetClassLong(Handle, GCL_WNDPROC);
            info.DWL_DLGPROC = GetClassLong(Handle, DWL_DLGPROC);
            info.DWL_USER = GetClassLong(Handle, DWL_USER);
            info.FullPath = process?.GetMainModuleFileName() ?? "";
            info.Priority = ProcessPriority;
            info.StartTime = process == null ? (DateTime?)null : process.StartTime;

            try
            {
                var processInfo = SystemUtils.GetProcessInfo(process.Id);
                info.Owner = processInfo.Owner;
                info.CommandLine = processInfo.CommandLine;
                info.ThreadCount = processInfo.ThreadCount;
                info.HandleCount = processInfo.HandleCount;
                info.VirtualSize = processInfo.VirtualSize;
                info.WorkingSetSize = processInfo.WorkingSetSize;
            }
            catch
            {
            }

            try
            {
                info.FontName = WindowUtils.GetFontName(Handle);
            }
            catch
            {
            }

            try
            {
                var windowInfo = new WindowInfo();
                windowInfo.cbSize = Marshal.SizeOf(windowInfo);
                if (User32.GetWindowInfo(Handle, ref windowInfo))
                {
                    info.WindowInfoExStyle = windowInfo.dwExStyle;
                }
            }
            catch
            {
            }

            try
            {
                var result = GetLayeredWindowAttributes(Handle, out var key, out var alpha, out var flags);
                var layeredWindow = (LayeredWindow)flags;
                info.LWA_ALPHA = layeredWindow.HasFlag(LayeredWindow.LWA_ALPHA);
                info.LWA_COLORKEY = layeredWindow.HasFlag(LayeredWindow.LWA_COLORKEY);
            }
            catch
            {
            }

            try
            {
                info.Instance = process == null ? IntPtr.Zero : process.Modules[0].BaseAddress;
            }
            catch
            {
            }

            try
            {
                info.Parent = Path.GetFileName(process.GetParentProcess().GetMainModuleFileName());
            }
            catch
            {
            }

            try
            {
                var fileVersionInfo = process.MainModule.FileVersionInfo;
                info.ProductName = fileVersionInfo.ProductName;
                info.ProductVersion = fileVersionInfo.ProductVersion;
                info.FileVersion = fileVersionInfo.FileVersion;
                info.Copyright = fileVersionInfo.LegalCopyright;
            }
            catch
            {
            }

            return info;
        }

        public void Suspend()
        {
            _suspended = true;
            Process.Suspend();
        }

        public void Resume()
        {
            _suspended = false;
            Process.Resume();
        }

        public void SetTransparency(int percent)
        {
            var opacity = (byte)Math.Round(255 * (100 - percent) / 100f, MidpointRounding.AwayFromZero);
            WindowUtils.SetOpacity(Handle, opacity);
        }

        public void RestoreTransparency()
        {
            SetTransparency(_defaultTransparency);
        }

        public void SetWidth(int width)
        {
            var size = Size;
            MoveWindow(Handle, size.Left, size.Top, width, size.Height, true);
        }

        public void SetHeight(int height)
        {
            var size = Size;
            MoveWindow(Handle, size.Left, size.Top, size.Width, height, true);
        }

        public void SetSize(int width, int height, int? left = null, int? top = null)
        {
            var size = Size;
            var sizeLeft = left == null ? size.Left : left.Value;
            var sizeTop = top == null ? Size.Top : top.Value;
            MoveWindow(Handle, sizeLeft, sizeTop, width, height, true);
        }

        public void SetLeft(int left)
        {
            var size = Size;
            MoveWindow(Handle, left, size.Top, size.Width, size.Height, true);
        }

        public void SetTop(int top)
        {
            var size = Size;
            MoveWindow(Handle, size.Left, top, size.Width, size.Height, true);
        }

        public void SetPosition(int left, int top)
        {
            var size = Size;
            MoveWindow(Handle, left, top, size.Width, size.Height, true);
        }

        public void SetAlignment(WindowAlignment alignment)
        {
            var x = 0;
            var y = 0;
            var screen = Screen.FromHandle(Handle).WorkingArea;
            var window = Size;

            if (alignment == WindowAlignment.CenterHorizontally)
            {
                SetLeft(((screen.Width - window.Width) / 2) + screen.X);
                return;
            }

            if (alignment == WindowAlignment.CenterVertically)
            {
                SetTop(((screen.Height - window.Height) / 2) + screen.Y);
                return;
            }

            switch (alignment)
            {
                case WindowAlignment.TopLeft:
                    {
                        x = screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.TopCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.TopRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.MiddleLeft:
                    {
                        x = screen.X;
                        y = (((screen.Height - window.Height) / 2) + screen.Y);
                    }
                    break;

                case WindowAlignment.MiddleCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = ((screen.Height - window.Height) / 2) + screen.Y;
                    }
                    break;

                case WindowAlignment.MiddleRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = (((screen.Height - window.Height) / 2) + screen.Y);
                    }
                    break;

                case WindowAlignment.BottomLeft:
                    {
                        x = screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;

                case WindowAlignment.BottomCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;

                case WindowAlignment.BottomRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;
            }
            SetPosition(x, y);
        }

        public void MakeAlwaysOnTop(bool topMost)
        {
            SetWindowPos(Handle, topMost ? HWND_TOPMOST : HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }

        public void DisableMinimizeButton(bool disable)
        {
            WindowUtils.DisableMinimizeButton(Handle, disable);
        }

        public void DisableMaximizeButton(bool disable)
        {
            WindowUtils.DisableMaximizeButton(Handle, disable);
        }

        public void DisableCloseButton(bool disable)
        {
            var systemMenuHandle = GetSystemMenu(Handle, false);
            EnableMenuItem(systemMenuHandle, SC_CLOSE, disable ? MF_BYCOMMAND | MF_DISABLED | MF_GRAYED : MF_BYCOMMAND | MF_ENABLED);
        }

        public void Restore()
        {
            PostMessage(Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
        }

        public void Minimize()
        {
            PostMessage(Handle, WM_SYSCOMMAND, SC_MINIMIZE, 0);
        }

        public void Maximize()
        {
            PostMessage(Handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
        }

        public void Close()
        {
            PostMessage(Handle, WM_CLOSE, 0, 0);
        }

        public void HideForAltTab(bool enable)
        {
            if (enable)
            {
                WindowUtils.SetExToolWindow(Handle);
            }
            else
            {
                WindowUtils.UnsetExToolWindow(Handle);
            }
        }

        public void ClickThrough(bool enable)
        {
            if (enable)
            {
                _isLayered = WindowUtils.IsLayered(Handle);
                WindowUtils.SetClickThrough(Handle);
            }
            else
            {
                if (_isLayered)
                {
                    WindowUtils.UnsetTransparent(Handle);
                }
                else
                {
                    WindowUtils.UnsetClickThrough(Handle);
                }
            }
        }

        public void SendToBottom()
        {
            SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }

        public void MinimizeToSystemTray()
        {
            CreateIconInSystemTray();
            ShowWindowAsync(Handle, (int)WindowShowStyle.Minimize);
            ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void MoveToSystemTray()
        {
            CreateIconInSystemTray();
            ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void ShowNormal()
        {
            ShowWindow(Handle, (int)WindowShowStyle.Normal);
        }

        public void RollUpDown()
        {
            if (IsRollUp)
            {
                SetSize(Size.Width, _beforeRollupHeight);
                IsRollUp = false;
            }
            else
            {
                _beforeRollupHeight = Size.Height;
                SetSize(Size.Width, SystemInformation.CaptionHeight);
                IsRollUp = true;
            }
        }

        public void SetPriority(Priority priority)
        {
            var process = Process;
            if (process != null)
            {
                Kernel32.SetPriorityClass(process.GetHandle(), priority.GetPriorityClass());
            }
        }

        public string ExtractText()
        {
            var text = WindowUtils.ExtractTextFromConsoleWindow(ProcessId);
            text ??= WindowUtils.ExtractTextFromWindow(Handle);
            return text;
        }

        public void AeroGlass()
        {
            var version = Environment.OSVersion.Version;
            if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
            {
                WindowUtils.AeroGlassForVistaAndSeven(Handle, !IsAeroGlass);
            }
            else if (version.Major >= 6 || (version.Major == 6 && version.Minor > 1))
            {
                WindowUtils.AeroGlassForEightAndHigher(Handle, !IsAeroGlass);
            }
            IsAeroGlass = !IsAeroGlass;
        }

        public void MoveToMonitor(IntPtr monitorHandle)
        {
            var currentMonitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
            if (currentMonitorHandle != monitorHandle)
            {
                var currentMonitorInfo = new MonitorInfo();
                currentMonitorInfo.Init();
                GetMonitorInfo(currentMonitorHandle, ref currentMonitorInfo);

                var newMonitorInfo = new MonitorInfo();
                newMonitorInfo.Init();
                GetMonitorInfo(monitorHandle, ref newMonitorInfo);
                GetWindowRect(Handle, out Rect windowRect);

                var left = newMonitorInfo.rcWork.Left + windowRect.Left - currentMonitorInfo.rcWork.Left;
                var top = newMonitorInfo.rcWork.Top + windowRect.Top - currentMonitorInfo.rcWork.Top;
                if (windowRect.Left - currentMonitorInfo.rcWork.Left > newMonitorInfo.rcWork.Width || windowRect.Top - currentMonitorInfo.rcWork.Top > newMonitorInfo.rcWork.Height)
                {
                    left = newMonitorInfo.rcWork.Left;
                    top = newMonitorInfo.rcWork.Top;
                }

                MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);
                Thread.Sleep(10);
                MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);
            }
        }

        public Rect GetSystemMargins()
        {
            var withMargin = GetSizeWithMargins();
            return new Rect
            {
                Left = withMargin.Left - Size.Left,
                Top = withMargin.Top - Size.Top,
                Right = Size.Right - withMargin.Right,
                Bottom = Size.Bottom - withMargin.Bottom
            };
        }

        private void MenuItemRestoreClick(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
        }

        private void MenuItemCloseClick(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
            PostMessage(Handle, WM_CLOSE, 0, 0);
        }

        private void RestoreFromSystemTray()
        {
            if (_systemTrayIcon != null && _systemTrayIcon.Visible)
            {
                if (_suspended)
                {
                    Resume();
                    Thread.Sleep(100);
                }

                _systemTrayIcon.Visible = false;
                /*_systemTrayMenu?.Dispose();
                _systemTrayIcon?.Dispose();
                _systemTrayMenu = null;
                _systemTrayIcon = null;*/

                ShowWindowAsync(Handle, (int)WindowShowStyle.Show);
                ShowWindowAsync(Handle, (int)WindowShowStyle.Restore);
                SetForegroundWindow(Handle);
            }
        }

        private void CreateIconInSystemTray()
        {
            _systemTrayMenu ??= CreateSystemTrayMenu();
            _systemTrayIcon ??= CreateNotifyIcon(_systemTrayMenu);
            _systemTrayIcon.Icon = WindowUtils.GetIcon(Handle);
            var windowText = GetWindowText();
            _systemTrayIcon.Text = windowText.Length > 63 ? windowText.Substring(0, 60).PadRight(63, '.') : windowText;
            _systemTrayIcon.Visible = true;
        }

        private void SystemTrayIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RestoreFromSystemTray();
            }
        }

        private Rect GetSizeWithMargins()
        {
            Rect size;
            if (Environment.OSVersion.Version.Major < 6)
            {
                GetWindowRect(Handle, out size);
            }
            else if (Dwmapi.DwmGetWindowAttribute(Handle, DWMWA_EXTENDED_FRAME_BOUNDS, out size, Marshal.SizeOf(typeof(Rect))) != 0)
            {
                GetWindowRect(Handle, out size);
            }
            return size;
        }

        private ContextMenuStrip CreateSystemTrayMenu()
        {
            var components = new Container();
            var menu = new ContextMenuStrip(components);
            menu.Items.AddRange(new ToolStripItem[] { _menuItemRestore, _menuItemClose });
            menu.Name = $"systemTrayMenu_{Handle}";
            menu.Size = new Size(176, 80);
            return menu;
        }

        private NotifyIcon CreateNotifyIcon(ContextMenuStrip contextMenuStrip)
        {
            var icon = new NotifyIcon();
            icon.ContextMenuStrip = contextMenuStrip;
            icon.MouseClick += SystemTrayIconClick;
            return icon;
        }

        private WindowPlacement GetWindowPlacement(IntPtr handle)
        {
            var placement = new WindowPlacement();
            placement.length = Marshal.SizeOf(placement);
            User32.GetWindowPlacement(handle, ref placement);
            return placement;
        }
    }
}