using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboExtensions.Strings;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
        string.IsNullOrWhiteSpace(s);
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrWhiteSpace(s);
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? s) =>
        string.IsNullOrEmpty(s);
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrEmpty(s);
    public static string TrimStartEnd(this string s, char c = ' ') =>
        s.TrimStart(c).TrimEnd(c);
    public static string StringJoin(this IEnumerable<object> s, string sep) =>
        string.Join(sep, s);
    public static string? OrElse(this string? s, string? fallback, bool alsows = true)
    {
        if (s.IsNullOrEmpty())
            return fallback;

        if (alsows == true && s.IsNullOrWhiteSpace())
            return fallback;

        return s;
    }
    public static string CharOnly(this string? s, string only)
    {
        if (s == null)
            return "";

        var _out = "";
        foreach (var c in s)
            if (only.Contains(c))
                _out = $"{_out}{c}";

        return _out;
    }
    public static string NumOnly(this string s) => s.CharOnly("0123456789");

    public static bool IsNumeric(this string text) => double.TryParse(text, out _);
    public static string Ellipsify(this string s, int max = 4)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        if (max < 4 && s.Length > 3)
            return "...";
        return s.Length <= max ? s : s[..(max - 3)] + "...";
    }
    public static string RemoveFirstChar(this string s, char c)
    {
        if (s.StartsWith(c)) return s[1..];
        return s;
    }

}
