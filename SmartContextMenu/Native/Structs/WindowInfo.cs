using System.Runtime.InteropServices;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct WindowInfo
    {
        public int cbSize;
        public Rect rcWindow;
        public Rect rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public int cxWindowBorders;
        public int cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;
    }
}
