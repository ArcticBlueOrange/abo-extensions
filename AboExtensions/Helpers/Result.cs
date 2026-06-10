namespace AboExtensions.Helpers;

// TODO: Result<T> - tipo per rappresentare il risultato di un'operazione che può fallire,
//   senza usare eccezioni per il flusso di controllo normale.
//
//   Struttura:
//     public class Result<T>
//     {
//         public bool IsSuccess { get; }
//         public bool IsFailure => !IsSuccess;
//         public T Value { get; }           // valido solo se IsSuccess
//         public string Error { get; }      // valido solo se IsFailure
//     }
//
//   Factory methods statici:
//     Result<T>.Ok(T value)          → crea un risultato di successo
//     Result<T>.Fail(string error)   → crea un risultato di errore
//
//   Extension methods su Result<T>:
//     .Map<R>(Func<T,R> map) : Result<R>
//         → trasforma il valore se Success, propaga l'errore se Failure
//         → result.Map(x => x.ToUpper())
//
//     .Bind<R>(Func<T, Result<R>> bind) : Result<R>
//         → come Map ma la funzione restituisce già un Result (flatMap/chain)
//         → result.Bind(x => TryParse(x))
//
//     .OnSuccess(Action<T> action) : Result<T>
//         → esegue action se Success, poi restituisce se stesso per il chaining
//
//     .OnFailure(Action<string> action) : Result<T>
//         → esegue action con il messaggio di errore se Failure, poi restituisce se stesso
//
//     .GetValueOrDefault(T defaultValue = default) : T
//         → restituisce Value se Success, altrimenti defaultValue
//
//   Esempi d'uso:
//     Result<int>.Ok(42).Map(x => x * 2).Value              → 84
//     Result<int>.Fail("errore").Map(x => x * 2).Error      → "errore"
//     Result<int>.Ok(42).OnSuccess(v => Console.WriteLine(v))
//     Result<string>.Fail("not found").GetValueOrDefault("") → ""
//
//   Nota: valutare se aggiungere un Result non-generico (senza valore) per operazioni
//   void che possono fallire.
