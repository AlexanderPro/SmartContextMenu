using System;
using System.Linq;
using SmartContextMenu.Native.Structs;
using SmartContextMenu.Settings;
using SmartContextMenu.Extensions;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.Constants;


namespace SmartContextMenu.Hooks
{
    class KeyboardHook : IDisposable
    {
        private readonly string _moduleName;
        private IntPtr _hookHandle;
        private KeyboardHookProc _hookProc;
        private MenuItems _menuItems;

        public event EventHandler<HotKeyEventArgs> Hooked;
        public event EventHandler<EventArgs> EscHooked;

        public KeyboardHook(string moduleName)
        {
            _moduleName = moduleName;
        }

        public bool Start(MenuItems menuItems)
        {
            _menuItems = menuItems;
            _hookProc = HookProc;
            var moduleHandle = GetModuleHandle(_moduleName);
            _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, moduleHandle, 0);
            return _hookHandle != IntPtr.Zero;
        }

        public bool Stop()
        {
            if (_hookHandle == IntPtr.Zero)
            {
                return true;
            }
            return UnhookWindowsHookEx(_hookHandle);
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

        ~KeyboardHook()
        {
            Dispose(false);
        }

        private int HookProc(int code, IntPtr wParam, ref KeyboardLLHookStruct lParam)
        {
            if (code == HC_ACTION)
            {
                if (wParam.ToInt32() == WM_KEYDOWN || wParam.ToInt32() == WM_SYSKEYDOWN)
                {
                    foreach (var item in _menuItems.Items.Flatten(x => x.Items).Where(x => x.Type == MenuItemType.Item))
                    {
                        var key1 = true;
                        var key2 = true;

                        if (item.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2 && lParam.vkCode == (int)item.Key3)
                        {
                            var handler = Hooked;
                            if (handler != null)
                            {
                                var eventArgs = new HotKeyEventArgs(item.Name);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    foreach (var item in _menuItems.WindowSizeItems)
                    {
                        var key1 = true;
                        var key2 = true;

                        if (item.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2 && lParam.vkCode == (int)item.Key3)
                        {
                            var handler = Hooked;
                            if (handler != null)
                            {
                                var eventArgs = new HotKeyEventArgs(item.Id);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    if (lParam.vkCode == (int)VirtualKey.VK_ESCAPE)
                    {
                        var handler = EscHooked;
                        handler?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, ref lParam);
        }
    }
}
