using System.Globalization;
using System.Text;

namespace AboExtensions.Chars;

public static class CharExtensions
{
    // TODO AGG. TEST
    private static readonly List<char> Vowels =
    [
        'a', 'e','i','o','u',
        'A', 'E','I','O','U'
    ];
    public static bool IsUnicodeLetter(this char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    public static bool IsVowel(this char c) => Vowels.Contains(c);
    public static bool IsConsonant(this char c) => c.IsUnicodeLetter() && !c.IsVowel();
    public static bool IsAscii(this char c) => c <= 127;

    public static string Repeat(this char c, int n)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < n; i++)
            sb.Append(c);

        return sb.ToString();
    }
    public static char Rot13(this char c)
        => c switch
        {
            >= 'a' and <= 'z' => (char)((c - 'a' + 13) % 26 + 'a'),
            >= 'A' and <= 'Z' => (char)((c - 'A' + 13) % 26 + 'A'),
            _ => c
        };

    /// <summary>
    /// Returns the visual density of a character in [0.0, 1.0],
    /// where ' ' = 0.0 (empty) and '█' = 1.0 (full).
    /// </summary>
    public static double Luminosity(this char c)
    {
        // Lower bar blocks U+2581–U+2588: height goes from 1/8 to 8/8
        if (c >= '▁' && c <= '█')
            return (c - '▀') / 8.0;

        return c switch
        {
            ' ' or '\t' => 0.0,
            '.' or ',' or '\'' or '`' => 0.05,
            '!' or ':' or ';' => 0.1,
            'i' or 'l' or 'I' or '|' or '1' or '/' or '\\' => 0.15,
            'j' or 'r' or 'f' or 't' or '-' => 0.2,
            // ░▒▓█ blocks
            '░' => 0.25,   // light shade
            '▒' => 0.5,   //  medium shade
            '▓' => 0.75, //   dark shade
            '█' => 1.0, //    full dark
            '▀' or '▄' or '▌' or '▐' => 0.5,   // half blocks
            'm' or 'w' => 0.55,
            'M' or 'W' => 0.65,
            '#' or '%' or '@' or '&' => 0.75,
            _ => CategoryLuminosity(c)
        };
    }

    private static double CategoryLuminosity(char c) =>
        char.GetUnicodeCategory(c) switch
        {
            UnicodeCategory.SpaceSeparator or
            UnicodeCategory.LineSeparator or
            UnicodeCategory.ParagraphSeparator or
            UnicodeCategory.Control or
            UnicodeCategory.Format => 0.0,

            UnicodeCategory.DashPunctuation or
            UnicodeCategory.OtherPunctuation or
            UnicodeCategory.OpenPunctuation or
            UnicodeCategory.ClosePunctuation or
            UnicodeCategory.ConnectorPunctuation or
            UnicodeCategory.InitialQuotePunctuation or
            UnicodeCategory.FinalQuotePunctuation => 0.2,

            UnicodeCategory.LowercaseLetter => 0.35,
            UnicodeCategory.UppercaseLetter or
            UnicodeCategory.TitlecaseLetter => 0.5,
            UnicodeCategory.OtherLetter => 0.45,

            UnicodeCategory.DecimalDigitNumber => 0.45,
            UnicodeCategory.LetterNumber or
            UnicodeCategory.OtherNumber => 0.4,

            UnicodeCategory.CurrencySymbol or
            UnicodeCategory.MathSymbol => 0.4,
            UnicodeCategory.OtherSymbol => 0.6,
            UnicodeCategory.ModifierSymbol => 0.3,

            _ => 0.4
        };
}
