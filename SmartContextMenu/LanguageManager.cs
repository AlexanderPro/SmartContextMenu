using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SmartContextMenu
{
    public class LanguageManager
    {
        private readonly static Dictionary<string, Dictionary<string, string>> _values = Parse();
        private readonly string _languageName;

        public LanguageManager(string languageName)
        {
            _languageName = languageName;
        }

        public string GetString(string name) => _values.TryGetValue(_languageName, out var dictionary) && dictionary.TryGetValue(name, out var value) ? value : string.Empty;

        private static Dictionary<string, Dictionary<string, string>> Parse()
        {
            var values = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SmartContextMenu.Language.xml");
            var document = XDocument.Load(stream);
            foreach (var languageElement in document.XPathSelectElements($"//languages/language"))
            {
                var languageValues = languageElement
                    .XPathSelectElements($"./item")
                    .Select(x => new
                {
                    Name = x.Attribute("name") != null ? x.Attribute("name").Value : string.Empty,
                    Value = x.Attribute("value") != null ? x.Attribute("value").Value : string.Empty,
                })
                .ToDictionary(x => x.Name, y => y.Value, StringComparer.OrdinalIgnoreCase);

                values.Add(languageElement.Attribute("name").Value, languageValues);
            }

            return values;
        }
    }
}
