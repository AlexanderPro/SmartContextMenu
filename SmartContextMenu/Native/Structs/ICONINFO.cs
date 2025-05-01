using System;
using System.Runtime.InteropServices;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct ICONINFO
    {
        public bool fIcon;
        public Int32 xHotspot;
        public Int32 yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
}
