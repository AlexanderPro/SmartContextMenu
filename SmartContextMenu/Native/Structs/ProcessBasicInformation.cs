using System;
using System.Runtime.InteropServices;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct ProcessBasicInformation
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }
}
