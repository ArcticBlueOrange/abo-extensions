namespace AboExtensions.Nullables;

public static class NullableExtensions
{
    public static void IfNotNull<T>(this T? value, Action<T> action) where T : class
    {
        if (value != null) action(value);
    }
    public static void IfNotNull<T>(this T? value, Action<T> action) where T : struct
    {
        if (value.HasValue) action(value.Value);
    }

    public static R? MapNotNull<T, R>(this T? value, Func<T, R> map)
    {
        if (value != null)
            return map(value);
        return default;
    }
}
