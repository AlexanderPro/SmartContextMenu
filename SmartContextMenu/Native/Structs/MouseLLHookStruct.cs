using System;
using System.Runtime.InteropServices;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct MouseLLHookStruct
    {
        public Point pt;
        public int mouseData;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    delegate int MouseHookProc(int code, int wParam, IntPtr lParam);
}
