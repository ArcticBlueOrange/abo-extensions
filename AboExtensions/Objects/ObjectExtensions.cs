namespace AboExtensions.Objects;

public static class ObjectExtensions
{
    public static bool IsNull(this object? o) => o == null;
    // TODO: IsNull(this object? o) : bool
    //   Descrizione: restituisce true se l'oggetto è null. Alternativa leggibile a == null.
    //   Esempi: ((string?)null).IsNull() → true
    //           "hello".IsNull() → false

    public static bool IsNotNull(this object? o) => o != null;
    // TODO: IsNotNull(this object? o) : bool
    //   Descrizione: restituisce true se l'oggetto non è null. Alternativa leggibile a != null.
    //   Esempi: "hello".IsNotNull() → true
    //           ((string?)null).IsNotNull() → false
}
