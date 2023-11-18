using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading;
using System.Reflection;
using SmartContextMenu.Utils;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Settings
{
    class ApplicationSettingsFile
    {
        public static FileInfo GetCurrentDirectoryFile()
        {
            var fileName = Path.Combine(AssemblyUtils.AssemblyDirectory, $"{AssemblyUtils.AssemblyTitle}.xml");
            return new FileInfo(fileName);
        }

        public static FileInfo GetProfileFile()
        {
            var directoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AssemblyUtils.AssemblyTitle, AssemblyUtils.AssemblyProductVersion);
            var fileName = Path.Combine(directoryName, $"{AssemblyUtils.AssemblyTitle}.xml");
            return new FileInfo(fileName);
        }

        public static ApplicationSettings ReadFromProfileFile()
        {
            var fileInfo = GetProfileFile();
            using var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Read(stream);
        }

        public static ApplicationSettings ReadFromCurrentDirectoryFile()
        {
            var fileInfo = GetCurrentDirectoryFile();
            using var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Read(stream);
        }

        public static ApplicationSettings ReadFromApplicationResources()
        {
            var resourceName = $"SmartContextMenu.{AssemblyUtils.AssemblyTitle}.xml";
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            return Read(stream);
        }

        public static void SaveToProfileFile(ApplicationSettings settings)
        {
            var fileInfo = GetProfileFile();
            if (!Directory.Exists(fileInfo.Directory.FullName))
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            Save(fileInfo.FullName, settings);
        }

        public static void SaveToCurrentDirectoryFile(ApplicationSettings settings)
        {
            var fileInfo = GetCurrentDirectoryFile();
            Save(fileInfo.FullName, settings);
        }

        private static ApplicationSettings Read(Stream stream)
        {
            var settings = new ApplicationSettings();
            var document = XDocument.Load(stream);

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

            var hotKeysElement = document.XPathSelectElement("/smartSystemMenu/hotKeys");
            settings.Key1 = hotKeysElement.Attribute("key1") != null && !string.IsNullOrEmpty(hotKeysElement.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(hotKeysElement.Attribute("key1").Value) : VirtualKeyModifier.None;
            settings.Key2 = hotKeysElement.Attribute("key2") != null && !string.IsNullOrEmpty(hotKeysElement.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(hotKeysElement.Attribute("key2").Value) : VirtualKeyModifier.None;
            settings.Key3 = hotKeysElement.Attribute("key3") != null && !string.IsNullOrEmpty(hotKeysElement.Attribute("key3").Value) ? (VirtualKey)int.Parse(hotKeysElement.Attribute("key3").Value) : VirtualKey.None;
            settings.Key4 = hotKeysElement.Attribute("key4") != null && !string.IsNullOrEmpty(hotKeysElement.Attribute("key4").Value) ? (VirtualKey)int.Parse(hotKeysElement.Attribute("key4").Value) : VirtualKey.None;
            settings.MouseButton = hotKeysElement.Attribute("mouseButton") != null && !string.IsNullOrEmpty(hotKeysElement.Attribute("mouseButton").Value) ? (MouseButton)int.Parse(hotKeysElement.Attribute("mouseButton").Value) : MouseButton.None;

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

        private static void Save(string fileName, ApplicationSettings settings)
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
                                 new XElement("hotKeys",
                                     new XAttribute("key1", ((int)settings.Key1).ToString()),
                                     new XAttribute("key2", ((int)settings.Key2).ToString()),
                                     new XAttribute("key3", ((int)settings.Key3).ToString()),
                                     new XAttribute("key4", ((int)settings.Key4).ToString()),
                                     new XAttribute("mouseButton", ((int)settings.MouseButton).ToString())
                                 ),
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
