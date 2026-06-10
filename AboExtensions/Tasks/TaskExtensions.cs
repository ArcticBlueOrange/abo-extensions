namespace AboExtensions.Tasks;

public static class TaskExtensions
{
    // TODO: FireAndForget(this Task task, Action<Exception>? onError = null) : void
    //   Descrizione: esegue una Task senza await, ignorando il risultato. Se onError è fornito,
    //   viene invocato in caso di eccezione invece di lasciare l'eccezione non osservata.
    //   Parametri: onError - callback opzionale per gestire le eccezioni (default null = ignora).
    //   Esempi: SomeAsyncMethod().FireAndForget();
    //           SomeAsyncMethod().FireAndForget(ex => logger.LogError(ex, "background task failed"));
    //   Nota: internamente fa ContinueWith con TaskContinuationOptions per evitare
    //   UnobservedTaskException.
}
