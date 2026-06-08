namespace AboExtensions.Lists;

public static class ListExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source) action(item);
    }
    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list) => list == null || !list.Any();
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
        var batch = new List<T>(size);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == size)
            {
                yield return batch;
                batch = new List<T>(size);
            }
        }
        if (batch.Count > 0)
            yield return batch;
    }
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        var result = list.ToList();
        var rng = new Random();
        for (int i = result.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (result[i], result[j]) = (result[j], result[i]);
        }
        return result;
    }

    // TODO: WhereNotNull<T>(this IEnumerable<T?> source) : IEnumerable<T>  [T : class]
    //   Descrizione: filtra i null dalla sequenza, il tipo di ritorno è non-nullable.
    //   Overload anche per value types: IEnumerable<T?> → IEnumerable<T>  [T : struct]
    //   Esempi: new[] { "a", null, "b" }.WhereNotNull() → ["a", "b"]
    //           new int?[] { 1, null, 3 }.WhereNotNull() → [1, 3]

    // TODO: Second<T>(this IEnumerable<T> source) : T
    //   Descrizione: restituisce il secondo elemento; lancia InvalidOperationException se la sequenza ha meno di 2 elementi.
    //   Esempi: new[] { 1, 2, 3 }.Second() → 2
    //           new[] { 1 }.Second() → throw InvalidOperationException

    // TODO: SecondOrDefault<T>(this IEnumerable<T> source, T defaultValue = default) : T
    //   Descrizione: restituisce il secondo elemento oppure defaultValue se la sequenza ha meno di 2 elementi.
    //   Esempi: new[] { 1, 2, 3 }.SecondOrDefault() → 2
    //           new[] { 1 }.SecondOrDefault() → 0
    //           new[] { 1 }.SecondOrDefault(-1) → -1

    // TODO: Flatten<T>(this IEnumerable<IEnumerable<T>> source) : IEnumerable<T>
    //   Descrizione: appiattisce una sequenza di sequenze in una singola sequenza.
    //   Esempi: new[] { new[] {1,2}, new[] {3,4} }.Flatten() → [1, 2, 3, 4]
    //           Enumerable.Empty<IEnumerable<int>>().Flatten() → []
}
