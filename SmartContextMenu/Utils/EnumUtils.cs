using System;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu.Utils
{
    static class EnumUtils
    {
        public static WindowAlignment GetAlignment(string menuItemName) => menuItemName switch
        {
            MenuItemName.AlignTopLeft => WindowAlignment.TopLeft,
            MenuItemName.AlignTopCenter => WindowAlignment.TopCenter,
            MenuItemName.AlignTopRight => WindowAlignment.TopRight,
            MenuItemName.AlignMiddleLeft => WindowAlignment.MiddleLeft,
            MenuItemName.AlignMiddleCenter => WindowAlignment.MiddleCenter,
            MenuItemName.AlignMiddleRight => WindowAlignment.MiddleRight,
            MenuItemName.AlignBottomLeft => WindowAlignment.BottomLeft,
            MenuItemName.AlignBottomCenter => WindowAlignment.BottomCenter,
            MenuItemName.AlignBottomRight => WindowAlignment.BottomRight,
            MenuItemName.AlignCenterHorizontally => WindowAlignment.CenterHorizontally,
            MenuItemName.AlignCenterVertically => WindowAlignment.CenterVertically,
            _ => throw new ArgumentException(nameof(menuItemName))
        };

        public static Priority GetPriority(string menuItemName) => menuItemName switch
        {
            MenuItemName.PriorityRealTime => Priority.RealTime,
            MenuItemName.PriorityHigh => Priority.High,
            MenuItemName.PriorityAboveNormal => Priority.AboveNormal,
            MenuItemName.PriorityNormal => Priority.Normal,
            MenuItemName.PriorityBelowNormal => Priority.BelowNormal,
            MenuItemName.PriorityIdle => Priority.Idle,
            _ => throw new ArgumentException(nameof(menuItemName))
        };

        public static string GetPriorityMenuItemName(Priority priority) => priority switch
        {
            Priority.RealTime => MenuItemName.PriorityRealTime,
            Priority.High => MenuItemName.PriorityHigh,
            Priority.AboveNormal => MenuItemName.PriorityAboveNormal,
            Priority.Normal => MenuItemName.PriorityNormal,
            Priority.BelowNormal => MenuItemName.PriorityBelowNormal,
            Priority.Idle => MenuItemName.PriorityIdle,
            _ => null
        };

        public static int GetTransparency(string menuItemName) => menuItemName switch
        {
            MenuItemName.TransparencyOpaque => 0,
            MenuItemName.Transparency10 => 10,
            MenuItemName.Transparency20 => 20,
            MenuItemName.Transparency30 => 30,
            MenuItemName.Transparency40 => 40,
            MenuItemName.Transparency50 => 50,
            MenuItemName.Transparency60 => 60,
            MenuItemName.Transparency70 => 70,
            MenuItemName.Transparency80 => 80,
            MenuItemName.Transparency90 => 90,
            MenuItemName.TransparencyInvisible => 100,
            _ => throw new ArgumentException(nameof(menuItemName))
        };

        public static string GetTransparencyMenuItemName(int transparency) => transparency switch
        {
            0 => MenuItemName.TransparencyOpaque,
            10 => MenuItemName.Transparency10,
            20 => MenuItemName.Transparency20,
            30 => MenuItemName.Transparency30,
            40 => MenuItemName.Transparency40,
            50 => MenuItemName.Transparency50,
            60 => MenuItemName.Transparency60,
            70 => MenuItemName.Transparency70,
            80 => MenuItemName.Transparency80,
            90 => MenuItemName.Transparency90,
            100 => MenuItemName.TransparencyInvisible,
            _ => null
        };
    }
}
