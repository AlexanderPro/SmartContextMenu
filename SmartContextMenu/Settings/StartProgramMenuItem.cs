using System;

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

        public KeyboardShortcut Shortcut { get; set; }

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
            Shortcut = new KeyboardShortcut();
        }

        public object Clone()
        {
            var menuItemClone = (StartProgramMenuItem)MemberwiseClone();
            menuItemClone.Shortcut = (KeyboardShortcut)Shortcut.Clone();
            return menuItemClone;
        }
    }
}
