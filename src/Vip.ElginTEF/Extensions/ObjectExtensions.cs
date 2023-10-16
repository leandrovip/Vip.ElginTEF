namespace Vip.ElginTEF.Extensions;

internal static class ObjectExtensions
{
    public static bool IsNull(this object value) => value == null;
    public static bool IsNotNull(this object value) => value != null;
    public static bool IsNullOrEmpty(this object value) => value.IsNull() || value.ToString().IsNullOrEmpty();
    public static bool IsNotNullOrEmpty(this object value) => !value.IsNullOrEmpty();
}