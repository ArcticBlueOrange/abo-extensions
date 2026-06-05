# AboExtensions

Piccola raccolta di extension methods per C# che mi ritrovo a riscrivere in ogni progetto. Li ho messi qui una volta per tutte.

## Installazione

```
dotnet add package AboExtensions
```

## Cosa c'è dentro

### Stringhe (`AboExtensions.Strings`)

```csharp
using AboExtensions.Strings;
```

**Controlli null/vuoto** — le versioni "leggibili" di `string.IsNullOrWhiteSpace` e compagni, usabili direttamente sulla variabile:

```csharp
if (nome.IsNullOrWhiteSpace()) ...       // null, "" e "   " → true
if (nome.IsNotNullOrWhiteSpace()) ...    // il contrario, con [NotNullWhen(true)]
if (nome.IsNullOrEmpty()) ...            // solo null e ""
if (nome.IsNotNullOrEmpty()) ...
```

**`OrElse`** — restituisce un valore di fallback se la stringa è vuota o nulla:

```csharp
var titolo = input.OrElse("Senza titolo");
// se input è null, "" o "   " → "Senza titolo"
// per ignorare gli spazi bianchi: .OrElse("default", alsows: false)
```

**`Ellipsify`** — tronca una stringa aggiungendo `...` se supera la lunghezza massima:

```csharp
"Ciao mondo".Ellipsify(7)   // → "Ciao..."
"Ciao".Ellipsify(7)         // → "Ciao"
```

**`NumOnly`** e **`CharOnly`** — filtrano i caratteri indesiderati:

```csharp
"abc123def456".NumOnly()              // → "123456"
"A1-B2/C3".CharOnly("ABC123")        // → "A1B2C3"
```

**`IsNumeric`** — controlla se la stringa rappresenta un numero:

```csharp
"42".IsNumeric()     // → true
"abc".IsNumeric()    // → false
```

**`TrimStartEnd`** — come `Trim` ma per un carattere specifico:

```csharp
"/percorso/".TrimStartEnd('/')    // → "percorso"
```

**`StringJoin`** — `string.Join` come extension su una collezione:

```csharp
new[] { "a", "b", "c" }.StringJoin(", ")    // → "a, b, c"
```

**`RemoveFirstChar`** — rimuove il primo carattere se corrisponde a quello indicato:

```csharp
"/api/users".RemoveFirstChar('/')    // → "api/users"
"api/users".RemoveFirstChar('/')     // → "api/users" (invariato)
```

### Liste (`AboExtensions.Lists`)

```csharp
using AboExtensions.Lists;
```

**`None`** — il contrario di `Any`: restituisce `true` se nessun elemento soddisfa il predicato:

```csharp
new[] { 1, 2, 3 }.None(x => x > 10)    // → true
new[] { 1, 2, 3 }.None(x => x > 2)     // → false
```

**`IsNullOrEmpty`** — controlla se una collezione è nulla o vuota:

```csharp
((IEnumerable<int>?)null).IsNullOrEmpty()    // → true
Array.Empty<int>().IsNullOrEmpty()           // → true
new[] { 1 }.IsNullOrEmpty()                 // → false
```

### Reflection (`AboExtensions.Reflections`)

```csharp
using AboExtensions.Reflections;
```

**`GetPropertyByName`** — legge il valore di una proprietà per nome:

```csharp
var persona = new { Nome = "Mario", Età = 30 };
persona.GetPropertyByName("Nome")    // → "Mario"
persona.GetPropertyByName("Età")     // → 30
```

**`GetPropertiesToString`** — concatena i valori di più proprietà in una stringa:

```csharp
var persona = new { Nome = "Mario", Cognome = "Rossi" };
ReflectionExtensions.GetPropertiesToString(persona, "Nome,Cognome")           // → "Mario Rossi"
ReflectionExtensions.GetPropertiesToString(persona, "Nome,Cognome", outSep: '-')  // → "Mario-Rossi"
ReflectionExtensions.GetPropertiesToString(persona, "Nome;Cognome", inSep: ';')   // → "Mario Rossi"
```

### Numeri (`AboExtensions.Numbers`)

```csharp
using AboExtensions.Numbers;
```

**`Or`** — valore di default per `float?` nullable:

```csharp
float? valore = null;
valore.Or()       // → 0f
valore.Or(99f)    // → 99f
```

**`Round`** — arrotondamento con numero di decimali, su `double` e `decimal`:

```csharp
3.14159.Round(2)          // → 3.14
2.555m.Round(2)           // → 2.56
```

**`IsNanOrInf`** / **`IsNotNanNorInf`** — controlli su valori float anomali:

```csharp
float.NaN.IsNanOrInf()           // → true
float.PositiveInfinity.IsNanOrInf()  // → true
3.14f.IsNotNanNorInf()           // → true
```

## Requisiti

- .NET 8.0+

## Licenza

MIT
