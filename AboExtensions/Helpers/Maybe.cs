namespace AboExtensions.Helpers;

// TODO: Maybe<T> - tipo per rendere esplicita la presenza o assenza di un valore,
//   alternativa più espressiva a T? per i reference type.
//
//   Struttura:
//     public struct Maybe<T>  where T : class
//     {
//         public bool HasValue { get; }
//         public T Value { get; }   // valido solo se HasValue
//     }
//
//   Factory methods:
//     Maybe<T>.Some(T value)   → crea un Maybe con valore (lancia se value è null)
//     Maybe<T>.None()          → crea un Maybe vuoto
//
//   Extension methods su Maybe<T>:
//     .Map<R>(Func<T,R> map) : Maybe<R>
//         → trasforma il valore se presente, altrimenti None
//
//     .Bind<R>(Func<T, Maybe<R>> bind) : Maybe<R>
//         → come Map ma la funzione restituisce già un Maybe
//
//     .GetValueOrDefault(T defaultValue) : T
//         → restituisce Value se presente, altrimenti defaultValue
//
//     .IfSome(Action<T> action) : Maybe<T>
//         → esegue action se HasValue, poi restituisce se stesso per il chaining
//
//     .IfNone(Action action) : Maybe<T>
//         → esegue action se !HasValue, poi restituisce se stesso per il chaining
//
//   Extension methods su T (conversione):
//     .ToMaybe(this T? value) : Maybe<T>   where T : class
//         → converte un nullable in Maybe
//         → "hello".ToMaybe() → Maybe<string>.Some("hello")
//         → ((string?)null).ToMaybe() → Maybe<string>.None()
//
//   Esempi d'uso:
//     "hello".ToMaybe().Map(s => s.Length).GetValueOrDefault(0)   → 5
//     ((string?)null).ToMaybe().Map(s => s.Length).HasValue       → false
//     user.ToMaybe().IfSome(u => Console.WriteLine(u.Name))
//
//   Nota: per value type usare direttamente T? - Maybe<T> aggiunge valore
//   principalmente per i reference type dove T? non porta semantica esplicita.
