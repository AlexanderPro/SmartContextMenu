using System;
using SmartContextMenu.Native.Structs;

namespace SmartContextMenu.Hooks
{
    class MouseEventArgs : EventArgs
    {
        public Point Point { get; }

        public bool Hooked { get; }

        public MouseEventArgs(Point point, bool hooked)
        {
            Point = point;
            Hooked = hooked;
        }
    }
}
