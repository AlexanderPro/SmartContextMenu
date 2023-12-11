using System;

namespace SmartContextMenu.Settings
{
    class MoveToMenuItem
    {
        public int MonitorIndex { get; }

        public IntPtr MonitorHandle { get; set; }

        public MoveToMenuItem(int monitorId, IntPtr monitorHandle)
        {
            MonitorIndex = monitorId;
            MonitorHandle = monitorHandle;
        }
    }
}
