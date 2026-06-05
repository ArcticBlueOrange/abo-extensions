using AboExtensions.Strings;

namespace AboExtensions.Tests;

public class TestStringExtensions
{
    [Theory]
    [InlineData("  hello  ", ' ', "hello")]
    [InlineData("/path/", '/', "path")]
    [InlineData("***", '*', "")]
    [InlineData("hello", '/', "hello")]
    [InlineData("  hello", ' ', "hello")]
    [InlineData("hello  ", ' ', "hello")]
    public void TestTrimStartEnd(string input, char c, string expected) =>
        Assert.Equal(expected, input.TrimStartEnd(c));

    [Theory]
    [InlineData(new object[] { "a", "b", "c" }, ", ", "a, b, c")]
    [InlineData(new object[] { "x" }, "-", "x")]
    [InlineData(new object[] { "1", "2", "3" }, "", "123")]
    [InlineData(new object[] { }, ",", "")]
    public void TestStringJoin(object[] items, string sep, string expected) =>
        Assert.Equal(expected, items.StringJoin(sep));

    [Theory]
    [InlineData("hello", "fallback", "hello")]
    [InlineData("", "fallback", "fallback")]
    [InlineData(null, "fallback", "fallback")]
    [InlineData("   ", "fallback", "fallback")]
    public void TestOrElse(string? input, string fallback, string? expected) =>
        Assert.Equal(expected, input.OrElse(fallback));

    [Theory]
    [InlineData("   ", "fallback", false, "   ")]
    [InlineData("", "fallback", false, "fallback")]
    [InlineData(null, "fallback", false, "fallback")]
    [InlineData("hello", "fallback", false, "hello")]
    public void TestOrElseNoWs(string? input, string fallback, bool alsows, string? expected) =>
        Assert.Equal(expected, input.OrElse(fallback, alsows));

    [Theory]
    [InlineData("abc123", "0123456789", "123")]
    [InlineData("A1-B2/C3", "ABC123", "A1B2C3")]
    [InlineData("hello", "xyz", "")]
    [InlineData("", "abc", "")]
    [InlineData(null, "abc", "")]
    public void TestCharOnly(string? input, string allowed, string expected) =>
        Assert.Equal(expected, input.CharOnly(allowed));

    [Theory]
    [InlineData("abc123def456", "123456")]
    [InlineData("no digits here!", "")]
    [InlineData("007", "007")]
    [InlineData("", "")]
    public void TestNumOnly(string input, string expected) =>
        Assert.Equal(expected, input.NumOnly());

    [Theory]
    [InlineData("42", true)]
    [InlineData("3.14", true)]
    [InlineData("-7", true)]
    [InlineData("abc", false)]
    [InlineData("12x", false)]
    [InlineData("", false)]
    public void TestIsNumeric(string input, bool expected) =>
        Assert.Equal(expected, input.IsNumeric());

    [Theory]
    [InlineData("Ciao mondo", 7, "Ciao...")]
    [InlineData("Ciao", 7, "Ciao")]
    [InlineData("Ciao!", 4, "C...")]
    [InlineData("Hi", 4, "Hi")]
    [InlineData("", 4, "")]
    [InlineData(null, 4, null)]
    [InlineData("Ciao mondo", 3, "...")]
    [InlineData("Ci", 3, "Ci")]
    public void TestEllipsify(string? input, int max, string? expected) =>
        Assert.Equal(expected, input?.Ellipsify(max) ?? input);

    [Theory]
    [InlineData("/api/users", '/', "api/users")]
    [InlineData("api/users", '/', "api/users")]
    [InlineData("/", '/', "")]
    [InlineData("hello", 'x', "hello")]
    [InlineData("", 'x', "")]
    public void TestRemoveFirstChar(string input, char c, string expected) =>
        Assert.Equal(expected, input.RemoveFirstChar(c));
}
