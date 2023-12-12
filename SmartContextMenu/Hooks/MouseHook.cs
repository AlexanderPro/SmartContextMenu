using System;
using System.Runtime.InteropServices;
using SmartContextMenu.Native.Structs;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Constants;

namespace SmartContextMenu.Hooks
{
    class MouseHook : IDisposable
    {
        private readonly string _moduleName;
        private IntPtr _hookHandle;
        private MouseHookProc _hookProc;
        private VirtualKeyModifier _key1;
        private VirtualKeyModifier _key2;
        private VirtualKey _key3;
        private VirtualKey _key4;
        private MouseButton _mouseButton;

        public event EventHandler<MouseEventArgs> Hooked;
        public event EventHandler<MouseEventArgs> ClickHooked;

        public MouseHook(string moduleName)
        {
            _moduleName = moduleName;
        }

        public bool Start(VirtualKeyModifier key1, VirtualKeyModifier key2, VirtualKey key3, VirtualKey key4, MouseButton mouseButton)
        {
            _key1 = key1;
            _key2 = key2;
            _key3 = key3;
            _key4 = key4;
            _mouseButton = mouseButton;
            _hookProc = HookProc;
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
                if (_mouseButton != MouseButton.None && 
                    (wParam == WM_LBUTTONDOWN || wParam == WM_RBUTTONDOWN || 
                    wParam == WM_MBUTTONDOWN || wParam == WM_LBUTTONUP || 
                    wParam == WM_RBUTTONUP || wParam == WM_MBUTTONUP))
                {
                    var key1 = true;
                    var key2 = true;
                    var key3 = true;
                    var key4 = true;

                    if (_key1 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)_key1) & 0x8000;
                        key1 = Convert.ToBoolean(keyState);
                    }

                    if (_key2 != VirtualKeyModifier.None)
                    {
                        var keyState = GetAsyncKeyState((int)_key2) & 0x8000;
                        key2 = Convert.ToBoolean(keyState);
                    }

                    if (_key3 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)_key3) & 0x8000;
                        key3 = Convert.ToBoolean(keyState);
                    }

                    if (_key4 != VirtualKey.None)
                    {
                        var keyState = GetAsyncKeyState((int)_key4) & 0x8000;
                        key4 = Convert.ToBoolean(keyState);
                    }


                    if (key1 && key2 && key3 && key4 && 
                        ((_mouseButton == MouseButton.Left && wParam == WM_LBUTTONDOWN) ||
                        (_mouseButton == MouseButton.Right && wParam == WM_RBUTTONDOWN) ||
                        (_mouseButton == MouseButton.Middle && wParam == WM_MBUTTONDOWN)))
                    {
                        var handler = Hooked;
                        if (handler != null)
                        {
                            var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                            var eventArgs = new MouseEventArgs(mouseHookStruct.pt);
                            handler.BeginInvoke(this, eventArgs, null, null);
                            return 1;
                        }
                    }

                    if (key1 && key2 && key3 && key4 && 
                        ((_mouseButton == MouseButton.Left && wParam == WM_LBUTTONUP) ||
                        (_mouseButton == MouseButton.Right && wParam == WM_RBUTTONUP) ||
                        (_mouseButton == MouseButton.Middle && wParam == WM_MBUTTONUP)))
                    {
                        return 1;
                    }

                    if (wParam == WM_LBUTTONUP || wParam == WM_RBUTTONUP)
                    {
                        var handler = ClickHooked;
                        if (handler != null)
                        {
                            var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                            var eventArgs = new MouseEventArgs(mouseHookStruct.pt);
                            handler.BeginInvoke(this, eventArgs, null, null);
                        }
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, lParam);
        }
    }
}
