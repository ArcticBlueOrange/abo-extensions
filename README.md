# AboExtensions

A small collection of C# extension methods I kept rewriting in every project. Collected here once and for all.

## Installation

```
dotnet add package AboExtensions
```

## Quick reference

| Namespace | Methods |
|-----------|---------|
| `Booleans` | `Toggle` |
| `Chars` | `IsUnicodeLetter`, `IsVowel`, `IsConsonant`, `IsAscii`, `Repeat`, `Rot13`, `Luminosity` |
| `ComplexNumbers` | `ToMathString`, `IsReal`, `IsImaginary`, `ToVector2` |
| `Dates` | `IsWeekend`, `IsWeekday`, `StartOfDay`, `EndOfDay`, `StartOfWeek`, `Age`, `IsInThePast`, `IsInTheFuture`, `Quarter`, `AddWorkdays`, `NextWeekday`, `ToUnixTimestamp`, `ToUnixTimestampMs`, `FromUnixTimestamp`, `FromoUnixTimestampMs` |
| `Dictionaries` | `AddOrUpdate`, `Invert` |
| `Enums` | `GetValues` |
| `Exceptions` | `GetRootCause`, `Flatten`, `ToLogString`, `IsOfType` |
| `Guids` | `IsEmpty`, `IsNotEmpty`, `OrNew` |
| `IpAddresses` | `IsValidIp`, `IsValidIpV4`, `IsValidIpV6`, `ToIpAddress` |
| `Lists` | `ForEach`, `None`, `IsNullOrEmpty`, `Batch`, `Shuffle`, `WhereNotNull`, `Flatten`, `Frequencies` |
| `Numbers` | `Or`, `Clamp`, `IsBetween`, `Abs`, `Percentage`, `Round`, `IsNanOrInf`, `IsNotNanNorInf`, `IsEven`, `IsOdd`, `Digits`, `ToOrdinal`, `RomanEncode`, `RomanDecode` |
| `Nullables` | `IfNotNull`, `MapNotNull` |
| `Objects` | `IsNull`, `IsNotNull`, `Also`, `Let`, `In`, `NotIn` |
| `Randoms` | `NextBool`, `NextItem`, `RandomItem`, `NextEnum`, `NextString` |
| `Reflections` | `GetPropertyByName`, `GetPropertiesToString` |
| `StringBuilders` | `AppendIf`, `AppendLineIf`, `Prepend` |
| `Strings` | `IsNullOrWhiteSpace`, `IsNotNullOrWhiteSpace`, `IsNullOrEmpty`, `IsNotNullOrEmpty`, `OrElse`, `Capitalize`, `ToSlug`, `ToPascalCase`, `ToCamelCase`, `Repeat`, `Left`, `Right`, `Ellipsify`, `NumOnly`, `CharOnly`, `IsNumeric`, `IsEmail`, `TrimStartEnd`, `StringJoin`, `RemoveFirstChar` |
| `Hashing` | `ToSha256`, `ToSha512`, `ToMd5`, `ToHex`, `FromHex`, `ToBase64` |
| `TimeSpans` | `IsZero`, `Ago`, `FromNow` |

---

## Strings (`AboExtensions.Strings`)

```csharp
using AboExtensions.Strings;
```

**Null/empty checks** - readable versions of `string.IsNullOrWhiteSpace` and friends, callable directly on the variable:

```csharp
if (name.IsNullOrWhiteSpace()) ...       // null, "" and "   " → true
if (name.IsNotNullOrWhiteSpace()) ...    // opposite, with [NotNullWhen(true)]
if (name.IsNullOrEmpty()) ...            // only null and ""
if (name.IsNotNullOrEmpty()) ...
```

**`OrElse`** - returns a fallback value if the string is empty or null:

```csharp
var title = input.OrElse("Untitled");
// if input is null, "" or "   " → "Untitled"
// to ignore whitespace: .OrElse("default", alsows: false)
```

**`Capitalize`** - uppercases the first letter, lowercases the rest. Pass `keepOthers: true` to leave the remaining characters unchanged:

```csharp
"hello".Capitalize()                    // → "Hello"
"WORLD".Capitalize()                    // → "World"
"hELLO".Capitalize(keepOthers: true)    // → "HELLO" (only first char uppercased)
```

**`ToSlug`** - converts a string to a URL-friendly slug:

