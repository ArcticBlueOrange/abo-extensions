namespace AboExtensions.Guids;

public static class GuidExtensions
{
    public static bool IsEmpty(this Guid g) => g == Guid.Empty;

    public static bool IsNotEmpty(this Guid g) => g != Guid.Empty;

    /// <summary>
    /// Returns the guid if not empty, or creates a new one
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public static Guid OrNew(this Guid g) => g.IsEmpty() ? Guid.NewGuid() : g;

    // TODO: ToShortString(this Guid g) : string
    //   Descrizione: converte il Guid in una rappresentazione compatta Base64 URL-safe
    //   (22 caratteri invece di 36), senza padding "==".
    //   Utile per URL, token, identificatori compatti.
    //   Esempi: Guid.Parse("d9428888-122b-11e1-b85c-61cd3cbb3210").ToShortString()
    //               → es. "iIhC2SsSEeG4XGHNPLsyEA"  (22 chars, varia per Guid)
    //   Nota: deve essere reversibile - considerare un FromShortString(string s) : Guid.
}
