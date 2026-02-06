using System;
using SmartContextMenu.Extensions;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    public class KeyboardShortcut : ICloneable
    {
        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public KeyboardShortcut()
        {
            Key1 = VirtualKeyModifier.None;
            Key2 = VirtualKeyModifier.None;
            Key3 = VirtualKey.None;
        }

        public object Clone() => MemberwiseClone();

        public override string ToString()
        {
            var combination = string.Empty;

            if (Key1 != VirtualKeyModifier.None)
            {
                combination = Key1.GetDescription();
            }

            if (Key2 != VirtualKeyModifier.None)
            {
                combination += string.IsNullOrEmpty(combination) ? Key2.GetDescription() : "+" + Key2.GetDescription();
            }

            if (Key3 != VirtualKey.None)
            {
                combination += string.IsNullOrEmpty(combination) ? Key3.GetDescription() : "+" + Key3.GetDescription();
            }
            else
            {
                combination = string.Empty;
            }

            return combination;
        }
    }
}
