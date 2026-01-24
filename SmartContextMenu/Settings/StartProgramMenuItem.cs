using System;
using SmartContextMenu.Extensions;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    public class StartProgramMenuItem : ICloneable
    {
        public const string PARAMETER_PROCESS_ID = "process_id";
        public const string PARAMETER_PROCESS_NAME = "process_name";
        public const string PARAMETER_WINDOW_TITLE = "window_title";

        public MenuItemType Type { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public bool UseWindowWorkingDirectory { get; set; }

        public bool ShowWindow { get; set; }

        public string BeginParameter { get; set; }

        public string EndParameter { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public StartProgramMenuItem()
        {
            Type = MenuItemType.Item;
            Title = string.Empty;
            FileName = string.Empty;
            Arguments = string.Empty;
            UseWindowWorkingDirectory = false;
            ShowWindow = true;
            BeginParameter = string.Empty;
            EndParameter = string.Empty;
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