```csharp
"Hello World".ToSlug()       // → "hello-world"
"C# is great!".ToSlug()     // → "c-is-great"
```

**`ToPascalCase`** / **`ToCamelCase`** - converts a phrase (spaces, dashes, any separator) to PascalCase or camelCase:

```csharp
"hello world".ToPascalCase()    // → "HelloWorld"
"foo bar baz".ToCamelCase()     // → "fooBarBaz"
"already-slug".ToPascalCase()   // → "AlreadySlug"
"UPPER CASE".ToCamelCase()      // → "upperCase"
```

**`Repeat`** - repeats a string n times:

```csharp
"ab".Repeat(3)    // → "ababab"
"x".Repeat(0)     // → ""
```

**`Left`** / **`Right`** - safe substring from the left or right:

```csharp
"hello".Left(3)     // → "hel"
"hello".Right(3)    // → "llo"
"hi".Left(10)       // → "hi"  (no exception if n > length)
```

**`Ellipsify`** - truncates a string adding `...` if it exceeds the max length:

```csharp
"Hello world".Ellipsify(7)   // → "Hell..."
"Hello".Ellipsify(7)         // → "Hello"
```

**`NumOnly`** and **`CharOnly`** - filter out unwanted characters:

```csharp
"abc123def456".NumOnly()              // → "123456"
"A1-B2/C3".CharOnly("ABC123")        // → "A1B2C3"
```

**`IsNumeric`** - checks whether the string represents a number:

```csharp
"42".IsNumeric()     // → true
"abc".IsNumeric()    // → false
```

**`IsEmail`** - basic structural email validation (no regex):

```csharp
"user@example.com".IsEmail()    // → true
"noatsign".IsEmail()            // → false
"user@".IsEmail()               // → false
```

**`TrimStartEnd`** - like `Trim` but for a specific character:

```csharp
"/path/".TrimStartEnd('/')    // → "path"
```

**`StringJoin`** - `string.Join` as an extension on a collection:

```csharp
new[] { "a", "b", "c" }.StringJoin(", ")    // → "a, b, c"
```

**`RemoveFirstChar`** - removes the first character if it matches the given one:

```csharp
"/api/users".RemoveFirstChar('/')    // → "api/users"
"api/users".RemoveFirstChar('/')     // → "api/users" (unchanged)
```

---

## Numbers (`AboExtensions.Numbers`)

```csharp
using AboExtensions.Numbers;
```

**`Or`** - default value for nullable numbers (`float?`, `int?`, `double?`, `decimal?`):

```csharp
float? f = null;   f.Or()      // → 0f
int? i = null;     i.Or(99)    // → 99
double? d = null;  d.Or()      // → 0.0
decimal? m = null; m.Or(9.9m)  // → 9.9m
```

**`Clamp`** - constrains a value within a min/max range, for `int`, `double`, and `decimal`:

```csharp
15.Clamp(1, 10)       // → 10
0.Clamp(1, 10)        // → 1
5.0.Clamp(1.0, 10.0)  // → 5.0
```

**`IsBetween`** - range check, inclusive by default:

```csharp
5.IsBetween(1, 10)                    // → true
1.IsBetween(1, 10)                    // → true  (inclusive)
1.IsBetween(1, 10, inclusive: false)  // → false (exclusive)
```

**`Abs`** - absolute value as an extension, for `int` and `double`:

```csharp
(-5).Abs()     // → 5
(-3.14).Abs()  // → 3.14
```

**`Percentage`** - calculates what percentage `part` is of `total`:

```csharp
50.0.Percentage(200.0)    // → 25.0
5.0.Percentage(0.0)       // → 0.0  (safe division by zero)
```

**`Round`** - rounding with decimal places, on `double` and `decimal`:

```csharp
3.14159.Round(2)    // → 3.14
2.555m.Round(2)     // → 2.56
```

**`IsNanOrInf`** / **`IsNotNanNorInf`** - checks for abnormal float values:

```csharp
float.NaN.IsNanOrInf()              // → true
float.PositiveInfinity.IsNanOrInf() // → true
3.14f.IsNotNanNorInf()              // → true
```

**`IsEven`** / **`IsOdd`** - parity checks on `int`:

```csharp
4.IsEven()    // → true
3.IsOdd()     // → true
0.IsEven()    // → true
```

