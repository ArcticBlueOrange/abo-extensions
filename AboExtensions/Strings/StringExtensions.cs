using AboExtensions.Chars;
using AboExtensions.StringBuilders;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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

    // TODO: NormalizeWhitespace(this string s) : string
    //   Descrizione: collassa sequenze di spazi/tab/newline consecutivi in un singolo spazio,
    //   e fa trim agli estremi.
    //   Esempi: "hello   world".NormalizeWhitespace()  → "hello world"
    //           "  a  b  c  ".NormalizeWhitespace()   → "a b c"
    //           "a\t\tb".NormalizeWhitespace()         → "a b"

    public static string Rev(this string s)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var c in s)
            sb.Prepend(c);

        return sb.ToString();
    }
    // TODO: Reverse(this string s) : string
    //   Descrizione: restituisce la stringa con i caratteri in ordine inverso.
    //   Esempi: "hello".Reverse() → "olleh"
    //           "".Reverse()      → ""
    //           "a".Reverse()     → "a"

    public static int CountOccurrences(this string s, string substr)
    {
        int c = 0;
        while (s.Contains(substr))
        {
            c++;
            s = s.Replace(substr, "");
        }
        return c;
    }

    public static string Rot13(this string s) => new([.. s.Select(c => c.Rot13())]);


    // TODO: CountOccurrences(this string s, string substring) : int
    //   Descrizione: conta quante volte substring appare in s (non sovrapposto).
    //   Parametri: substring — la sottostringa da cercare.
    //   Esempi: "abcabc".CountOccurrences("bc") → 2
    //           "aaaa".CountOccurrences("aa")   → 2  (non sovrapposto)
    //           "hello".CountOccurrences("x")   → 0

    // TODO: PadBoth(this string s, int width, char c = ' ') : string
    //   Descrizione: centra la stringa aggiungendo padding simmetrico. Se il padding
    //   totale è dispari, il carattere extra va a destra.
    //   Parametri: width — larghezza totale desiderata; c — carattere di padding (default ' ').
    //   Esempi: "hi".PadBoth(6)      → "  hi  "
    //           "hi".PadBoth(5)      → "  hi " (extra a destra)
    //           "hi".PadBoth(6, '-') → "--hi--"
    //           "toolong".PadBoth(4) → "toolong" (invariato se già più lunga)

    // TODO: ContainsAny(this string s, params string[] values) : bool
    //   Descrizione: true se la stringa contiene almeno uno dei valori passati.
    //   Parametri: values — uno o più valori da cercare.
    //   Esempi: "hello world".ContainsAny("world", "foo") → true
    //           "hello".ContainsAny("foo", "bar") → false

    // TODO: ToLines(this string s, bool removeEmpty = false) : IEnumerable<string>
    //   Descrizione: split per newline (\n, \r\n). Se removeEmpty = true scarta le righe vuote.
    //   Parametri: removeEmpty — se true ignora le righe vuote (default false).
    //   Esempi: "a\nb\nc".ToLines() → ["a", "b", "c"]
    //           "a\n\nb".ToLines(removeEmpty: true) → ["a", "b"]

    // TODO: RemoveAll(this string s, char c) : string
    //   Descrizione: rimuove tutte le occorrenze del carattere c dalla stringa.
    //   Parametri: c — il carattere da rimuovere.
    //   Esempi: "hello world".RemoveAll('l') → "heo word"
    //           "aaa".RemoveAll('a') → ""

    // TODO: Wrap(this string s, string prefix, string suffix, bool skipIfPresent = true) : string
    //   Descrizione: aggiunge prefix all'inizio e suffix alla fine della stringa.
    //   Se skipIfPresent = true (default), non aggiunge prefix se la stringa inizia già
    //   con esso, e non aggiunge suffix se la stringa finisce già con esso (check indipendenti).
    //   Parametri: prefix — stringa da preporre; suffix — stringa da appendere;
    //              skipIfPresent — evita doppio wrap (default true).
    //   Esempi: "world".Wrap("[", "]")        → "[world]"
    //           "[world".Wrap("[", "]")        → "[world]"   (prefix già presente)
    //           "world]".Wrap("[", "]")        → "[world]"   (suffix già presente)
    //           "[world]".Wrap("[", "]")       → "[world]"   (entrambi già presenti)
    //           "world".Wrap("[", "]", false)  → "[[world]]" (skipIfPresent disabilitato)

    // TODO: SmartWrap(this string s, string startEnd, bool skipIfPresent = true,
    //                IReadOnlyDictionary<string, string>? customMappings = null) : string
    //   Descrizione: wrappa la stringa usando startEnd come apertura e deducendo
    //   automaticamente la chiusura. Per caratteri con coppia naturale usa la controparte;
    //   per caratteri simmetrici ripete lo stesso.
    //   Se skipIfPresent = true (default), controlla che la stringa non inizi/finisca già
    //   con i caratteri di apertura/chiusura prima di aggiungerli.
    //   Mappa di chiusura: '[' → ']', '(' → ')', '{' → '}', '<' → '>'
    //   Simmetrici (stessa chiusura): '"', '\'', '`', '*', '_', '|', '~'
    //   Parametri: startEnd — stringa di apertura (anche multi-char, es. "<!--");
    //              skipIfPresent — evita doppio wrap (default true);
    //              customMappings — override/estensione per-call della mappa built-in.
    //   Esempi: "ciao".SmartWrap("[")    → "[ciao]"
    //           "'ciao".SmartWrap("'")   → "'ciao'"   (inizia già con ', non raddoppia)
    //           "ciao'".SmartWrap("'")   → "'ciao'"   (finisce già con ', non raddoppia)
    //           "'ciao'".SmartWrap("'")  → "'ciao'"   (invariato)
    //           "ciao".SmartWrap("'", skipIfPresent: false) → "'ciao'"  (nulla da saltare)
    //           "'ciao".SmartWrap("'", skipIfPresent: false) → "''ciao'" (raddoppia)
    //           "ciao".SmartWrap("<!--") → "<!--ciao-->"  (caso speciale HTML)
    //           "code".SmartWrap("/*", customMappings: new Dictionary<string,string>{["/*"]="*/"})
    //               → "/*code*/"
    //   Nota: per stringhe multi-char senza caso speciale, usare Wrap() esplicito.
    //
    //   ESTENSIBILITÀ — dizionario globale + override per-call (ibrido):
    //     - Esporre un dizionario statico pubblico su SmartWrapExtensions (o StringExtensions):
    //         public static Dictionary<string, string> CustomMappings = new();
    //       Il consumer lo popola una volta all'avvio dell'app:
    //         StringExtensions.CustomMappings["/*"] = "*/";
    //         StringExtensions.CustomMappings["<!--"] = "-->";
    //       e poi chiama normalmente senza parametri aggiuntivi:
    //         "code".SmartWrap("/*") → "/*code*/"
    //     - Il parametro customMappings per-call ha precedenza sul dizionario globale,
    //       utile per override isolati o contesti multi-tenant.
    //     - Ordine di risoluzione della chiusura:
    //         1. customMappings (per-call)
    //         2. CustomMappings (globale statico)
    //         3. mappa built-in (char singoli)
    //     - ATTENZIONE: CustomMappings è stato globale mutabile — documentare che
    //       le scritture non sono thread-safe; popolare solo durante l'inizializzazione.
}
