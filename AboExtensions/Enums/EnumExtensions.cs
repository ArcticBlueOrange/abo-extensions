namespace AboExtensions.Enums;

public static class EnumExtensions
{
    public static IEnumerable<T> GetValues<T>() where T : struct, Enum =>
        Enum.GetValues<T>();
}
