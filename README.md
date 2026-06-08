# AboExtensions

A small collection of C# extension methods I kept rewriting in every project. Collected here once and for all.

## Installation

```
dotnet add package AboExtensions
```

## What's inside

### Strings (`AboExtensions.Strings`)

```csharp
using AboExtensions.Strings;
```

**Null/empty checks** — readable versions of `string.IsNullOrWhiteSpace` and friends, callable directly on the variable:

```csharp
if (name.IsNullOrWhiteSpace()) ...       // null, "" and "   " → true
if (name.IsNotNullOrWhiteSpace()) ...    // opposite, with [NotNullWhen(true)]
if (name.IsNullOrEmpty()) ...            // only null and ""
if (name.IsNotNullOrEmpty()) ...
```

**`OrElse`** — returns a fallback value if the string is empty or null:

```csharp
var title = input.OrElse("Untitled");
// if input is null, "" or "   " → "Untitled"
// to ignore whitespace: .OrElse("default", alsows: false)
```

**`Capitalize`** — uppercases the first letter, lowercases the rest. Pass `keepOthers: true` to leave the remaining characters unchanged:

```csharp
"hello".Capitalize()                    // → "Hello"
"WORLD".Capitalize()                    // → "World"
"hELLO".Capitalize(keepOthers: true)    // → "HELLO" (only first char uppercased)
```

**`ToSlug`** — converts a string to a URL-friendly slug:

```csharp
"Hello World".ToSlug()       // → "hello-world"
"C# is great!".ToSlug()     // → "c-is-great"
```

**`Repeat`** — repeats a string n times:

```csharp
"ab".Repeat(3)    // → "ababab"
"x".Repeat(0)     // → ""
```

**`Left`** / **`Right`** — safe substring from the left or right:

```csharp
"hello".Left(3)     // → "hel"
"hello".Right(3)    // → "llo"
"hi".Left(10)       // → "hi"  (no exception if n > length)
```

**`Ellipsify`** — truncates a string adding `...` if it exceeds the max length:

```csharp
"Hello world".Ellipsify(7)   // → "Hell..."
"Hello".Ellipsify(7)         // → "Hello"
```

**`NumOnly`** and **`CharOnly`** — filter out unwanted characters:

```csharp
"abc123def456".NumOnly()              // → "123456"
"A1-B2/C3".CharOnly("ABC123")        // → "A1B2C3"
```

**`IsNumeric`** — checks whether the string represents a number:

```csharp
"42".IsNumeric()     // → true
"abc".IsNumeric()    // → false
```

**`TrimStartEnd`** — like `Trim` but for a specific character:

```csharp
"/path/".TrimStartEnd('/')    // → "path"
```

**`StringJoin`** — `string.Join` as an extension on a collection:

```csharp
new[] { "a", "b", "c" }.StringJoin(", ")    // → "a, b, c"
```

**`ToPascalCase`** / **`ToCamelCase`** — converts a phrase (spaces, dashes, any separator) to PascalCase or camelCase:

```csharp
"hello world".ToPascalCase()    // → "HelloWorld"
"foo bar baz".ToCamelCase()     // → "fooBarBaz"
"already-slug".ToPascalCase()   // → "AlreadySlug"
"UPPER CASE".ToCamelCase()      // → "upperCase"
```

**`RemoveFirstChar`** — removes the first character if it matches the given one:

```csharp
"/api/users".RemoveFirstChar('/')    // → "api/users"
"api/users".RemoveFirstChar('/')     // → "api/users" (unchanged)
```

### Lists (`AboExtensions.Lists`)

```csharp
using AboExtensions.Lists;
```

**`None`** — the opposite of `Any`: returns `true` if no element satisfies the predicate:

```csharp
new[] { 1, 2, 3 }.None(x => x > 10)    // → true
new[] { 1, 2, 3 }.None(x => x > 2)     // → false
```

