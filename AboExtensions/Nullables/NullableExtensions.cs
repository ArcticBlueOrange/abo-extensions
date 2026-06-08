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

    // TODO: Map<T, R>(this T? value, Func<T, R> map) : R?  [T : class]
    //   Descrizione: applica map al valore se non è null, altrimenti restituisce null.
    //   Equivalente a un Select su nullable — stile funzionale, evita if espliciti.
    //   Overload anche per value types: T? → R?  [T : struct]
    //   Parametri: map — funzione di trasformazione da T a R.
    //   Esempi: "hello".Map(s => s.Length)        → 5
    //           ((string?)null).Map(s => s.Length) → null
    //           ((int?)42).Map(x => x * 2)         → 84
    //           ((int?)null).Map(x => x * 2)       → null
}
