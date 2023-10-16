using System.Linq;

namespace Vip.ElginTEF.Extensions;

internal static class StringExtensions
{
    public static string TrimVip(this string value)
    {
        return value?.Trim() ?? string.Empty;
    }

    public static bool IsNullOrEmpty(this string value)
    {
        value = value.TrimVip();
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotNullOrEmpty(this string value)
    {
        return !value.IsNullOrEmpty();
    }

    public static string OnlyNumbers(this string value)
    {
        return value.TrimVip().IsNullOrEmpty()
            ? string.Empty
            : new string(value.Where(char.IsDigit).ToArray());
    }

    public static decimal ToDecimal(this string value, decimal valueDefault = 0)
    {
        value = value.Replace("R$", "").Replace("%", "").Replace(" ", "");
        decimal.TryParse(value, out var result);
        return result != 0 ? result : valueDefault;
    }

    public static int ToInt(this string value, int valueDefault = 0)
    {
        value = value.OnlyNumbers();
        int.TryParse(value, out var result);
        return result != 0 ? result : valueDefault;
    }
}