**`IsNullOrEmpty`** — checks if a collection is null or empty:

```csharp
((IEnumerable<int>?)null).IsNullOrEmpty()    // → true
Array.Empty<int>().IsNullOrEmpty()           // → true
new[] { 1 }.IsNullOrEmpty()                 // → false
```

### Reflection (`AboExtensions.Reflections`)

```csharp
using AboExtensions.Reflections;
```

**`GetPropertyByName`** — reads a property value by name:

```csharp
var person = new { Name = "Mario", Age = 30 };
person.GetPropertyByName("Name")    // → "Mario"
person.GetPropertyByName("Age")     // → 30
```

**`GetPropertiesToString`** — concatenates multiple property values into a string:

```csharp
var person = new { FirstName = "Mario", LastName = "Rossi" };
ReflectionExtensions.GetPropertiesToString(person, "FirstName,LastName")              // → "Mario Rossi"
ReflectionExtensions.GetPropertiesToString(person, "FirstName,LastName", outSep: '-') // → "Mario-Rossi"
ReflectionExtensions.GetPropertiesToString(person, "FirstName;LastName", inSep: ';')  // → "Mario Rossi"
```

### Numbers (`AboExtensions.Numbers`)

```csharp
using AboExtensions.Numbers;
```

**`Or`** — default value for nullable `float?` and `int?`:

```csharp
float? f = null;
f.Or()       // → 0f
f.Or(99f)    // → 99f

int? i = null;
i.Or()       // → 0
i.Or(99)     // → 99
```

**`Clamp`** — constrains a value within a min/max range, for `int`, `double`, and `decimal`:

```csharp
15.Clamp(1, 10)      // → 10
0.Clamp(1, 10)       // → 1
5.Clamp(1, 10)       // → 5
5.0.Clamp(1.0, 10.0) // → 5.0
```

**`Percentage`** — calculates what percentage `part` is of `total`:

```csharp
50.0.Percentage(200.0)    // → 25.0
1.0.Percentage(4.0)       // → 25.0
5.0.Percentage(0.0)       // → 0.0  (safe division by zero)
```

**`Round`** — rounding with decimal places, on `double` and `decimal`:

```csharp
3.14159.Round(2)    // → 3.14
2.555m.Round(2)     // → 2.56
```

**`IsNanOrInf`** / **`IsNotNanNorInf`** — checks for abnormal float values:

```csharp
float.NaN.IsNanOrInf()              // → true
float.PositiveInfinity.IsNanOrInf() // → true
3.14f.IsNotNanNorInf()              // → true
```

### Dates (`AboExtensions.Dates`)

```csharp
using AboExtensions.Dates;
```

**`IsWeekend`** / **`IsWeekday`** — checks whether the date falls on a weekend or weekday:

```csharp
new DateTime(2024, 1, 6).IsWeekend()   // Saturday → true
new DateTime(2024, 1, 8).IsWeekday()   // Monday   → true
```

**`StartOfDay`** / **`EndOfDay`** — returns midnight or the last tick of the day:

```csharp
new DateTime(2024, 6, 15, 14, 30, 0).StartOfDay()  // → 2024-06-15 00:00:00.000
new DateTime(2024, 6, 15, 14, 30, 0).EndOfDay()    // → 2024-06-15 23:59:59.999...
```

**`StartOfWeek`** — returns the first day of the week containing the date (default: Monday):

```csharp
new DateTime(2024, 1, 10).StartOfWeek()                    // Wednesday → 2024-01-08 (Monday)
new DateTime(2024, 1, 10).StartOfWeek(DayOfWeek.Sunday)    // Wednesday → 2024-01-07 (Sunday)
```

**`Age`** — calculates completed years from a birth date to today:

```csharp
new DateTime(1990, 6, 15).Age()   // → 34  (if today is 2024-06-15 or later)
```

## Requirements

- .NET 8.0+

## License

MIT
