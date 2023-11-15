using System;
using System.Linq;
using System.ComponentModel;

namespace SmartContextMenu.Extensions
{
    static class EnumExtensions
    {
        public static string GetDescription(this Enum value) => value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is not DescriptionAttribute attribute ? null : attribute.Description;
    }
}
