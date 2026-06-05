namespace AboExtensions.Lists;

public static class ListExtensions
{
    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list) => list == null || !list.Any();
}
