using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading;
using SmartContextMenu.Utils;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    public class SmartContextMenuSettings : ICloneable
    {
        public MenuItems MenuItems { get; private set; }

        public bool ShowSystemTrayIcon { get; private set; }

        public bool EnableHighDPI { get; set; }

        public WindowSizerType Sizer { get; set; }

        public string LanguageName { get; set; }

        public SmartContextMenuSettings()
        {
            MenuItems = new MenuItems();
            Sizer = WindowSizerType.WindowWithMargins;
            ShowSystemTrayIcon = true;
            EnableHighDPI = false;
            LanguageName = "";
        }

        public object Clone()
        {
            var settings = new SmartContextMenuSettings();

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

            return Equals(other as SmartContextMenuSettings);
        }

        public bool Equals(SmartContextMenuSettings other)
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

            hashCode ^= Sizer.GetHashCode();
            hashCode ^= LanguageName.GetHashCode();
            hashCode ^= ShowSystemTrayIcon.GetHashCode();
            hashCode ^= EnableHighDPI.GetHashCode();
            return hashCode;
        }

        public static SmartContextMenuSettings Read(string fileName, string languageFileName)
        {
            var settings = new SmartContextMenuSettings();
            var document = XDocument.Load(fileName);
            var languageDocument = XDocument.Load(languageFileName);

            settings.MenuItems.WindowSizeItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/windowSizeItems/item")
                .Select(x => new WindowSizeMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    Left = !string.IsNullOrEmpty(x.Attribute("left").Value) ? int.Parse(x.Attribute("left").Value) : (int?)null,
                    Top = !string.IsNullOrEmpty(x.Attribute("top").Value) ? int.Parse(x.Attribute("top").Value) : (int?)null,
                    Width = int.Parse(x.Attribute("width").Value),
                    Height = int.Parse(x.Attribute("height").Value),
                    Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                    Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                    Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None
                })
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItems/item")
                .Select(x => new StartProgramMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : "",
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : "",
                    BeginParameter = x.Attribute("beginParameter") != null ? x.Attribute("beginParameter").Value : "",
                    EndParameter = x.Attribute("endParameter") != null ? x.Attribute("endParameter").Value : "",
                    ShowWindow = x.Attribute("showWindow") == null || string.IsNullOrEmpty(x.Attribute("showWindow").Value) || x.Attribute("showWindow").Value.ToLower() == "true",
                    UseWindowWorkingDirectory = x.Attribute("useWindowWorkingDirectory") != null && !string.IsNullOrEmpty(x.Attribute("useWindowWorkingDirectory").Value) && x.Attribute("useWindowWorkingDirectory").Value.ToLower() == "true"
                })
                .ToList();

            settings.MenuItems.Items = document
                .XPathSelectElements("/smartSystemMenu/menuItems/items/item")
                .Select(x => {
                    var menuItem = new MenuItem
                    {
                        Name = x.Attribute("name") != null ? x.Attribute("name").Value : "",
                        Show = x.Attribute("show") == null || x.Attribute("show").Value.ToLower() != "false",
                        Type = x.Attribute("type") != null && !string.IsNullOrEmpty(x.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), x.Attribute("type").Value, true) : MenuItemType.Item,
                        Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                        Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                        Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None
                    };
                    menuItem.Items = menuItem.Type == MenuItemType.Group ?
                    x.XPathSelectElements("./items/item")
                    .Select(y => new MenuItem
                    {
                        Name = y.Attribute("name") != null ? y.Attribute("name").Value : "",
                        Show = y.Attribute("show") == null || y.Attribute("show").Value.ToLower() != "false",
                        Type = y.Attribute("type") != null && !string.IsNullOrEmpty(y.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), y.Attribute("type").Value, true) : MenuItemType.Item,
                        Key1 = y.Attribute("key1") != null && !string.IsNullOrEmpty(y.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(y.Attribute("key1").Value) : VirtualKeyModifier.None,
                        Key2 = y.Attribute("key2") != null && !string.IsNullOrEmpty(y.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(y.Attribute("key2").Value) : VirtualKeyModifier.None,
                        Key3 = y.Attribute("key3") != null && !string.IsNullOrEmpty(y.Attribute("key3").Value) ? (VirtualKey)int.Parse(y.Attribute("key3").Value) : VirtualKey.None
                    }).ToList() : new List<MenuItem>();
                    return menuItem;
                })
                .ToList();

            var sizerElement = document.XPathSelectElement("/smartSystemMenu/sizer");
            settings.Sizer = sizerElement.Attribute("type") != null && !string.IsNullOrEmpty(sizerElement.Attribute("type").Value) ? (WindowSizerType)int.Parse(sizerElement.Attribute("type").Value) : WindowSizerType.WindowWithMargins;

            var systemTrayIconElement = document.XPathSelectElement("/smartSystemMenu/systemTrayIcon");
            if (systemTrayIconElement != null && systemTrayIconElement.Attribute("show") != null && systemTrayIconElement.Attribute("show").Value != null && systemTrayIconElement.Attribute("show").Value.ToLower() == "false")
            {
                settings.ShowSystemTrayIcon = false;
            }

            var displayElement = document.XPathSelectElement("/smartSystemMenu/display");
            if (displayElement != null && displayElement.Attribute("highDPI") != null && displayElement.Attribute("highDPI").Value != null && displayElement.Attribute("highDPI").Value.ToLower() == "true")
            {
                settings.EnableHighDPI = true;
            }

            var languageElement = document.XPathSelectElement("/smartSystemMenu/language");
            var languageName = "";
            if (languageElement != null && languageElement.Attribute("name") != null && languageElement.Attribute("name").Value != null)
            {
                languageName = languageElement.Attribute("name").Value.ToLower().Trim();
                settings.LanguageName = languageName;
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "zh-CN"))
            {
                settings.LanguageName = "zh_cn";
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "zh-TW"))
            {
                settings.LanguageName = "zh_tw";
            }

            if (languageName == string.Empty && Thread.CurrentThread.CurrentCulture.Name == "ja-JP")
            {
                settings.LanguageName = "ja";
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "ko-KR" || Thread.CurrentThread.CurrentCulture.Name == "ko-KP"))
            {
                settings.LanguageName = "ko";
            }

            if (languageName == string.Empty && Thread.CurrentThread.CurrentCulture.Name == "ru-RU")
            {
                settings.LanguageName = "ru";
            }

            if (languageName == string.Empty && Thread.CurrentThread.CurrentCulture.Name == "de-DE")
            {
                settings.LanguageName = "de";
            }

            if (languageName == string.Empty && Thread.CurrentThread.CurrentCulture.Name == "fr-FR")
            {
                settings.LanguageName = "fr";
            }

            if (languageName == string.Empty && Thread.CurrentThread.CurrentCulture.Name == "hu-HU")
            {
                settings.LanguageName = "hu";
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "it-IT" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-SM" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-CH" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-VA"))
            {
                settings.LanguageName = "it";
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "pt-BR" || Thread.CurrentThread.CurrentCulture.Name == "pt-PT"))
            {
                settings.LanguageName = "pt";
            }

            if (languageName == string.Empty && (Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-BA" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-ME" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-RS" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-CS"))
            {
                settings.LanguageName = "sr";
            }

            if (languageName == string.Empty)
            {
                settings.LanguageName = "en";
            }

            return settings;
        }

        public static void Save(string fileName, SmartContextMenuSettings settings)
        {
            var document = new XDocument();
            document.Add(new XElement("smartSystemMenu",
                                 new XElement("menuItems",
                                     new XElement("items", settings.MenuItems.Items.Select(x => new XElement("item",
                                         new XAttribute("type", x.Type.ToString()),
                                         x.Type == MenuItemType.Item || x.Type == MenuItemType.Group ? new XAttribute("name", x.Name) : null,
                                         x.Show == false ? new XAttribute("show", x.Show.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString()) : null,
                                         x.Items.Any() ?
                                            new XElement("items", x.Items.Select(y => new XElement("item",
                                            new XAttribute("type", y.Type.ToString()),
                                            y.Type == MenuItemType.Item || y.Type == MenuItemType.Group ? new XAttribute("name", y.Name) : null,
                                            y.Show == false ? new XAttribute("show", y.Show.ToString().ToLower()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key1", y.Key1 == VirtualKeyModifier.None ? "" : ((int)y.Key1).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key2", y.Key2 == VirtualKeyModifier.None ? "" : ((int)y.Key2).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key3", y.Key3 == VirtualKey.None ? "" : ((int)y.Key3).ToString()) : null))) : null))),
                                     new XElement("windowSizeItems", settings.MenuItems.WindowSizeItems.Select(x => new XElement("item",
                                         new XAttribute("title", x.Title),
                                         new XAttribute("left", x.Left == null ? "" : x.Left.Value.ToString()),
                                         new XAttribute("top", x.Top == null ? "" : x.Top.Value.ToString()),
                                         new XAttribute("width", x.Width),
                                         new XAttribute("height", x.Height),
                                         new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()),
                                         new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()),
                                         new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString())))),
                                     new XElement("startProgramItems", settings.MenuItems.StartProgramItems.Select(x => new XElement("item",
                                         new XAttribute("title", x.Title),
                                         new XAttribute("fileName", x.FileName),
                                         new XAttribute("arguments", x.Arguments),
                                         new XAttribute("useWindowWorkingDirectory", x.UseWindowWorkingDirectory.ToString().ToLower()),
                                         new XAttribute("showWindow", x.ShowWindow.ToString().ToLower()),
                                         new XAttribute("beginParameter", x.BeginParameter),
                                         new XAttribute("endParameter", x.EndParameter))))),
                                 new XElement("sizer",
                                     new XAttribute("type", ((int)settings.Sizer).ToString())
                                 ),
                                 new XElement("systemTrayIcon",
                                     new XAttribute("show", settings.ShowSystemTrayIcon.ToString().ToLower())
                                 ),
                                 new XElement("display",
                                     new XAttribute("highDPI", settings.EnableHighDPI.ToString().ToLower())
                                 ),
                                 new XElement("language",
                                     new XAttribute("name", settings.LanguageName.ToLower())
                                 )));
            FileUtils.Save(fileName, document);
        }
    }
}
