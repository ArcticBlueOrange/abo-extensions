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

## Requisiti

- .NET 8.0+

## Licenza

MIT
