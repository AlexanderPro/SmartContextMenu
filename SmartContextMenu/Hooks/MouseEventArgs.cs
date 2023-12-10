using System;
using SmartContextMenu.Native.Structs;

namespace SmartContextMenu.Hooks
{
    class MouseEventArgs : EventArgs
    {
        public Point Point { get; }

        public MouseEventArgs(Point point)
        {
            Point = point;
        }
    }
}
