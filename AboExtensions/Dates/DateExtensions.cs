namespace AboExtensions.Dates;

public static class DateExtensions
{
    public static bool IsWeekend(this DateTime d) =>
        d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday;
    public static bool IsWeekday(this DateTime d) => !d.IsWeekend();
    public static DateTime StartOfDay(this DateTime d) => d.Date;
    public static DateTime EndOfDay(this DateTime d) => d.Date.AddDays(1).AddTicks(-1);
    public static DateTime StartOfWeek(this DateTime d, DayOfWeek startDay = DayOfWeek.Monday)
    {
        int diff = ((int)d.DayOfWeek - (int)startDay + 7) % 7;
        return d.Date.AddDays(-diff);
    }
    public static int Age(this DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
    public static bool IsInThePast(this DateTime d) => d < DateTime.Now;
    public static bool IsInTheFuture(this DateTime d) => d > DateTime.Now;
    public static int Quarter(this DateTime d) => (d.Month - 1) / 3 + 1;
    public static DateTime AddWorkdays(this DateTime d, int days)
    {
        int step = days < 0 ? -1 : 1;
        int remaining = Math.Abs(days);
        while (remaining > 0)
        {
            d = d.AddDays(step);
            if (d.IsWeekday()) remaining--;
        }
        return d;
    }
    public static DateTime NextWeekday(this DateTime d, DayOfWeek day)
    {
        int diff = ((int)day - (int)d.DayOfWeek + 7) % 7;
        return d.Date.AddDays(diff == 0 ? 7 : diff);
    }

    // TODO: IsToday(this DateTime d) : bool
    //   Descrizione: true se la data coincide con oggi (confronta solo la parte Date).
    //   Esempi: DateTime.Today.IsToday()            → true
    //           DateTime.Today.AddDays(1).IsToday() → false

    // TODO: IsYesterday(this DateTime d) : bool
    //   Descrizione: true se la data coincide con ieri (confronta solo la parte Date).
    //   Esempi: DateTime.Today.AddDays(-1).IsYesterday() → true

    // TODO: IsTomorrow(this DateTime d) : bool
    //   Descrizione: true se la data coincide con domani (confronta solo la parte Date).
    //   Esempi: DateTime.Today.AddDays(1).IsTomorrow() → true

    // TODO: IsSameDay(this DateTime d, DateTime other) : bool
    //   Descrizione: true se d e other hanno la stessa data (anno, mese, giorno),
    //   ignorando la componente oraria.
    //   Esempi: new DateTime(2024,6,1,10,0,0).IsSameDay(new DateTime(2024,6,1,22,0,0)) → true
    //           new DateTime(2024,6,1).IsSameDay(new DateTime(2024,6,2)) → false

    // TODO: Elapsed(this DateTime d) : TimeSpan
    //   Descrizione: restituisce il tempo trascorso da d a DateTime.Now.
    //   Per date future il TimeSpan sarà negativo.
    //   Esempi: DateTime.Now.AddHours(-2).Elapsed() → circa TimeSpan(2, 0, 0)
    //           DateTime.Now.AddDays(1).Elapsed()   → TimeSpan negativo (~-24h)

    // TODO: ToUnixTimestamp(this DateTime d) : long
    //   Descrizione: converte la data in secondi Unix (secondi dall'epoch 1970-01-01 UTC).
    //   Tratta la data come UTC se Kind == Utc, altrimenti come locale.
    //   Esempi: new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToUnixTimestamp() → 0
    //           new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToUnixTimestamp() → 1704067200

    // TODO: Yesterday() : DateTime  [metodo statico su DateExtensions, non extension]
    //   Descrizione: restituisce la data di ieri a mezzanotte, analogo a DateTime.Today.
    //   Non è un extension method ma un metodo statico di convenienza, come DateTime.Today.
    //   Esempi: DateExtensions.Yesterday() → DateTime.Today.AddDays(-1)
    //   Nota: valutare se esporre anche come property statica invece che metodo.

    // TODO: Tomorrow() : DateTime  [metodo statico su DateExtensions, non extension]
    //   Descrizione: restituisce la data di domani a mezzanotte, analogo a DateTime.Today.
    //   Non è un extension method ma un metodo statico di convenienza, come DateTime.Today.
    //   Esempi: DateExtensions.Tomorrow() → DateTime.Today.AddDays(1)
    //   Nota: valutare se esporre anche come property statica invece che metodo.

    // TODO: WeekNumber(this DateTime d) : int
    //   Descrizione: restituisce il numero di settimana ISO 8601 (1–53).
    //   La settimana ISO inizia il lunedì; la prima settimana dell'anno è quella con il primo giovedì.
    //   Parametri: nessuno oltre alla data.
    //   Esempi: new DateTime(2024, 1, 1).WeekNumber() → 1
    //           new DateTime(2024, 12, 30).WeekNumber() → 1  (appartiene alla settimana 1 del 2025)
    //   Nota: usare ISOWeek.GetWeekOfYear() disponibile in .NET 5+.
}
