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

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
    {
        foreach (var item in source)
            if (item != null)
                yield return item;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
    {
        foreach (var item in source)
            if (item.HasValue)
                yield return item.Value;
    }

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

    // TODO: Frequencies<T>(this IEnumerable<T> source) : Dictionary<T, int>
    //   Descrizione: restituisce un dizionario con il conteggio di ogni elemento distinto.
    //   Esempi: new[] { "a", "b", "a", "c", "b", "a" }.Frequencies()
    //               → { "a": 3, "b": 2, "c": 1 }
    //           new[] { 1, 1, 2 }.Frequencies() → { 1: 2, 2: 1 }

    // TODO: Paginate<T>(this IEnumerable<T> source, int page, int pageSize) : IEnumerable<T>
    //   Descrizione: restituisce gli elementi della pagina richiesta (1-based).
    //   Lancia ArgumentOutOfRangeException se page < 1 o pageSize < 1.
    //   Parametri: page — numero di pagina (da 1); pageSize — elementi per pagina.
    //   Esempi: new[] {1,2,3,4,5}.Paginate(1, 2) → [1, 2]
    //           new[] {1,2,3,4,5}.Paginate(2, 2) → [3, 4]
    //           new[] {1,2,3,4,5}.Paginate(3, 2) → [5]
    //           new[] {1,2,3,4,5}.Paginate(4, 2) → []

    // TODO: Window<T>(this IEnumerable<T> source, int size) : IEnumerable<IEnumerable<T>>
    //   Descrizione: produce finestre scorrevoli di dimensione fissa. L'ultima finestra
    //   viene emessa solo se completa (no padding).
    //   Lancia ArgumentOutOfRangeException se size < 1.
    //   Esempi: new[] {1,2,3,4}.Window(2) → [[1,2], [2,3], [3,4]]
    //           new[] {1,2,3}.Window(3)   → [[1,2,3]]
    //           new[] {1,2}.Window(3)     → []

    // TODO: Interleave<T>(this IEnumerable<T> source, IEnumerable<T> other) : IEnumerable<T>
    //   Descrizione: alterna elementi di source e other. Si ferma quando la sequenza
    //   più corta è esaurita.
    //   Esempi: new[] {1,3,5}.Interleave(new[] {2,4,6}) → [1,2,3,4,5,6]
    //           new[] {1,2}.Interleave(new[] {10,20,30}) → [1,10,2,20]
    //           new[] {1,2,3}.Interleave(new[] {10})     → [1,10]
}
