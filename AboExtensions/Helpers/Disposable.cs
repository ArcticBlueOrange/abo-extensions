namespace AboExtensions.Helpers;

// TODO: Disposable - classe helper per creare IDisposable da una lambda,
//   utile per il pattern using senza dover creare classi dedicate.
//
//   Factory method statico:
//     Disposable.Create(Action onDispose) : IDisposable
//         → restituisce un IDisposable che esegue onDispose quando viene disposto.
//         → thread-safe: onDispose viene eseguito una sola volta anche se Dispose()
//           viene chiamato più volte.
//
//   Esempi d'uso:
//     using (Disposable.Create(() => Console.WriteLine("cleaned up")))
//     {
//         // ... lavoro ...
//     }  // → stampa "cleaned up"
//
//     // utile per gestire risorse temporanee senza una classe dedicata:
//     var timer = StartTimer();
//     using var cleanup = Disposable.Create(() => timer.Stop());
//
//     // utile per scope temporanei (es. cambiare cultura, impostare contesto):
//     var prev = Thread.CurrentThread.CurrentCulture;
//     using var scope = Disposable.Create(() => Thread.CurrentThread.CurrentCulture = prev);
//     Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT");
//
//   Implementazione interna:
//     private class ActionDisposable : IDisposable
//     {
//         private Action? _action;
//         public void Dispose() { Interlocked.Exchange(ref _action, null)?.Invoke(); }
//     }
//   Nota: Interlocked.Exchange garantisce che _action venga invocata una sola volta.
