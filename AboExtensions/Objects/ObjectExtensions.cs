namespace AboExtensions.Objects;

public static class ObjectExtensions
{
    public static bool IsNull(this object? o) => o == null;

    public static bool IsNotNull(this object? o) => o != null;

    /// <summary>
    ///   Descrizione: esegue action sull'oggetto e restituisce l'oggetto stesso per il chaining.
    ///   Utile per side-effect (logging, mutazione) dentro una pipeline fluente.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="o"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static T Also<T>(this T o, Func<T, bool> predicate)
    {
        predicate(o);
        return o;
    }

    /// <summary>
    ///   Descrizione: applica map all'oggetto e restituisce il risultato.
    ///   Equivale a map(o) ma leggibile come estensione fluente.
    ///   Esempi: "42".Let(int.Parse)          → 42
    ///           user.Let(u => u.Name.Trim())  → "Mario"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="o"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public static R Let<T, R>(this T o, Func<T, R> map) => map(o);

    public static bool In<T>(this T o, params T[] array)
    {
        foreach (var t in array)
            if (EqualityComparer<T>.Default.Equals(o, t))
                return true;

        return false;
    }

    public static bool NotIn<T>(this T o, params T[] array)
    {
        foreach (var t in array)
            if (EqualityComparer<T>.Default.Equals(o, t))
                return false;

        return true;
    }
}
