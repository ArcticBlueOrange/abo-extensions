namespace AboExtensions.Guards;

public static class GuardExtensions
{
    // Design: ogni metodo lancia l'eccezione appropriata se la condizione non è soddisfatta,
    // altrimenti restituisce il valore originale per permettere il chaining:
    //   var name = input.ThrowIfNull("input").ThrowIfNullOrEmpty("input");

    // TODO: ThrowIfNull<T>(this T? value, string? paramName = null) : T  where T : class
    //   Descrizione: lancia ArgumentNullException se value è null,
    //   altrimenti restituisce value (non-nullable) per il chaining.
    //   Esempi: ((string?)null).ThrowIfNull("name")  → throw ArgumentNullException
    //           "hello".ThrowIfNull("name")           → "hello"

    // TODO: ThrowIfNullOrEmpty(this string? s, string? paramName = null) : string
    //   Descrizione: lancia ArgumentException se la stringa è null o vuota.
    //   Esempi: "".ThrowIfNullOrEmpty("name")     → throw ArgumentException
    //           "hi".ThrowIfNullOrEmpty("name")   → "hi"

    // TODO: ThrowIfNullOrWhiteSpace(this string? s, string? paramName = null) : string
    //   Descrizione: lancia ArgumentException se la stringa è null, vuota o whitespace.
    //   Esempi: "   ".ThrowIfNullOrWhiteSpace("name")  → throw ArgumentException
    //           "hi".ThrowIfNullOrWhiteSpace("name")   → "hi"

    // TODO: ThrowIfNegative(this int i, string? paramName = null) : int
    //   Descrizione: lancia ArgumentOutOfRangeException se i < 0.
    //   Overload anche per double e decimal.
    //   Esempi: (-1).ThrowIfNegative("count") → throw ArgumentOutOfRangeException
    //           5.ThrowIfNegative("count")    → 5

    // TODO: ThrowIfZero(this int i, string? paramName = null) : int
    //   Descrizione: lancia ArgumentOutOfRangeException se i == 0.
    //   Overload anche per double e decimal.
    //   Esempi: 0.ThrowIfZero("divisor")  → throw ArgumentOutOfRangeException
    //           1.ThrowIfZero("divisor")  → 1

    // TODO: ThrowIfEmpty<T>(this IEnumerable<T>? source, string? paramName = null) : IEnumerable<T>
    //   Descrizione: lancia ArgumentException se la collezione è null o vuota.
    //   Esempi: Array.Empty<int>().ThrowIfEmpty("items") → throw ArgumentException
    //           new[]{1}.ThrowIfEmpty("items")           → [1]

    // TODO: ThrowIfOutOfRange<T>(this T value, T min, T max, string? paramName = null) : T
    //                            where T : IComparable<T>
    //   Descrizione: lancia ArgumentOutOfRangeException se value < min o value > max (inclusivo).
    //   Esempi: 15.ThrowIfOutOfRange(1, 10, "age") → throw ArgumentOutOfRangeException
    //           5.ThrowIfOutOfRange(1, 10, "age")  → 5

    // TODO: Must<T>(this T value, Func<T, bool> predicate, string message) : T
    //   Descrizione: lancia ArgumentException con message se predicate(value) è false,
    //   altrimenti restituisce value. Guard generico per condizioni personalizzate.
    //   Esempi: age.Must(x => x >= 18, "devi essere maggiorenne")
    //           name.Must(x => !x.Contains(" "), "nessuno spazio consentito")
}
