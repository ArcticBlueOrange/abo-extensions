using System.Text;

namespace AboExtensions.StringBuilders;

public static class StringBuilderExtensions
{
    // TODO: AppendIf(this StringBuilder sb, bool condition, string value) : StringBuilder
    //   Descrizione: aggiunge value al builder solo se condition è true.
    //   Restituisce il builder per il chaining.
    //   Esempi: new StringBuilder().AppendIf(true, "hello").AppendIf(false, " world").ToString()
    //               → "hello"

    // TODO: AppendLineIf(this StringBuilder sb, bool condition, string value) : StringBuilder
    //   Descrizione: come AppendIf ma aggiunge anche il newline finale.
    //   Restituisce il builder per il chaining.
    //   Esempi: new StringBuilder().AppendLineIf(true, "line1").AppendLineIf(false, "line2").ToString()
    //               → "line1\r\n"  (o "\n" su Linux)

    // TODO: AppendJoin<T>(this StringBuilder sb, IEnumerable<T> values, string separator) : StringBuilder
    //   Descrizione: aggiunge tutti i valori separati da separator, senza separatore finale.
    //   Restituisce il builder per il chaining.
    //   Esempi: new StringBuilder("Result: ").AppendJoin(new[]{1,2,3}, ", ").ToString()
    //               → "Result: 1, 2, 3"

    // TODO: IsEmpty(this StringBuilder sb) : bool
    //   Descrizione: true se il builder non contiene caratteri (Length == 0).
    //   Esempi: new StringBuilder().IsEmpty()          → true
    //           new StringBuilder("x").IsEmpty()       → false
}
