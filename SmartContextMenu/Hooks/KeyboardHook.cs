using System;
using System.Linq;
using SmartContextMenu.Settings;
using SmartContextMenu.Extensions;
using SmartContextMenu.Native.Structs;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.Constants;


namespace SmartContextMenu.Hooks
{
    class KeyboardHook : IDisposable
    {
        private readonly string _moduleName;
        private readonly KeyboardHookProc _hookProc;
        private IntPtr _hookHandle;

        public event EventHandler<KeyboardEventArgs> MenuItemHooked;
        public event EventHandler<KeyboardEventArgs> WindowSizeMenuItemHooked;
        public event EventHandler<KeyboardEventArgs> StartProgramMenuItemHooked;
        public event EventHandler<KeyboardEventArgs> MoveToHooked;
        public event EventHandler<KeyboardEventArgs> EscKeyHooked;

        public ApplicationSettings Settings { get; set; }

        public KeyboardHook(ApplicationSettings settings, string moduleName)
        {
            Settings = settings;
            _moduleName = moduleName;
            _hookProc = HookProc;
        }

        public bool Start()
        {
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
                    if ((int)Settings.NextMonitor.Key3 == lParam.vkCode)
                    {
                        var key1 = true;
                        var key2 = true;

                        if (Settings.NextMonitor.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)Settings.NextMonitor.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (Settings.NextMonitor.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)Settings.NextMonitor.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2)
                        {
                            var handler = MoveToHooked;
                            if (handler != null)
                            {
                                var eventArgs = new KeyboardEventArgs();
                                eventArgs.NextMonitor = true;
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    if ((int)Settings.PreviousMonitor.Key3 == lParam.vkCode)
                    {
                        var key1 = true;
                        var key2 = true;

                        if (Settings.PreviousMonitor.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)Settings.PreviousMonitor.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (Settings.PreviousMonitor.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)Settings.PreviousMonitor.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2)
                        {
                            var handler = MoveToHooked;
                            if (handler != null)
                            {
                                var eventArgs = new KeyboardEventArgs();
                                eventArgs.PreviousMonitor = true;
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    foreach (var item in Settings.MenuItems.Items.Flatten(x => x.Items).Where(x => x.Show && x.Type == MenuItemType.Item))
                    {
                        if (item.Shortcut.Key3 == VirtualKey.None || lParam.vkCode != (int)item.Shortcut.Key3)
                        {
                            continue;
                        }

                        var key1 = true;
                        var key2 = true;

                        if (item.Shortcut.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Shortcut.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Shortcut.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Shortcut.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2)
                        {
                            var handler = MenuItemHooked;
                            if (handler != null)
                            {
                                var eventArgs = new KeyboardEventArgs(item);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    foreach (var item in Settings.MenuItems.WindowSizeItems)
                    {
                        if (item.Shortcut.Key3 == VirtualKey.None || lParam.vkCode != (int)item.Shortcut.Key3)
                        {
                            continue;
                        }

                        var key1 = true;
                        var key2 = true;

                        if (item.Shortcut.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Shortcut.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Shortcut.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Shortcut.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2)
                        {
                            var handler = WindowSizeMenuItemHooked;
                            if (handler != null)
                            {
                                var eventArgs = new KeyboardEventArgs(item);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    foreach (var item in Settings.MenuItems.StartProgramItems)
                    {
                        if (item.Shortcut.Key3 == VirtualKey.None || lParam.vkCode != (int)item.Shortcut.Key3)
                        {
                            continue;
                        }

                        var key1 = true;
                        var key2 = true;

                        if (item.Shortcut.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Shortcut.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Shortcut.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Shortcut.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2)
                        {
                            var handler = StartProgramMenuItemHooked;
                            if (handler != null)
                            {
                                var eventArgs = new KeyboardEventArgs(item);
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
                        var handler = EscKeyHooked;
                        if (handler != null)
                        {
                            var eventArgs = new KeyboardEventArgs();
                            handler?.Invoke(this, eventArgs);
                            if (eventArgs.Succeeded)
                            {
                                return 1;
                            }
                        }
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, ref lParam);
        }
    }
}
