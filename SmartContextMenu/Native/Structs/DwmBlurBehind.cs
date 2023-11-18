using System;
using System.Runtime.InteropServices;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct DwmBlurBehind
    {
        public DwmBB dwFlags;
        public bool fEnable;
        public IntPtr hRgnBlur;
        public bool fTransitionOnMaximized;
    }
}
