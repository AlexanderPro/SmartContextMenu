using System.Runtime.InteropServices;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Native
{
    static class SHCore
    {
        [DllImport("SHCore.dll", SetLastError = true)]
        public static extern bool SetProcessDpiAwareness(ProcessDpiAwareness processDpiAwareness);
    }
}
