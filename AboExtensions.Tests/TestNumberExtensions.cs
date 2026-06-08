using System.Globalization;
using AboExtensions.Numbers;

namespace AboExtensions.Tests;

public class TestNumberExtensions
{
    [Theory]
    [InlineData(3.14f, 3.14f)]
    [InlineData(null, 0f)]
    public void TestFloatOrDefault(float? input, float expected) =>
        Assert.Equal(expected, input.Or());

    [Theory]
    [InlineData(null, 99f, 99f)]
    [InlineData(3.14f, 99f, 3.14f)]
    [InlineData(0f, 99f, 0f)]
    public void TestFloatOrFallback(float? input, float fallback, float expected) =>
        Assert.Equal(expected, input.Or(fallback));

    [Theory]
    [InlineData(3.14159, 2, 3.14)]
    [InlineData(2.555, 2, 2.56)]
    [InlineData(1.0, 0, 1.0)]
    public void TestDoubleRound(double input, int dec, double expected) =>
        Assert.Equal(expected, input.Round(dec));

    [Theory]
    [InlineData("3.14159", 2, "3.14")]
    [InlineData("2.555", 2, "2.56")]
    [InlineData("1.0", 0, "1")]
    public void TestDecimalRound(string input, int dec, string expected) =>
        Assert.Equal(
            decimal.Parse(expected, CultureInfo.InvariantCulture),
            decimal.Parse(input, CultureInfo.InvariantCulture).Round(dec));

    [Theory]
    [InlineData(float.NaN, true)]
    [InlineData(float.PositiveInfinity, true)]
    [InlineData(float.NegativeInfinity, true)]
    [InlineData(0f, false)]
    [InlineData(3.14f, false)]
    public void TestIsNanOrInf(float input, bool expected) =>
        Assert.Equal(expected, input.IsNanOrInf());

    [Theory]
    [InlineData(float.NaN, false)]
    [InlineData(float.PositiveInfinity, false)]
    [InlineData(float.NegativeInfinity, false)]
    [InlineData(0f, true)]
    [InlineData(3.14f, true)]
    public void TestIsNotNanNorInf(float input, bool expected) =>
        Assert.Equal(expected, input.IsNotNanNorInf());

    [Theory]
    [InlineData(5, 5)]
    [InlineData(null, 0)]
    public void TestIntOrDefault(int? input, int expected) =>
        Assert.Equal(expected, input.Or());

    [Theory]
    [InlineData(null, 99, 99)]
    [InlineData(5, 99, 5)]
    [InlineData(0, 99, 0)]
    public void TestIntOrFallback(int? input, int fallback, int expected) =>
        Assert.Equal(expected, input.Or(fallback));

    [Theory]
    [InlineData(5, 1, 10, 5)]
    [InlineData(0, 1, 10, 1)]
    [InlineData(15, 1, 10, 10)]
    [InlineData(1, 1, 1, 1)]
    public void TestIntClamp(int input, int min, int max, int expected) =>
        Assert.Equal(expected, input.Clamp(min, max));

    [Theory]
    [InlineData(5.0, 1.0, 10.0, 5.0)]
    [InlineData(0.0, 1.0, 10.0, 1.0)]
    [InlineData(15.0, 1.0, 10.0, 10.0)]
    public void TestDoubleClamp(double input, double min, double max, double expected) =>
        Assert.Equal(expected, input.Clamp(min, max));

    [Theory]
    [InlineData(50.0, 200.0, 25.0)]
    [InlineData(1.0, 4.0, 25.0)]
    [InlineData(0.0, 100.0, 0.0)]
    [InlineData(5.0, 0.0, 0.0)]
    public void TestPercentage(double part, double total, double expected) =>
        Assert.Equal(expected, part.Percentage(total));

    [Theory]
    [InlineData(null, 0.0)]
    [InlineData(3.14, 3.14)]
    public void TestDoubleOrDefault(double? input, double expected) =>
        Assert.Equal(expected, input.Or());

    [Fact]
    public void TestDoubleOrFallback() => Assert.Equal(9.9, ((double?)null).Or(9.9));

    [Theory]
    [InlineData(-5, 5)]
    [InlineData(5, 5)]
    [InlineData(0, 0)]
    public void TestIntAbs(int input, int expected) =>
        Assert.Equal(expected, input.Abs());

    [Theory]
    [InlineData(-3.14, 3.14)]
    [InlineData(3.14, 3.14)]
    [InlineData(0.0, 0.0)]
    public void TestDoubleAbs(double input, double expected) =>
        Assert.Equal(expected, input.Abs());

    [Theory]
    [InlineData(5, 1, 10, true, true)]
    [InlineData(1, 1, 10, true, true)]
    [InlineData(10, 1, 10, true, true)]
    [InlineData(0, 1, 10, true, false)]
    [InlineData(11, 1, 10, true, false)]
    [InlineData(1, 1, 10, false, false)]
    [InlineData(10, 1, 10, false, false)]
    [InlineData(5, 1, 10, false, true)]
    public void TestIntIsBetween(int input, int min, int max, bool inclusive, bool expected) =>
        Assert.Equal(expected, input.IsBetween(min, max, inclusive));

    [Theory]
    [InlineData(5.0, 1.0, 10.0, true, true)]
    [InlineData(1.0, 1.0, 10.0, true, true)]
    [InlineData(1.0, 1.0, 10.0, false, false)]
    [InlineData(5.0, 1.0, 10.0, false, true)]
    public void TestDoubleIsBetween(double input, double min, double max, bool inclusive, bool expected) =>
        Assert.Equal(expected, input.IsBetween(min, max, inclusive));
}
