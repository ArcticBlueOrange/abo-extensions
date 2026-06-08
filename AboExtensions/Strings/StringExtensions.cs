using System.Diagnostics.CodeAnalysis;

namespace AboExtensions.Strings;

public static class StringExtensions
{
    /// <summary>
    /// Shortcut for string.IsNullOrWhiteSpace(s)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
        string.IsNullOrWhiteSpace(s);
    /// <summary>
    /// Shortcut for !string.IsNullOrWhiteSpace(s)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrWhiteSpace(s);
    /// <summary>
    /// Shortcut for string.IsNullOrEmpty(s)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? s) =>
        string.IsNullOrEmpty(s);
    /// <summary>
    /// Shortcut for !string.IsNullOrEmpty(s)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrEmpty(s);
    /// <summary>
    /// Trims the same characters  at the start and end
    /// </summary>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string TrimStartEnd(this string s, char c = ' ') =>
        s.TrimStart(c).TrimEnd(c);
    /// <summary>
    /// Extension method for string.Join()
    /// </summary>
    /// <param name="s"></param>
    /// <param name="sep"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Removes characters outside of the string 'only'
    /// </summary>
    /// <param name="s"></param>
    /// <param name="only"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Removes not-numerical chars
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string NumOnly(this string s) => s.CharOnly("0123456789");

    /// <summary>
    /// True if the string is a valid number
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNumeric(this string text) => double.TryParse(text, out _);
    /// <summary>
    /// Crops a string and adds "..." at the end
    /// </summary>
    /// <param name="s"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static string Ellipsify(this string s, int max = 4)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        if (max < 4 && s.Length > 3)
            return "...";
        return s.Length <= max ? s : s[..(max - 3)] + "...";
    }
    /// <summary>
    /// Remove first character if equals to c
    /// </summary>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string RemoveFirstChar(this string s, char c)
    {
        if (s.StartsWith(c)) return s[1..];
        return s;
    }
    public static string Capitalize(this string s, bool keepOthers = false) =>
        string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + (keepOthers ? s[1..] : s[1..].ToLower());
    public static string ToSlug(this string s)
    {
        var result = new System.Text.StringBuilder();
        bool prevDash = false;
        foreach (var c in s.ToLower())
        {
            if (char.IsLetterOrDigit(c))
            {
                result.Append(c);
                prevDash = false;
            }
            else if (!prevDash && result.Length > 0)
            {
                result.Append('-');
                prevDash = true;
            }
        }
        if (result.Length > 0 && result[^1] == '-')
            result.Length--;
        return result.ToString();
    }
    public static string Repeat(this string s, int n) =>
        n <= 0 ? string.Empty : string.Concat(Enumerable.Repeat(s, n));
    public static string Left(this string s, int n) =>
        string.IsNullOrEmpty(s) ? s : s[..Math.Min(n, s.Length)];
    public static string Right(this string s, int n) =>
        string.IsNullOrEmpty(s) ? s : s[Math.Max(0, s.Length - n)..];
    public static string ToPascalCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        var result = new System.Text.StringBuilder();
        bool nextUpper = true;
        foreach (var c in s)
        {
            if (!char.IsLetterOrDigit(c)) { nextUpper = true; continue; }
            result.Append(nextUpper ? char.ToUpper(c) : char.ToLower(c));
            nextUpper = false;
        }
        return result.ToString();
    }
    public static string ToCamelCase(this string s)
    {
        var pascal = s.ToPascalCase();
        if (string.IsNullOrEmpty(pascal)) return pascal;
        return char.ToLower(pascal[0]) + pascal[1..];
    }
    /// <summary>
    /// Simple valid email checker (not regex)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsEmail(this string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return false;
        var at = s.IndexOf('@');
        if (at <= 0 || at == s.Length - 1) return false;
        var dot = s.LastIndexOf('.');
        return dot > at + 1 && dot < s.Length - 1;
    }
}
