namespace AboExtensions.TimeSpans;

public static class TimeSpanExtensions
{
    // TODO: IsZero(this TimeSpan ts) : bool
    //   Descrizione: true se il TimeSpan è esattamente zero.
    //   Esempi: TimeSpan.Zero.IsZero()              → true
    //           TimeSpan.FromSeconds(1).IsZero()    → false

    // TODO: Ago(this TimeSpan ts) : DateTime
    //   Descrizione: restituisce DateTime.Now - ts. Utile per espressioni leggibili.
    //   Esempi: TimeSpan.FromHours(2).Ago()   → DateTime.Now.AddHours(-2)
    //           TimeSpan.FromDays(1).Ago()    → ieri alla stessa ora

    // TODO: FromNow(this TimeSpan ts) : DateTime
    //   Descrizione: restituisce DateTime.Now + ts.
    //   Esempi: TimeSpan.FromMinutes(30).FromNow() → DateTime.Now.AddMinutes(30)

    // TODO: ToReadableString(this TimeSpan ts) : string
    //   Descrizione: formatta il TimeSpan in linguaggio naturale, mostrando solo le
    //   unità significative (ignora le unità a zero tranne i secondi come fallback).
    //   Usa abbreviazioni: d, h, m, s.
    //   Esempi: TimeSpan.FromHours(2.5).ToReadableString()           → "2h 30m"
    //           TimeSpan.FromDays(1).ToReadableString()              → "1d"
    //           TimeSpan.FromSeconds(45).ToReadableString()          → "45s"
    //           TimeSpan.FromMinutes(90).ToReadableString()          → "1h 30m"
    //           TimeSpan.FromMilliseconds(500).ToReadableString()    → "0s"  (o "< 1s"?)
    //           TimeSpan.Zero.ToReadableString()                     → "0s"
    //   Nota: decidere se gestire i millisecondi o truncare al secondo.
    //   Nota: decidere se gestire i TimeSpan negativi (es. "-2h 30m" o eccezione).
}