**`Digits`** - number of digits in an integer (sign ignored), optionally in a given base:

```csharp
123.Digits()      // → 3
0.Digits()        // → 1
(-42).Digits()    // → 2
255.Digits(2)     // → 8  (11111111₂)
255.Digits(16)    // → 2  (FF₁₆)
```

**`ToOrdinal`** - converts an integer to its English ordinal string, handles the 11th/12th/13th special cases:

```csharp
1.ToOrdinal()     // → "1st"
2.ToOrdinal()     // → "2nd"
3.ToOrdinal()     // → "3rd"
4.ToOrdinal()     // → "4th"
11.ToOrdinal()    // → "11th"
21.ToOrdinal()    // → "21st"
```

**`RomanEncode`** - converts a positive integer to a Roman numeral string. Range: 1–3999:

```csharp
1.RomanEncode()       // → "I"
4.RomanEncode()       // → "IV"
1994.RomanEncode()    // → "MCMXCIV"
3999.RomanEncode()    // → "MMMCMXCIX"
// throws ArgumentOutOfRangeException if outside 1–3999
```

**`RomanDecode`** - converts a Roman numeral string to an integer. Case-insensitive:

```csharp
"XIV".RomanDecode()     // → 14
"MCMXCIV".RomanDecode() // → 1994
"xiv".RomanDecode()     // → 14
```

---

## Lists (`AboExtensions.Lists`)

```csharp
using AboExtensions.Lists;
```

