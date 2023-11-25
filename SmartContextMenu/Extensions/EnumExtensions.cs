using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace SmartContextMenu.Extensions
{
    static class EnumExtensions
    {
        public static string GetDescription(this Enum value) => value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is not DescriptionAttribute attribute ? null : attribute.Description;

        public static IEnumerable<TEnum> AsEnumerable<TEnum>() where TEnum : IComparable, IConvertible, IFormattable => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
    }
}
