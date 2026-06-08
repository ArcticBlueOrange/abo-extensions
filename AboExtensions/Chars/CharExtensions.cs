namespace AboExtensions.Chars;

public static class CharExtensions
{
    // TODO: IsVowel(this char c) : bool
    //   Descrizione: true se il carattere è una vocale (a, e, i, o, u), case-insensitive.
    //   Considera solo le vocali ASCII base, non le accentate.
    //   Esempi: 'a'.IsVowel() → true
    //           'E'.IsVowel() → true
    //           'b'.IsVowel() → false

    // TODO: IsConsonant(this char c) : bool
    //   Descrizione: true se il carattere è una lettera ma non una vocale ASCII base.
    //   Esempi: 'b'.IsConsonant() → true
    //           'a'.IsConsonant() → false
    //           '1'.IsConsonant() → false  (non è una lettera)

    // TODO: IsAscii(this char c) : bool
    //   Descrizione: true se il carattere è nel range ASCII (0–127).
    //   Nota: già disponibile come char.IsAscii() in .NET 7+; questa extension
    //   è utile per target framework precedenti o per uniformità di stile.
    //   Esempi: 'A'.IsAscii()  → true
    //           'à'.IsAscii()  → false
    //           ' '.IsAscii()  → true

    // TODO: Repeat(this char c, int n) : string
    //   Descrizione: restituisce una stringa con il carattere ripetuto n volte.
    //   Equivale a new string(c, n) ma come extension leggibile.
    //   Esempi: '-'.Repeat(5)  → "-----"
    //           'x'.Repeat(0)  → ""
    //           'a'.Repeat(1)  → "a"
}
