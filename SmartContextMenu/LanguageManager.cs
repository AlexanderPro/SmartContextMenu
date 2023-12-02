using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SmartContextMenu
{
    class LanguageManager
    {
        private readonly IDictionary<string, string> _values;

        public LanguageManager(string languageName)
        {
            _values = Parse(languageName);
        }

        public string GetString(string name) => _values.TryGetValue(name, out var value) ? value : string.Empty;

        private IDictionary<string, string> Parse(string languageName)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SmartContextMenu.Language.xml");
            var document = XDocument.Load(stream);
            return document
                .XPathSelectElements($"/language/items/{languageName}/item")
                .Select(x => new
                {
                    Name = x.Attribute("name") != null ? x.Attribute("name").Value : string.Empty,
                    Value = x.Attribute("value") != null ? x.Attribute("value").Value : string.Empty,
                })
                .ToDictionary(x => x.Name, y => y.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
