using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Extensions
{
    static class PriorityExtensions
    {
        public static PriorityClass GetPriorityClass(this Priority priority) => priority switch
        {
            Priority.RealTime => PriorityClass.REALTIME_PRIORITY_CLASS,
            Priority.High => PriorityClass.HIGH_PRIORITY_CLASS,
            Priority.AboveNormal => PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS,
            Priority.Normal => PriorityClass.NORMAL_PRIORITY_CLASS,
            Priority.BelowNormal => PriorityClass.BELOW_NORMAL_PRIORITY_CLASS,
            Priority.Idle => PriorityClass.IDLE_PRIORITY_CLASS,
            _ => PriorityClass.NORMAL_PRIORITY_CLASS
        };
    }
}
