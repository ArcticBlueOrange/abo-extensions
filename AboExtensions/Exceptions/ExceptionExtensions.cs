namespace AboExtensions.Exceptions;

public static class ExceptionExtensions
{
    public static Exception GetRootCause(this Exception e)
    {
        var ret = e;
        while (ret.InnerException != null)
            ret = ret.InnerException;
        return ret;
    }
    public static IEnumerable<Exception> Flatten(this Exception e)
    {
        var v = e;
        yield return v;
        while (v.InnerException != null)
        {
            v = v.InnerException;
            yield return v;
        }
    }
    // TODO: ToLogString(this Exception ex, bool includeStackTrace = true) : string
    //   Descrizione: formatta l'eccezione e tutta la sua catena in una stringa
    //   leggibile per il logging. Include tipo, messaggio e opzionalmente lo stack trace.
    //   Parametri: includeStackTrace — se true include lo stack trace (default true).
    //   Esempi: ex.ToLogString()
    //               → "[ArgumentNullException] Value cannot be null.\n  at ...\n"
    //                  "---> [InvalidOperationException] Inner message.\n  at ..."
    //           ex.ToLogString(includeStackTrace: false)
    //               → "[ArgumentNullException] Value cannot be null.\n"
    //                  "---> [InvalidOperationException] Inner message."

    // TODO: IsOfType<T>(this Exception ex) : bool  where T : Exception
    //   Descrizione: true se l'eccezione è del tipo T (usa is, quindi vale anche per
    //   sottotipi). Shorthand leggibile per ex is T.
    //   Esempi: new ArgumentNullException("x").IsOfType<ArgumentException>() → true
    //           new InvalidOperationException().IsOfType<ArgumentException>() → false

    // TODO: InnerIfNotNull(this Exception ex, Action<Exception> action) : Exception
    //   Descrizione: esegue action sull'InnerException se non è null, poi restituisce
    //   ex per il chaining. Utile per loggare o ispezionare l'inner senza if espliciti.
    //   Analogo a IfNotNull ma specifico per la catena di eccezioni.
    //   Esempi: ex.InnerIfNotNull(inner => logger.Log(inner.Message))
    //               .ToLogString();   // chaining
    //           new Exception("no inner").InnerIfNotNull(_ => Console.WriteLine("x"))
    //               // non stampa nulla
}
