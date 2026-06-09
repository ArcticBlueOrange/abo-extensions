using AboExtensions.ComplexNumbers;
using System.Numerics;

namespace AboExtensions.Tests;

public class TestComplexNumbers
{
    [Theory]
    [InlineData(1, 2, "1+2i")]
    [InlineData(1, -2, "1-2i")]
    [InlineData(0, 2, "2i")]
    [InlineData(1, 0, "1")]
    [InlineData(0, 0, "0")]
    [InlineData(0, 1, "i")]
    [InlineData(0, -1, "-i")]
    [InlineData(1, 1, "1+i")]
    [InlineData(1, -1, "1-i")]
    [InlineData(-1, 2, "-1+2i")]
    public void TestToMathString(double real, double imaginary, string expected)
    {
        var c = new Complex(real, imaginary);
        Assert.Equal(expected, c.ToMathString());
    }
}
