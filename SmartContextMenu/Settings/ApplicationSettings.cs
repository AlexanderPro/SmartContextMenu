using System;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    public class ApplicationSettings : ICloneable
    {
        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public VirtualKey Key4 { get; set; }

        public MouseButton MouseButton { get; set; }

        public bool ShowSystemTrayIcon { get; set; }

        public bool EnableHighDPI { get; set; }

        public WindowSizerType Sizer { get; set; }

        public string LanguageName { get; set; }

        public MenuItems MenuItems { get; set; }

        public ApplicationSettings()
        {
            Key1 = VirtualKeyModifier.None;
            Key2 = VirtualKeyModifier.None;
            Key3 = VirtualKey.None;
            Key4 = VirtualKey.None;
            MouseButton = MouseButton.None;
            Sizer = WindowSizerType.WindowWithMargins;
            ShowSystemTrayIcon = true;
            EnableHighDPI = false;
            LanguageName = string.Empty;
            MenuItems = new MenuItems();
        }

        public object Clone()
        {
            var settings = new ApplicationSettings();

            foreach (var menuItem in MenuItems.WindowSizeItems)
            {
                settings.MenuItems.WindowSizeItems.Add(new WindowSizeMenuItem { Title = menuItem.Title, Width = menuItem.Width, Height = menuItem.Height });
            }

            foreach (var menuItem in MenuItems.StartProgramItems)
            {
                settings.MenuItems.StartProgramItems.Add(new StartProgramMenuItem { Title = menuItem.Title, FileName = menuItem.FileName, Arguments = menuItem.Arguments });
            }

            foreach (var menuItem in MenuItems.Items)
            {
                settings.MenuItems.Items.Add(new MenuItem { Name = menuItem.Name, Key1 = menuItem.Key1, Key2 = menuItem.Key2, Key3 = menuItem.Key3 });
            }

            settings.Key1 = Key1;
            settings.Key2 = Key2;
            settings.Key3 = Key3;
            settings.Key4 = Key4;
            settings.MouseButton = MouseButton;
            settings.Sizer = Sizer;
            settings.ShowSystemTrayIcon = ShowSystemTrayIcon;
            settings.EnableHighDPI = EnableHighDPI;
            settings.LanguageName = LanguageName;
            return settings;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Equals(other as ApplicationSettings);
        }

        public bool Equals(ApplicationSettings other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (MenuItems.WindowSizeItems.Count != other.MenuItems.WindowSizeItems.Count)
            {
                return false;
            }

            if (MenuItems.StartProgramItems.Count != other.MenuItems.StartProgramItems.Count)
            {
                return false;
            }

            if (MenuItems.Items.Count != other.MenuItems.Items.Count)
            {
                return false;
            }

            for (var i = 0; i < MenuItems.WindowSizeItems.Count; i++)
            {
                if (string.Compare(MenuItems.WindowSizeItems[i].Title, other.MenuItems.WindowSizeItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.WindowSizeItems[i].Left != other.MenuItems.WindowSizeItems[i].Left ||
                    MenuItems.WindowSizeItems[i].Top != other.MenuItems.WindowSizeItems[i].Top ||
                    MenuItems.WindowSizeItems[i].Width != other.MenuItems.WindowSizeItems[i].Width ||
                    MenuItems.WindowSizeItems[i].Height != other.MenuItems.WindowSizeItems[i].Height ||
                    MenuItems.WindowSizeItems[i].Key1 != other.MenuItems.WindowSizeItems[i].Key1 ||
                    MenuItems.WindowSizeItems[i].Key2 != other.MenuItems.WindowSizeItems[i].Key2 ||
                    MenuItems.WindowSizeItems[i].Key3 != other.MenuItems.WindowSizeItems[i].Key3)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.StartProgramItems.Count; i++)
            {
                if (string.Compare(MenuItems.StartProgramItems[i].Title, other.MenuItems.StartProgramItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].FileName, other.MenuItems.StartProgramItems[i].FileName, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].Arguments, other.MenuItems.StartProgramItems[i].Arguments, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].BeginParameter, other.MenuItems.StartProgramItems[i].BeginParameter, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].EndParameter, other.MenuItems.StartProgramItems[i].EndParameter, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.StartProgramItems[i].ShowWindow != other.MenuItems.StartProgramItems[i].ShowWindow ||
                    MenuItems.StartProgramItems[i].UseWindowWorkingDirectory != other.MenuItems.StartProgramItems[i].UseWindowWorkingDirectory)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.Items.Count; i++)
            {
                if (string.Compare(MenuItems.Items[i].Name, other.MenuItems.Items[i].Name, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.Items[i].Show != other.MenuItems.Items[i].Show ||
                    MenuItems.Items[i].Type != other.MenuItems.Items[i].Type ||
                    MenuItems.Items[i].Key1 != other.MenuItems.Items[i].Key1 ||
                    MenuItems.Items[i].Key2 != other.MenuItems.Items[i].Key2 ||
                    MenuItems.Items[i].Key3 != other.MenuItems.Items[i].Key3)
                {
                    return false;
                }

                if (MenuItems.Items[i].Items.Count != other.MenuItems.Items[i].Items.Count)
                {
                    return false;
                }

                for (var j = 0; j < MenuItems.Items[i].Items.Count; j++)
                {
                    if (string.Compare(MenuItems.Items[i].Items[j].Name, other.MenuItems.Items[i].Items[j].Name, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                        MenuItems.Items[i].Items[j].Show != other.MenuItems.Items[i].Items[j].Show ||
                        MenuItems.Items[i].Items[j].Type != other.MenuItems.Items[i].Items[j].Type ||
                        MenuItems.Items[i].Items[j].Key1 != other.MenuItems.Items[i].Items[j].Key1 ||
                        MenuItems.Items[i].Items[j].Key2 != other.MenuItems.Items[i].Items[j].Key2 ||
                        MenuItems.Items[i].Items[j].Key3 != other.MenuItems.Items[i].Items[j].Key3)
                    {
                        return false;
                    }
                }
            }

            if (Key1 != other.Key1 || Key2 != other.Key2 || Key3 != other.Key3 || Key4 != other.Key4 || MouseButton != other.MouseButton)
            {
                return false;
            }

            if (Sizer != other.Sizer)
            {
                return false;
            }

            if (ShowSystemTrayIcon != other.ShowSystemTrayIcon)
            {
                return false;
            }

            if (EnableHighDPI != other.EnableHighDPI)
            {
                return false;
            }

            if (string.Compare(LanguageName, other.LanguageName, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            foreach (var item in MenuItems.WindowSizeItems)
            {
                hashCode ^= item.Title.GetHashCode() ^ item.Left.GetHashCode() ^ item.Top.GetHashCode() ^ item.Width.GetHashCode() ^ item.Height.GetHashCode() ^ item.Key1.GetHashCode() ^ item.Key2.GetHashCode() ^ item.Key3.GetHashCode();
            }

            foreach (var item in MenuItems.StartProgramItems)
            {
                hashCode ^= item.Title.GetHashCode() ^ item.FileName.GetHashCode() ^ item.Arguments.GetHashCode() ^ item.UseWindowWorkingDirectory.GetHashCode() ^ item.BeginParameter.GetHashCode() ^ item.EndParameter.GetHashCode();
            }

            foreach (var item in MenuItems.Items)
            {
                hashCode ^= item.Show.GetHashCode() ^ item.Type.GetHashCode() ^  item.Name.GetHashCode() ^ item.Key1.GetHashCode() ^ item.Key2.GetHashCode() ^ item.Key3.GetHashCode();
                foreach (var subItem in item.Items)
                {
                    hashCode ^= subItem.Show.GetHashCode() ^ subItem.Type.GetHashCode() ^ subItem.Name.GetHashCode() ^ subItem.Key1.GetHashCode() ^ subItem.Key2.GetHashCode() ^ subItem.Key3.GetHashCode();
                }
            }

            hashCode ^= Key1.GetHashCode();
            hashCode ^= Key2.GetHashCode();
            hashCode ^= Key3.GetHashCode();
            hashCode ^= Key4.GetHashCode();
            hashCode ^= MouseButton.GetHashCode();
            hashCode ^= Sizer.GetHashCode();
            hashCode ^= LanguageName.GetHashCode();
            hashCode ^= ShowSystemTrayIcon.GetHashCode();
            hashCode ^= EnableHighDPI.GetHashCode();
            return hashCode;
        }       
    }
}
