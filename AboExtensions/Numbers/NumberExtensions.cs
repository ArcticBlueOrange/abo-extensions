using AboExtensions.Strings;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

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

    public static bool IsEven(this int i) => (i & 1) == 0;

    public static bool IsOdd(this int i) => (i & 1) != 0;

    /// <summary>
    /// Returns the number of digits of the int 
    /// (ignoring sign, and with arbitrary bases)
    /// </summary>
    /// <param name="n"></param>
    /// <param name="base">Default 10</param>
    /// <returns></returns>
    public static int Digits(this int n, int @base = 10)
    {
        n = n.Abs();
        int d = 0;

        do
        {
            n /= @base;
            d++;
        } while (n != 0);

        return d;
    }

    /// <summary>
    /// Converts an int into a roman numeral
    /// </summary>
    /// <param name="i"> Must be between 1 and 3999</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string RomanEncode(this int i)
    {
        if (i < 1 || i > 3999)
            throw new ArgumentOutOfRangeException("i must be between 1 and 3999");

        var ret = new StringBuilder();
        var divisors = new Dictionary<int, string>()
        {
            [1000] = "M",
            [900] = "CM",
            [500] = "D",
            [400] = "CD",
            [100] = "C",
            [90] = "XC",
            [50] = "L",
            [40] = "XL",
            [10] = "X",
            [9] = "IX",
            [5] = "V",
            [4] = "IV",
            [1] = "I",
        };

        foreach (var d in divisors)
        {
            while (i >= d.Key)
            {
                i -= d.Key;
                ret.Append(d.Value);
            }
        }

        return ret.ToString();
    }

    /// <summary>
    /// Convrts a string of roman numerals into an integer
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int RomanDecode(this string s)
    {
        var td = s.ToUpper().CharOnly("MCDXLIV");
        if (td.Length != s.Length || td.IsNullOrWhiteSpace())
            throw new FormatException("Invalid String");
        var cum = 0;
        var divisors = new Dictionary<int, string>()
        {
            [1000] = "M",
            [900] = "CM",
            [500] = "D",
            [400] = "CD",
            [100] = "C",
            [90] = "XC",
            [50] = "L",
            [40] = "XL",
            [10] = "X",
            [9] = "IX",
            [5] = "V",
            [4] = "IV",
            [1] = "I",
        };
        var counters = new Dictionary<int, int>()
        {
            [1000] = 3,
            [900] = 1,
            [500] = 1,
            [400] = 1,
            [100] = 3,
            [90] = 1,
            [50] = 1,
            [40] = 1,
            [10] = 3,
            [9] = 1,
            [5] = 1,
            [4] = 1,
            [1] = 3,

        };

        foreach (var d in divisors)
        {
            var cnt = 0;
            while (td.StartsWith(d.Value))
            {
                cum += d.Key;
                td = td[d.Value.Length..];
                cnt++;
                if (cnt > counters[d.Key])
                    throw new FormatException("Invalid Roman Numeral");
            }
        }

        return cum;
    }

    public static string ToOrdinal(this int i)
    {
        var s = $"{i}";
        if (s.EndsWith("11")) s += "th";
        else if (s.EndsWith("12")) s += "th";
        else if (s.EndsWith("13")) s += "th";
        else if (s.EndsWith('1')) s += "st";
        else if (s.EndsWith('2')) s += "nd";
        else if (s.EndsWith('3')) s += "rd";
        else s += "th";
        return s;
    }
}

