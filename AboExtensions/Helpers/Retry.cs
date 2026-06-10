namespace AboExtensions.Helpers;

// TODO: Retry - classe helper statica per eseguire operazioni con retry automatico,
//   senza dipendenze esterne.
//
//   Metodi statici:
//
//     Retry.Execute(Action action, int times, TimeSpan? delay = null,
//                   Func<Exception, bool>? shouldRetry = null) : void
//         → esegue action fino a times volte. Se fallisce tutte le volte rilancia
//           l'ultima eccezione. delay - attesa tra un tentativo e il successivo.
//           shouldRetry - predicato per decidere se ritentare su un tipo di eccezione
//           specifico (default: riprova sempre).
//
//     Retry.Execute<T>(Func<T> func, int times, TimeSpan? delay = null,
//                      Func<Exception, bool>? shouldRetry = null) : T
//         → come sopra ma restituisce il valore prodotto da func.
//
//     Retry.ExecuteAsync(Func<Task> action, int times, TimeSpan? delay = null,
//                        Func<Exception, bool>? shouldRetry = null) : Task
//         → versione async di Execute.
//
//     Retry.ExecuteAsync<T>(Func<Task<T>> func, int times, TimeSpan? delay = null,
//                           Func<Exception, bool>? shouldRetry = null) : Task<T>
//         → versione async con valore di ritorno.
//
//   Esempi d'uso:
//     Retry.Execute(() => CallUnstableApi(), times: 3, delay: TimeSpan.FromSeconds(1));
//
//     var result = Retry.Execute(() => FetchData(), times: 5,
//                                shouldRetry: ex => ex is HttpRequestException);
//
//     await Retry.ExecuteAsync(() => SendEmailAsync(), times: 3,
//                              delay: TimeSpan.FromSeconds(2));
//
//   Nota: valutare una strategia di backoff esponenziale come parametro opzionale
//   (es. delay raddoppia ad ogni tentativo) - implementabile senza deps.
