using System.Runtime.InteropServices;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }
}
