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
}
