namespace AboExtensions.Numbers;

public static class NumberExtensions
{
    public static float Or(this float? f, float o = 0) => f ?? o;
    public static double Round(this double d, int dec) => Math.Round(d, dec);
    public static decimal Round(this decimal d, int dec) => Math.Round(d, dec);
    public static bool IsNanOrInf(this float f) => float.IsNaN(f) || float.IsInfinity(f);
    public static bool IsNotNanNorInf(this float f) => !f.IsNanOrInf();
    public static int Or(this int? i, int o = 0) => i ?? o;
    public static int Clamp(this int i, int min, int max) => Math.Clamp(i, min, max);
    public static double Clamp(this double d, double min, double max) => Math.Clamp(d, min, max);
    public static decimal Clamp(this decimal d, decimal min, decimal max) => Math.Clamp(d, min, max);
    public static double Percentage(this double part, double total) =>
        total == 0 ? 0 : part / total * 100;
    public static double Or(this double? d, double o = 0) => d ?? o;
    public static decimal Or(this decimal? d, decimal o = 0) => d ?? o;
    public static int Abs(this int i) => Math.Abs(i);
    public static double Abs(this double d) => Math.Abs(d);
    public static bool IsBetween(this int i, int min, int max, bool inclusive = true) =>
        inclusive ? i >= min && i <= max : i > min && i < max;
    public static bool IsBetween(this double d, double min, double max, bool inclusive = true) =>
        inclusive ? d >= min && d <= max : d > min && d < max;
    public static bool IsBetween(this decimal d, decimal min, decimal max, bool inclusive = true) =>
        inclusive ? d >= min && d <= max : d > min && d < max;

    // TODO: ToOrdinal(this int i) : string
    //   Descrizione: converte un intero nel corrispondente ordinale in inglese.
    //   Gestisce i casi speciali 11th, 12th, 13th.
    //   Esempi: 1.ToOrdinal() → "1st"
    //           2.ToOrdinal() → "2nd"
    //           3.ToOrdinal() → "3rd"
    //           4.ToOrdinal() → "4th"
    //           11.ToOrdinal() → "11th"
    //           21.ToOrdinal() → "21st"
}

