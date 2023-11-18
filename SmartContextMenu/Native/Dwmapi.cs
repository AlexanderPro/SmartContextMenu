using System;
using System.Runtime.InteropServices;
using SmartContextMenu.Native.Structs;

namespace SmartContextMenu.Native
{
    static class Dwmapi
    {
        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DwmBlurBehind blurBehind);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);
    }
}
