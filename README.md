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

**`ToPascalCase`** / **`ToCamelCase`** — converts a phrase (spaces, dashes, any separator) to PascalCase or camelCase:

```csharp
"hello world".ToPascalCase()    // → "HelloWorld"
"foo bar baz".ToCamelCase()     // → "fooBarBaz"
"already-slug".ToPascalCase()   // → "AlreadySlug"
"UPPER CASE".ToCamelCase()      // → "upperCase"
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

**`IsEmail`** — basic structural email validation (no regex):

```csharp
"user@example.com".IsEmail()    // → true
"noatsign".IsEmail()            // → false
"user@".IsEmail()               // → false
```

**`TrimStartEnd`** — like `Trim` but for a specific character:

```csharp
"/path/".TrimStartEnd('/')    // → "path"
```

**`StringJoin`** — `string.Join` as an extension on a collection:

```csharp
new[] { "a", "b", "c" }.StringJoin(", ")    // → "a, b, c"
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

**`ForEach`** — executes an action for each element of any `IEnumerable` (LINQ doesn't have this):

```csharp
new[] { 1, 2, 3 }.ForEach(x => Console.WriteLine(x));
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

**`Batch`** — splits a sequence into chunks of the given size; the last batch may be smaller:

```csharp
new[] { 1, 2, 3, 4, 5 }.Batch(2)   // → [[1,2], [3,4], [5]]
new[] { 1, 2 }.Batch(10)            // → [[1,2]]
```

**`Shuffle`** — returns a new shuffled list (Fisher-Yates), does not mutate the original:

```csharp
new List<int> { 1, 2, 3, 4, 5 }.Shuffle()   // → e.g. [3, 1, 5, 2, 4]
```

**`WhereNotNull`** — filters null elements from a sequence; the return type is non-nullable. Works for both reference types and nullable value types:

```csharp
new[] { "a", null, "b" }.WhereNotNull()    // → ["a", "b"]
new int?[] { 1, null, 3 }.WhereNotNull()   // → [1, 3]
```

### Numbers (`AboExtensions.Numbers`)

```csharp
using AboExtensions.Numbers;
```

**`Or`** — default value for nullable numbers (`float?`, `int?`, `double?`, `decimal?`):

```csharp
float? f = null;   f.Or()      // → 0f
int? i = null;     i.Or(99)    // → 99
double? d = null;  d.Or()      // → 0.0
decimal? m = null; m.Or(9.9m)  // → 9.9m
```

**`Clamp`** — constrains a value within a min/max range, for `int`, `double`, and `decimal`:

```csharp
15.Clamp(1, 10)       // → 10
0.Clamp(1, 10)        // → 1
5.0.Clamp(1.0, 10.0)  // → 5.0
```

**`IsBetween`** — range check, inclusive by default:

```csharp
5.IsBetween(1, 10)              // → true
1.IsBetween(1, 10)              // → true  (inclusive)
1.IsBetween(1, 10, inclusive: false)  // → false (exclusive)
```

**`Abs`** — absolute value as an extension, for `int` and `double`:

```csharp
(-5).Abs()     // → 5
(-3.14).Abs()  // → 3.14
```

**`Percentage`** — calculates what percentage `part` is of `total`:

```csharp
50.0.Percentage(200.0)    // → 25.0
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

**`IsEven`** / **`IsOdd`** — parity checks on `int`:

```csharp
4.IsEven()    // → true
3.IsOdd()     // → true
0.IsEven()    // → true
```

**`Digits`** — number of digits in an integer (sign ignored), optionally in a given base:

```csharp
123.Digits()      // → 3
0.Digits()        // → 1
(-42).Digits()    // → 2
255.Digits(2)     // → 8  (11111111₂)
255.Digits(16)    // → 2  (FF₁₆)
```

**`ToOrdinal`** — converts an integer to its English ordinal string, handles the 11th/12th/13th special cases:

```csharp
1.ToOrdinal()     // → "1st"
2.ToOrdinal()     // → "2nd"
3.ToOrdinal()     // → "3rd"
4.ToOrdinal()     // → "4th"
11.ToOrdinal()    // → "11th"
21.ToOrdinal()    // → "21st"
```

### Complex Numbers (`AboExtensions.ComplexNumbers`)

```csharp
using AboExtensions.ComplexNumbers;
```

**`ToMathString`** — formats a complex number in standard mathematical notation `a+bi`, omitting zero parts and the coefficient `1` before `i`:

```csharp
new Complex(1, 2).ToMathString()    // → "1+2i"
new Complex(1, -2).ToMathString()   // → "1-2i"
new Complex(0, 2).ToMathString()    // → "2i"
new Complex(1, 0).ToMathString()    // → "1"
new Complex(0, 0).ToMathString()    // → "0"
new Complex(0, 1).ToMathString()    // → "i"
new Complex(0, -1).ToMathString()   // → "-i"
new Complex(1, 1).ToMathString()    // → "1+i"
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
new DateTime(1990, 6, 15).Age()   // → 35  (as of 2025)
```

**`IsInThePast`** / **`IsInTheFuture`** — checks whether a date is before or after the current moment:

```csharp
DateTime.Now.AddSeconds(-1).IsInThePast()    // → true
DateTime.Now.AddSeconds(1).IsInTheFuture()   // → true
```

**`Quarter`** — returns the quarter of the year (1–4):

```csharp
new DateTime(2024, 4, 1).Quarter()    // → 2
new DateTime(2024, 12, 31).Quarter()  // → 4
```

**`AddWorkdays`** — adds (or subtracts) n working days, skipping weekends:

```csharp
new DateTime(2024, 1, 5).AddWorkdays(3)    // Friday + 3 workdays → Wednesday 2024-01-10
new DateTime(2024, 1, 10).AddWorkdays(-3)  // Wednesday - 3 workdays → Friday 2024-01-05
```

**`NextWeekday`** — returns the next occurrence of a given day of the week:

```csharp
new DateTime(2024, 1, 10).NextWeekday(DayOfWeek.Friday)   // Wednesday → 2024-01-12
new DateTime(2024, 1, 8).NextWeekday(DayOfWeek.Monday)    // Monday → next Monday 2024-01-15
```

### Nullables (`AboExtensions.Nullables`)

```csharp
using AboExtensions.Nullables;
```

**`IfNotNull`** — executes an action only if the value is not null, works for both reference types and nullable value types:

```csharp
string? name = "Mario";
name.IfNotNull(n => Console.WriteLine(n));   // prints "Mario"

string? empty = null;
empty.IfNotNull(n => Console.WriteLine(n));  // does nothing

int? score = 42;
score.IfNotNull(s => Console.WriteLine(s));  // prints "42"
```

### Booleans (`AboExtensions.Booleans`)

```csharp
using AboExtensions.Booleans;
```

**`Toggle`** — returns the opposite boolean value:

```csharp
true.Toggle()     // → false
false.Toggle()    // → true

isActive = isActive.Toggle();
```

### Enums (`AboExtensions.Enums`)

```csharp
using AboExtensions.Enums;
```

**`GetValues<T>`** — returns all values of an enum type:

```csharp
EnumExtensions.GetValues<DayOfWeek>()
// → [Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday]

foreach (var status in EnumExtensions.GetValues<OrderStatus>())
    Console.WriteLine(status);
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

## Requirements

- .NET 8.0+

## License

MIT
