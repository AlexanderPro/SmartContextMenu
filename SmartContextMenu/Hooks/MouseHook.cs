using System;
using System.Diagnostics;
using SmartContextMenu.Settings;
using SmartContextMenu.Native.Structs;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Constants;

namespace SmartContextMenu.Hooks
{
    class MouseHook : IDisposable
    {
        private readonly string _moduleName;
        private static MouseHookProc _hookProc;
        private IntPtr _moduleHandle;
        private IntPtr _hookHandle;

        public event EventHandler<EventArgs> Hooked;
        public event EventHandler<EventArgs> ClickHooked;

        public ApplicationSettings Settings { get; set; }
        
        public MouseHook(ApplicationSettings settings, string moduleName)
        {
            Settings = settings;
            _moduleName = moduleName;
            _hookProc = HookProc;
        }

        public bool Start()
        {
            _moduleHandle = GetModuleHandle(_moduleName);
            InitializeHook();
            return _hookHandle != IntPtr.Zero;
        }

        public bool Stop()
        {
            if (_hookHandle == IntPtr.Zero)
            {
                return true;
            }
            var hookStoped = UnhookWindowsHookEx(_hookHandle);
            return hookStoped;
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
                // get rid of managed resources
            }

            Stop();
        }

        ~MouseHook()
        {
            Dispose(false);
        }

        private int HookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var stopWatch = Stopwatch.StartNew();

                if ((Settings.MouseButton == MouseButton.Left && wParam == WM_LBUTTONUP) ||
                    (Settings.MouseButton == MouseButton.Right && wParam == WM_RBUTTONUP) ||
                    (Settings.MouseButton == MouseButton.Middle && wParam == WM_MBUTTONUP))
                {
                    var key1 = true;
                    var key2 = true;
                    var key3 = true;
                    var key4 = true;

                    if (Settings.Key1 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)Settings.Key1) & 0x8000;
                        key1 = Convert.ToBoolean(keyState);
                    }

                    if (Settings.Key2 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)Settings.Key2) & 0x8000;
                        key2 = Convert.ToBoolean(keyState);
                    }

                    if (Settings.Key3 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)Settings.Key3) & 0x8000;
                        key3 = Convert.ToBoolean(keyState);
                    }

                    if (Settings.Key4 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)Settings.Key4) & 0x8000;
                        key4 = Convert.ToBoolean(keyState);
                    }

                    if (key1 && key2 && key3 && key4)
                    {
                        var handler = Hooked;
                        if (handler != null)
                        {
                            handler.Invoke(this, EventArgs.Empty);
                            stopWatch.Stop();
                            if (stopWatch.ElapsedMilliseconds > Settings.LowLevelHooksTimeout)
                            {
                                InitializeHook();
                            }
                        }
                    }
                }

                if (Settings.MouseButton != MouseButton.None && wParam == WM_LBUTTONDOWN)
                {
                    var handler = ClickHooked;
                    if (handler != null)
                    {
                        handler.BeginInvoke(this, EventArgs.Empty, null, null);
                    }
                }

                stopWatch.Stop();
                if (stopWatch.ElapsedMilliseconds > Settings.LowLevelHooksTimeout)
                {
                    InitializeHook();
                }
            }

            return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        public void InitializeHook()
        {
            if (_hookHandle != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookHandle);
                _hookHandle = IntPtr.Zero;
            }
            _hookHandle = SetWindowsHookEx(WH_MOUSE_LL, _hookProc, _moduleHandle, 0);
        }
    }
}
