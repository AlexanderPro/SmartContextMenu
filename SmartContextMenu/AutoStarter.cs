using Microsoft.Win32;

namespace SmartContextMenu
{
    static class AutoStarter
    {
        private const string RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void Enable(string keyName, string assemblyLocation)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.SetValue(keyName, assemblyLocation);
        }

        public static void Disable(string keyName)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.DeleteValue(keyName);
        }

        public static bool IsEnabled(string keyName, string assemblyLocation)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);
            if (key == null)
            {
                return false;
            }
            var value = (string)key.GetValue(keyName);
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return (value == assemblyLocation);
        }
    }
}
