using System;
using System.ComponentModel;
using System.Globalization;

namespace Vip.ElginTEF.Extensions;

internal static class EnumExtensions
{
    public static string GetDescription<T>(this T value) where T : struct, IConvertible
    {
        var type = value.GetType();
        if (!type.IsEnum)
            return string.Empty;

        var memberInfo = type.GetMember(value.ToString(CultureInfo.InstalledUICulture));
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length > 0) return ((DescriptionAttribute) attrs[0]).Description;
        }

        return value.ToString(CultureInfo.InstalledUICulture);
    }

    public static int ToInt<T>(this T value, int valueDefault = 0) where T : struct, IConvertible
    {
        var type = value.GetType();
        if (!type.IsEnum) return valueDefault;

        return (int) (object) value;
    }
}