**`ForEach`** - executes an action for each element of any `IEnumerable` (LINQ doesn't have this):

```csharp
new[] { 1, 2, 3 }.ForEach(x => Console.WriteLine(x));
```

**`None`** - the opposite of `Any`: returns `true` if no element satisfies the predicate:

```csharp
new[] { 1, 2, 3 }.None(x => x > 10)    // → true
new[] { 1, 2, 3 }.None(x => x > 2)     // → false
```

**`IsNullOrEmpty`** - checks if a collection is null or empty:

```csharp
((IEnumerable<int>?)null).IsNullOrEmpty()    // → true
Array.Empty<int>().IsNullOrEmpty()           // → true
new[] { 1 }.IsNullOrEmpty()                 // → false
```

**`Batch`** - splits a sequence into chunks of the given size; the last batch may be smaller:

```csharp
new[] { 1, 2, 3, 4, 5 }.Batch(2)   // → [[1,2], [3,4], [5]]
new[] { 1, 2 }.Batch(10)            // → [[1,2]]
```

**`Shuffle`** - returns a new shuffled list (Fisher-Yates), does not mutate the original:

```csharp
new List<int> { 1, 2, 3, 4, 5 }.Shuffle()   // → e.g. [3, 1, 5, 2, 4]
```

**`WhereNotNull`** - filters null elements from a sequence; the return type is non-nullable. Works for both reference types and nullable value types:

```csharp
new[] { "a", null, "b" }.WhereNotNull()    // → ["a", "b"]
new int?[] { 1, null, 3 }.WhereNotNull()   // → [1, 3]
```

**`Flatten`** - flattens a sequence of sequences into a single list:

```csharp
new[] { new[] { 1, 2 }, new[] { 3, 4 } }.Flatten()   // → [1, 2, 3, 4]
new[] { new[] { "a" }, new[] { "b", "c" } }.Flatten() // → ["a", "b", "c"]
```

**`Frequencies`** - counts occurrences of each element, returning a `Dictionary<T, int>`:

```csharp
new[] { "a", "b", "a", "c", "a", "b" }.Frequencies()
// → { "a": 3, "b": 2, "c": 1 }

new[] { 1, 2, 1, 1 }.Frequencies()
// → { 1: 3, 2: 1 }
```

---

## Dates (`AboExtensions.Dates`)

```csharp
using AboExtensions.Dates;
```

**`IsWeekend`** / **`IsWeekday`** - checks whether the date falls on a weekend or weekday:

```csharp
new DateTime(2024, 1, 6).IsWeekend()   // Saturday → true
new DateTime(2024, 1, 8).IsWeekday()   // Monday   → true
```

**`StartOfDay`** / **`EndOfDay`** - returns midnight or the last tick of the day:

```csharp
new DateTime(2024, 6, 15, 14, 30, 0).StartOfDay()  // → 2024-06-15 00:00:00.000
new DateTime(2024, 6, 15, 14, 30, 0).EndOfDay()    // → 2024-06-15 23:59:59.999...
```

**`StartOfWeek`** - returns the first day of the week containing the date (default: Monday):

```csharp
new DateTime(2024, 1, 10).StartOfWeek()                    // Wednesday → 2024-01-08 (Monday)
new DateTime(2024, 1, 10).StartOfWeek(DayOfWeek.Sunday)    // Wednesday → 2024-01-07 (Sunday)
```

**`Age`** - calculates completed years from a birth date to today:

```csharp
new DateTime(1990, 6, 15).Age()   // → 35  (as of 2025)
```

**`IsInThePast`** / **`IsInTheFuture`** - checks whether a date is before or after the current moment:

```csharp
DateTime.Now.AddSeconds(-1).IsInThePast()    // → true
DateTime.Now.AddSeconds(1).IsInTheFuture()   // → true
```

**`Quarter`** - returns the quarter of the year (1–4):

```csharp
new DateTime(2024, 4, 1).Quarter()    // → 2
new DateTime(2024, 12, 31).Quarter()  // → 4
```

**`AddWorkdays`** - adds (or subtracts) n working days, skipping weekends:

```csharp
new DateTime(2024, 1, 5).AddWorkdays(3)    // Friday + 3 workdays → Wednesday 2024-01-10
new DateTime(2024, 1, 10).AddWorkdays(-3)  // Wednesday - 3 workdays → Friday 2024-01-05
```

**`NextWeekday`** - returns the next occurrence of a given day of the week:

```csharp
new DateTime(2024, 1, 10).NextWeekday(DayOfWeek.Friday)   // Wednesday → 2024-01-12
new DateTime(2024, 1, 8).NextWeekday(DayOfWeek.Monday)    // Monday → next Monday 2024-01-15
```

**`ToUnixTimestamp`** / **`ToUnixTimestampMs`** — converts a `DateTime` to a Unix timestamp in seconds or milliseconds. `Local` is converted to UTC; `Unspecified` is treated as UTC by default (pass `throwOnUnspecified: true` to enforce strictness):

```csharp
DateTime.UnixEpoch.ToUnixTimestamp()                                          // → 0
new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToUnixTimestamp()        // → 1704067200
new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc).ToUnixTimestampMs() // → 1704067200500
```

**`FromUnixTimestamp`** / **`FromoUnixTimestampMs`** — converts a Unix timestamp back to a `DateTime` with `Kind = Utc`:

```csharp
0L.FromUnixTimestamp()           // → DateTime.UnixEpoch (1970-01-01 00:00:00 UTC)
1704067200L.FromUnixTimestamp()  // → 2024-01-01 00:00:00 UTC

// round-trip:
dt.ToUnixTimestamp().FromUnixTimestamp() == dt   // → true
```

---

## Booleans (`AboExtensions.Booleans`)

```csharp
using AboExtensions.Booleans;
```

**`Toggle`** - returns the opposite boolean value:

```csharp
true.Toggle()     // → false
false.Toggle()    // → true

isActive = isActive.Toggle();
```

---

## Chars (`AboExtensions.Chars`)

```csharp
using AboExtensions.Chars;
```

**`IsUnicodeLetter`** / **`IsVowel`** / **`IsConsonant`** - character classification:

```csharp
'a'.IsUnicodeLetter()    // → true
'3'.IsUnicodeLetter()    // → false
'e'.IsVowel()            // → true
'b'.IsConsonant()        // → true
```

**`IsAscii`** - true if the character code is ≤ 127:

```csharp
'A'.IsAscii()    // → true
'é'.IsAscii()    // → false
```

**`Repeat`** - repeats a character n times into a string:

```csharp
'-'.Repeat(5)    // → "-----"
'x'.Repeat(0)    // → ""
```

**`Rot13`** - applies the ROT13 cipher. Non-letter characters are unchanged:

```csharp
'A'.Rot13()    // → 'N'
'n'.Rot13()    // → 'a'
'3'.Rot13()    // → '3'
```

**`Luminosity`** - returns the visual density of a character as a `double` in `[0.0, 1.0]`, where `' '` is empty and `'█'` is full. Useful for ASCII art and terminal rendering:

```csharp
' '.Luminosity()    // → 0.0
'.'.Luminosity()    // → 0.05
'i'.Luminosity()    // → 0.15
'a'.Luminosity()    // → 0.35  (Unicode category fallback)
'W'.Luminosity()    // → 0.65
'@'.Luminosity()    // → 0.75
'░'.Luminosity()    // → 0.25
'▒'.Luminosity()    // → 0.5
'▓'.Luminosity()    // → 0.75
'█'.Luminosity()    // → 1.0
// lower bar blocks are proportional (▁→0.125 … █→1.0)
```

---

## Complex Numbers (`AboExtensions.ComplexNumbers`)

```csharp
using AboExtensions.ComplexNumbers;
```

**`ToMathString`** - formats a complex number in standard mathematical notation `a+bi`, omitting zero parts and the coefficient `1` before `i`:

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

**`IsReal`** - true if the imaginary part is zero:

```csharp
new Complex(3, 0).IsReal()    // → true
new Complex(3, 1).IsReal()    // → false
```

**`IsImaginary`** - true if the real part is zero and the imaginary part is non-zero:

```csharp
new Complex(0, 2).IsImaginary()    // → true
new Complex(0, 0).IsImaginary()    // → false
new Complex(1, 2).IsImaginary()    // → false
```

**`ToVector2`** - converts to `System.Numerics.Vector2` (Real → X, Imaginary → Y):

```csharp
new Complex(3, 4).ToVector2()    // → new Vector2(3f, 4f)
new Complex(0, 1).ToVector2()    // → new Vector2(0f, 1f)
```

---

## Dictionaries (`AboExtensions.Dictionaries`)

```csharp
using AboExtensions.Dictionaries;
```

**`AddOrUpdate`** - adds or updates a key/value pair; returns the dictionary for chaining:

```csharp
dict.AddOrUpdate("key", 42)    // adds if missing, overwrites if present

new Dictionary<string, int>()
    .AddOrUpdate("a", 1)
    .AddOrUpdate("b", 2);      // chaining
```

**`Invert`** - swaps keys and values. Throws `ArgumentException` on duplicate values by default:

```csharp
new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }.Invert()
// → { 1: "a", 2: "b" }

// silently keep last value on duplicate:
dict.Invert(throwOnDuplicate: false)
```

---

## Enums (`AboExtensions.Enums`)

```csharp
using AboExtensions.Enums;
```

**`GetValues<T>`** - returns all values of an enum type:

```csharp
EnumExtensions.GetValues<DayOfWeek>()
// → [Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday]

foreach (var status in EnumExtensions.GetValues<OrderStatus>())
    Console.WriteLine(status);
```

---

## Exceptions (`AboExtensions.Exceptions`)

```csharp
using AboExtensions.Exceptions;
```

**`GetRootCause`** - walks the `InnerException` chain and returns the deepest exception:

```csharp
new Exception("outer", new Exception("mid", new Exception("root")))
    .GetRootCause().Message    // → "root"

new Exception("no inner").GetRootCause().Message    // → "no inner"
```

**`Flatten`** - returns all exceptions in the chain as a flat sequence, outermost first:

```csharp
new Exception("a", new Exception("b", new Exception("c")))
    .Flatten()
    .Select(e => e.Message)    // → ["a", "b", "c"]
```

**`ToLogString`** - formats the full exception chain into a readable string for logging, with type name in brackets and inner exceptions prefixed by `--->`:

```csharp
var inner = new InvalidOperationException("connection lost");
var outer = new Exception("request failed", inner);

outer.ToLogString()
// → "[Exception]request failed...
//    ---> [InvalidOperationException]connection lost"
```

**`IsOfType<T>`** - checks whether the exception is of a given type, including derived types:

```csharp
new ArgumentNullException().IsOfType<ArgumentNullException>()  // → true
new ArgumentNullException().IsOfType<ArgumentException>()      // → true  (base type)
new ArgumentException().IsOfType<ArgumentNullException>()      // → false
```

---

## Guids (`AboExtensions.Guids`)

```csharp
using AboExtensions.Guids;
```

**`IsEmpty`** / **`IsNotEmpty`** - checks against `Guid.Empty`:

```csharp
Guid.Empty.IsEmpty()       // → true
Guid.NewGuid().IsEmpty()   // → false
```

**`OrNew`** - returns the Guid if not empty, otherwise generates a new one:

```csharp
Guid.Empty.OrNew()       // → new Guid
Guid.NewGuid().OrNew()   // → same Guid (unchanged)
```

---

## IP Addresses (`AboExtensions.IpAddresses`)

```csharp
using AboExtensions.IpAddresses;
```

**`IsValidIp`** - true if the string is a valid IPv4 or IPv6 address:

```csharp
"192.168.1.1".IsValidIp()    // → true
"::1".IsValidIp()            // → true  (IPv6 loopback)
"256.0.0.1".IsValidIp()      // → false
"hello".IsValidIp()          // → false
```

**`IsValidIpV4`** / **`IsValidIpV6`** - version-specific validation:

```csharp
"192.168.1.1".IsValidIpV4()   // → true
"::1".IsValidIpV4()           // → false
"::1".IsValidIpV6()           // → true
"192.168.1.1".IsValidIpV6()   // → false
```

**`ToIpAddress`** - parses to `IPAddress?`, returning null if invalid:

```csharp
"192.168.1.1".ToIpAddress()   // → IPAddress { 192.168.1.1 }
"invalid".ToIpAddress()       // → null
```

---

## Nullables (`AboExtensions.Nullables`)

```csharp
using AboExtensions.Nullables;
```

**`IfNotNull`** - executes an action only if the value is not null, works for both reference types and nullable value types:

```csharp
string? name = "Mario";
name.IfNotNull(n => Console.WriteLine(n));   // prints "Mario"

string? empty = null;
empty.IfNotNull(n => Console.WriteLine(n));  // does nothing

int? score = 42;
score.IfNotNull(s => Console.WriteLine(s));  // prints "42"
```

**`MapNotNull`** - transforms a nullable value with a function, returning `default` if null:

```csharp
string? s = "hello";
s.MapNotNull(x => x.Length)      // → 5
s.MapNotNull(x => x.ToUpper())   // → "HELLO"

string? n = null;
n.MapNotNull(x => x.Length)      // → 0 (default int)
n.MapNotNull(x => x.ToUpper())   // → null

// chaining:
"42".MapNotNull(int.Parse).MapNotNull(n => n * 2)   // → 84
```

---

## Objects (`AboExtensions.Objects`)

```csharp
using AboExtensions.Objects;
```

**`IsNull`** / **`IsNotNull`** - readable null checks on any object:

```csharp
((string?)null).IsNull()     // → true
"hello".IsNull()             // → false
"hello".IsNotNull()          // → true
```

**`Also`** - executes a side-effect on the object and returns it unchanged, for use inside fluent pipelines:

```csharp
user.Also(u => { logger.Log(u.Name); return true; })
    .Save();

// chaining:
"hello"
    .Also(s => { log.Add(s); return true; })
    .Also(s => { log.Add(s.ToUpper()); return true; });
// log → ["hello", "HELLO"], result → "hello"
```

**`Let`** - applies a transformation and returns the result; readable alternative to a temporary variable:

```csharp
"42".Let(int.Parse)              // → 42
"hello".Let(s => s.Length)       // → 5
5.Let(n => n.ToString())         // → "5"

// chaining:
"42".Let(int.Parse).Let(n => n * 2)   // → 84
```

**`In`** / **`NotIn`** - checks whether a value is (or isn't) in a set of candidates; readable alternative to chained `||`:

```csharp
status.In(Active, Pending)          // → true if status is either
"x".In("a", "b", "c")              // → false
status.NotIn(Deleted, Archived)     // → true if status is neither
```

---

## Randoms (`AboExtensions.Randoms`)

```csharp
using AboExtensions.Randoms;
```

**`NextBool`** - returns `true` or `false` with 50/50 probability, or with a given probability:

```csharp
rng.NextBool()          // → true or false (50%)
rng.NextBool(0.9)       // → true ~90% of the time
rng.NextBool(1.0)       // → always true
rng.NextBool(0.0)       // → always false
```

**`NextItem`** - returns a random element from a list:

```csharp
rng.NextItem(new[] { "a", "b", "c" })   // → "a", "b" or "c"
```

**`RandomItem`** - extension on `IList<T>`, picks a random element without needing a `Random` instance:

```csharp
new[] { 1, 2, 3 }.RandomItem()   // → 1, 2 or 3
```

**`NextEnum`** - returns a random value of an enum:

```csharp
rng.NextEnum<DayOfWeek>()   // → one of the 7 days
```

**`NextString`** - generates a random string of the given length from a character pool:

```csharp
rng.NextString(8)                   // → e.g. "k4f2m9xr"  (alphanumeric + - _)
rng.NextString(4, "AEIOU")          // → e.g. "OEUA"
```

---

## Reflection (`AboExtensions.Reflections`)

```csharp
using AboExtensions.Reflections;
```

**`GetPropertyByName`** - reads a property value by name:

```csharp
var person = new { Name = "Mario", Age = 30 };
person.GetPropertyByName("Name")    // → "Mario"
person.GetPropertyByName("Age")     // → 30
```

**`GetPropertiesToString`** - concatenates multiple property values into a string:

```csharp
var person = new { FirstName = "Mario", LastName = "Rossi" };
ReflectionExtensions.GetPropertiesToString(person, "FirstName,LastName")              // → "Mario Rossi"
ReflectionExtensions.GetPropertiesToString(person, "FirstName,LastName", outSep: '-') // → "Mario-Rossi"
ReflectionExtensions.GetPropertiesToString(person, "FirstName;LastName", inSep: ';')  // → "Mario Rossi"
```

---

## StringBuilders (`AboExtensions.StringBuilders`)

```csharp
using AboExtensions.StringBuilders;
```

**`AppendIf`** - appends text only if the condition is true; returns the builder for chaining:

```csharp
new StringBuilder()
    .AppendIf(true, "hello")
    .AppendIf(false, " world")
    .ToString()    // → "hello"
```

**`AppendLineIf`** - like `AppendIf` but also appends a newline:

```csharp
new StringBuilder()
    .AppendLineIf(true, "line1")
    .AppendLineIf(false, "line2")
    .ToString()    // → "line1\r\n"
```

**`Prepend`** - inserts text at the beginning of the builder:

```csharp
new StringBuilder("world").Prepend("hello ").ToString()   // → "hello world"
```

---

## TimeSpans (`AboExtensions.TimeSpans`)

```csharp
using AboExtensions.TimeSpans;
```

**`IsZero`** - true if the TimeSpan equals `TimeSpan.Zero`:

```csharp
TimeSpan.Zero.IsZero()              // → true
TimeSpan.FromSeconds(1).IsZero()    // → false
```

**`Ago`** / **`FromNow`** - converts a TimeSpan to a `DateTime` relative to now:

```csharp
TimeSpan.FromHours(2).Ago()       // → DateTime.Now - 2 hours
TimeSpan.FromDays(1).FromNow()    // → DateTime.Now + 1 day
```

---

## Hashing (`AboExtensions.Hashing`)

```csharp
using AboExtensions.Hashing;
```

**`ToSha256`** - SHA-256 hash as a lowercase hex string. Use for checksums, cache keys, deduplication. Do not use for passwords:

```csharp
"hello".ToSha256()    // → "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824"
"".ToSha256()         // → "e3b0c44298fc1c149afbf4c8996fb924..."

// byte[] overload:
new byte[] { 1, 2, 3 }.ToSha256()   // → "039058c6f2c0cb492c533b0a4d14ef77..."
```

**`ToSha512`** - SHA-512 hash as a lowercase hex string (128 characters):

```csharp
"hello".ToSha512()   // → "9b71d224bd62f3785d96d46ad3ea3d73..."
```

**`ToMd5`** - MD5 hash as a lowercase hex string. Use for Gravatar, ETag, checksums. Do not use for passwords:

```csharp
"hello".ToMd5()    // → "5d41402abc4b2a76b9719d911017c592"
"".ToMd5()         // → "d41d8cd98f00b204e9800998ecf8427e"
```

**`ToHex`** / **`FromHex`** - converts between `byte[]` and a lowercase hex string:

```csharp
new byte[] { 0, 255, 16 }.ToHex()    // → "00ff10"
"00ff10".FromHex()                    // → new byte[] { 0, 255, 16 }

// round-trip:
bytes.ToHex().FromHex() == bytes      // → true
```

**`ToBase64`** - encodes to Base64, available on both `string` and `byte[]`:

```csharp
"hello".ToBase64()                    // → "aGVsbG8="
new byte[] { 1, 2, 3 }.ToBase64()    // → "AQID"
```

---

## Requirements

- .NET 8.0+

## License

MIT
