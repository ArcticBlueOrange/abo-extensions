using AboExtensions.Strings;

namespace AboExtensions.Tests;

public class TestNullWs
{
    [Theory]
    [InlineData("", true)]
    [InlineData(null, true)]
    [InlineData("mario", false)]
    [InlineData(" mario", false)]
    [InlineData("luigi", false)]
    [InlineData("luigi ", false)]
    [InlineData(" ", true)]
    [InlineData("\t", true)]
    [InlineData("\n", true)]
    public void TestNWS(string? s, bool exp)
    {
        var res = s.IsNullOrWhiteSpace();
        Assert.Equal(exp, res);
    }
    [Theory]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("mario", true)]
    [InlineData(" mario", true)]
    [InlineData("luigi", true)]
    [InlineData("luigi ", true)]
    [InlineData(" ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    public void TestNNWS(string? s, bool exp)
    {
        var res = s.IsNotNullOrWhiteSpace();
        Assert.Equal(exp, res);
    }
    [Theory]
    [InlineData("", true)]
    [InlineData(null, true)]
    [InlineData("mario", false)]
    [InlineData(" mario", false)]
    [InlineData("luigi", false)]
    [InlineData("luigi ", false)]
    [InlineData(" ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    public void TestNB(string? s, bool exp)
    {
        var res = s.IsNullOrEmpty();
        Assert.Equal(exp, res);
    }
    [Theory]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("mario", true)]
    [InlineData(" mario", true)]
    [InlineData("luigi", true)]
    [InlineData("luigi ", true)]
    [InlineData(" ", true)]
    [InlineData("\t", true)]
    [InlineData("\n", true)]
    public void TestNNB(string? s, bool exp)
    {
        var res = s.IsNotNullOrEmpty();
        Assert.Equal(exp, res);
    }
}