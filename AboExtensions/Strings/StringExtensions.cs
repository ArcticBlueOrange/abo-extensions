using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboExtensions.Strings;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
        string.IsNullOrWhiteSpace(s);
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrWhiteSpace(s);
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? s) =>
        string.IsNullOrEmpty(s);
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrEmpty(s);
}
