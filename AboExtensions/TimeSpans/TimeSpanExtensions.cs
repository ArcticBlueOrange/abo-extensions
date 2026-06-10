namespace AboExtensions.TimeSpans;

public static class TimeSpanExtensions
{
    public static bool IsZero(this TimeSpan value) => value == TimeSpan.Zero;

    public static DateTime Ago(this TimeSpan value) => DateTime.Now - value;

    public static DateTime FromNow(this TimeSpan value) => DateTime.Now + value;

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
