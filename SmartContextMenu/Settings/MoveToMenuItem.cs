using System;

namespace SmartContextMenu.Settings
{
    class MoveToMenuItem
    {
        public int MonitorId { get; }

        public IntPtr MonitorHandle { get; set; }

        public MoveToMenuItem(int monitorId, IntPtr monitorHandle)
        {
            MonitorId = monitorId;
            MonitorHandle = monitorHandle;
        }
    }
}
