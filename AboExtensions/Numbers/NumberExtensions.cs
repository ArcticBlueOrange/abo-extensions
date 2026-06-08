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
}

