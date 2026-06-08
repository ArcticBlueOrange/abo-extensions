namespace AboExtensions.Guids;

public static class GuidExtensions
{
    public static bool IsEmpty(this Guid g) => g == Guid.Empty;
    // TODO: IsEmpty(this Guid g) : bool
    //   Descrizione: true se il Guid è uguale a Guid.Empty (tutti zeri).
    //   Esempi: Guid.Empty.IsEmpty()      → true
    //           Guid.NewGuid().IsEmpty()  → false

    public static bool IsNotEmpty(this Guid g) => g != Guid.Empty;
    // TODO: IsNotEmpty(this Guid g) : bool
    //   Descrizione: true se il Guid non è Guid.Empty. Equivale a !IsEmpty().
    //   Esempi: Guid.NewGuid().IsNotEmpty() → true
    //           Guid.Empty.IsNotEmpty()     → false

    public static Guid OrNew(this Guid g) => g.IsEmpty() ? Guid.NewGuid() : g;
    // TODO: OrNew(this Guid g) : Guid
    //   Descrizione: restituisce il Guid stesso se non è Empty, altrimenti Guid.NewGuid().
    //   Utile per garantire un Guid valido senza if espliciti.
    //   Esempi: Guid.Empty.OrNew()      → un nuovo Guid casuale
    //           Guid.NewGuid().OrNew()  → il Guid originale invariato

    // TODO: ToShortString(this Guid g) : string
    //   Descrizione: converte il Guid in una rappresentazione compatta Base64 URL-safe
    //   (22 caratteri invece di 36), senza padding "==".
    //   Utile per URL, token, identificatori compatti.
    //   Esempi: Guid.Parse("d9428888-122b-11e1-b85c-61cd3cbb3210").ToShortString()
    //               → es. "iIhC2SsSEeG4XGHNPLsyEA"  (22 chars, varia per Guid)
    //   Nota: deve essere reversibile — considerare un FromShortString(string s) : Guid.
}
