namespace AboExtensions.Objects;

public static class ObjectExtensions
{
    public static bool IsNull(this object? o) => o == null;

    public static bool IsNotNull(this object? o) => o != null;

    // TODO: Also<T>(this T o, Action<T> action) : T
    //   Descrizione: esegue action sull'oggetto e restituisce l'oggetto stesso per il chaining.
    //   Utile per side-effect (logging, mutazione) dentro una pipeline fluente.
    //   Esempi: user.Also(u => logger.Log(u.Name)).Save()
    //           new List<int>().Also(l => l.Add(1)).Also(l => l.Add(2))

    // TODO: Let<T, R>(this T o, Func<T, R> map) : R
    //   Descrizione: applica map all'oggetto e restituisce il risultato.
    //   Equivale a map(o) ma leggibile come estensione fluente.
    //   Esempi: "42".Let(int.Parse)          → 42
    //           user.Let(u => u.Name.Trim())  → "Mario"

    // TODO: In<T>(this T o, params T[] values) : bool
    //   Descrizione: true se o è uguale ad almeno uno dei valori.
    //   Alternativa leggibile a o == a || o == b || o == c.
    //   Esempi: status.In(Active, Pending)       → true se status è uno dei due
    //           "x".In("a", "b", "c")            → false

    // TODO: NotIn<T>(this T o, params T[] values) : bool
    //   Descrizione: complementare di In — true se o non è presente in values.
    //   Esempi: status.NotIn(Deleted, Archived)  → true se status è diverso da entrambi
}
