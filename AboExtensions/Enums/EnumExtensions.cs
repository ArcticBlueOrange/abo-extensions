namespace AboExtensions.Enums;

public static class EnumExtensions
{
    public static IEnumerable<T> GetValues<T>() where T : struct, Enum =>
        Enum.GetValues<T>();

    // TODO: Next<T>(this T value, bool wrap = true) : T  where T : struct, Enum
    //   Descrizione: restituisce il valore successivo nell'enum. Se wrap = true e il valore
    //   è l'ultimo, ritorna il primo; se wrap = false lancia InvalidOperationException.
    //   Esempi: DayOfWeek.Monday.Next() → DayOfWeek.Tuesday
    //           DayOfWeek.Saturday.Next() → DayOfWeek.Sunday
    //           DayOfWeek.Saturday.Next(wrap: false) → throw InvalidOperationException

    // TODO: Previous<T>(this T value, bool wrap = true) : T  where T : struct, Enum
    //   Descrizione: restituisce il valore precedente nell'enum. Se wrap = true e il valore
    //   è il primo, ritorna l'ultimo; se wrap = false lancia InvalidOperationException.
    //   Esempi: DayOfWeek.Wednesday.Previous() → DayOfWeek.Tuesday
    //           DayOfWeek.Sunday.Previous() → DayOfWeek.Saturday
    //           DayOfWeek.Sunday.Previous(wrap: false) → throw InvalidOperationException

    // TODO: GetDescription<T>(this T value) : string?  where T : struct, Enum
    //   Descrizione: legge il valore dell'attributo [Description("...")] applicato al membro
    //   dell'enum. Ritorna null se l'attributo non è presente.
    //   Richiede: using System.ComponentModel;
    //   Esempi: (dato enum Status { [Description("In Progress")] Running, Done })
    //           Status.Running.GetDescription() → "In Progress"
    //           Status.Done.GetDescription() → null
}
