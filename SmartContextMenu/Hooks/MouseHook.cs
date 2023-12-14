using System;
using SmartContextMenu.Native.Structs;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Constants;

namespace SmartContextMenu.Hooks
{
    class MouseHook : IDisposable
    {
        private readonly string _moduleName;
        private readonly MouseHookProc _hookProc;
        private IntPtr _hookHandle;

        public event EventHandler<EventArgs> Hooked;
        public event EventHandler<EventArgs> ClickHooked;

        public VirtualKeyModifier Key1 { get; set; }
        public VirtualKeyModifier Key2 { get; set; }
        public VirtualKey Key3 { get; set; }
        public VirtualKey Key4 { get; set; }
        public MouseButton MouseButton { get; set; }

        public MouseHook(VirtualKeyModifier key1, VirtualKeyModifier key2, VirtualKey key3, VirtualKey key4, MouseButton mouseButton, string moduleName)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
            MouseButton = mouseButton;
            _moduleName = moduleName;
            _hookProc = HookProc;
        }

        public bool Start()
        {
            var moduleHandle = GetModuleHandle(_moduleName);
            _hookHandle = SetWindowsHookEx(WH_MOUSE_LL, _hookProc, moduleHandle, 0);
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

        private int HookProc(int code, int wParam, IntPtr lParam)
        {
            if (code == HC_ACTION)
            {
                if (MouseButton != MouseButton.None && 
                    (wParam == WM_LBUTTONDOWN || wParam == WM_RBUTTONDOWN || 
                    wParam == WM_MBUTTONDOWN || wParam == WM_LBUTTONUP || 
                    wParam == WM_RBUTTONUP || wParam == WM_MBUTTONUP))
                {
                    var key1 = true;
                    var key2 = true;
                    var key3 = true;
                    var key4 = true;

                    if (Key1 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)Key1) & 0x8000;
                        key1 = Convert.ToBoolean(keyState);
                    }

                    if (Key2 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)Key2) & 0x8000;
                        key2 = Convert.ToBoolean(keyState);
                    }

                    if (Key3 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)Key3) & 0x8000;
                        key3 = Convert.ToBoolean(keyState);
                    }

                    if (Key4 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)Key4) & 0x8000;
                        key4 = Convert.ToBoolean(keyState);
                    }

                    if (key1 && key2 && key3 && key4 && 
                        ((MouseButton == MouseButton.Left && wParam == WM_LBUTTONDOWN) ||
                        (MouseButton == MouseButton.Right && wParam == WM_RBUTTONDOWN) ||
                        (MouseButton == MouseButton.Middle && wParam == WM_MBUTTONDOWN)))
                    {
                        var handler = Hooked;
                        if (handler != null)
                        {
                            handler.BeginInvoke(this, EventArgs.Empty, null, null);
                            return 1;
                        }
                    }

                    if (wParam == WM_LBUTTONDOWN)
                    {
                        var handler = ClickHooked;
                        if (handler != null)
                        {
                            handler.BeginInvoke(this, EventArgs.Empty, null, null);
                        }
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, lParam);
        }
    }
}
