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

    // Digits

    [Theory]
    [InlineData(123, 3)]
    [InlineData(0, 1)]
    [InlineData(-42, 2)]
    [InlineData(1, 1)]
    [InlineData(9, 1)]
    [InlineData(10, 2)]
    [InlineData(99, 2)]
    [InlineData(100, 3)]
    [InlineData(1000000, 7)]
    public void TestDigitsBase10(int input, int expected) =>
        Assert.Equal(expected, input.Digits());

    // ToOrdinal

    [Theory]
    [InlineData(1, "1st")]
    [InlineData(2, "2nd")]
    [InlineData(3, "3rd")]
    [InlineData(4, "4th")]
    [InlineData(11, "11th")]
    [InlineData(12, "12th")]
    [InlineData(13, "13th")]
    [InlineData(21, "21st")]
    [InlineData(22, "22nd")]
    [InlineData(23, "23rd")]
    [InlineData(111, "111th")]
    [InlineData(112, "112th")]
    [InlineData(113, "113th")]
    [InlineData(101, "101st")]
    [InlineData(0, "0th")]
    public void TestToOrdinal(int input, string expected) =>
        Assert.Equal(expected, input.ToOrdinal());

    // ToRoman

    [Theory]
    [InlineData(1, "I")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(9, "IX")]
    [InlineData(10, "X")]
    [InlineData(14, "XIV")]
    [InlineData(40, "XL")]
    [InlineData(49, "XLIX")]
    [InlineData(50, "L")]
    [InlineData(90, "XC")]
    [InlineData(99, "XCIX")]
    [InlineData(100, "C")]
    [InlineData(400, "CD")]
    [InlineData(500, "D")]
    [InlineData(900, "CM")]
    [InlineData(1000, "M")]
    [InlineData(1994, "MCMXCIV")]
    [InlineData(3999, "MMMCMXCIX")]
    [InlineData(2024, "MMXXIV")]
    [InlineData(58, "LVIII")]
    public void TestToRoman(int input, string expected) =>
        Assert.Equal(expected, input.RomanEncode());

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(4000)]
    public void TestToRomanOutOfRange(int input) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => input.RomanEncode());

    // RomanDecode

    [Theory]
    [InlineData("I", 1)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("IX", 9)]
    [InlineData("X", 10)]
    [InlineData("XIV", 14)]
    [InlineData("XL", 40)]
    [InlineData("XLIX", 49)]
    [InlineData("L", 50)]
    [InlineData("LVIII", 58)]
    [InlineData("XC", 90)]
    [InlineData("XCIX", 99)]
    [InlineData("C", 100)]
    [InlineData("CD", 400)]
    [InlineData("D", 500)]
    [InlineData("CM", 900)]
    [InlineData("M", 1000)]
    [InlineData("MCMXCIV", 1994)]
    [InlineData("MMMCMXCIX", 3999)]
    [InlineData("MMXXIV", 2024)]
    public void TestRomanDecode(string input, int expected) =>
        Assert.Equal(expected, input.RomanDecode());

    [Theory]
    [InlineData("xiv", 14)]
    [InlineData("mcmxciv", 1994)]
    [InlineData("Xiv", 14)]
    public void TestRomanDecodeCaseInsensitive(string input, int expected) =>
        Assert.Equal(expected, input.RomanDecode());

    [Theory]
    [InlineData("ABC")]
    [InlineData("123")]
    [InlineData("")]
    [InlineData("IIII")]
    [InlineData("IXIX")]
    [InlineData("DDD")]
    [InlineData("MMMM")]
    [InlineData("LL")]
    public void TestRomanDecodeInvalid(string input) =>
        Assert.Throws<FormatException>(() => input.RomanDecode());

    [Theory]
    [InlineData(4, 2, 3)]    // 4 = 100₂
    [InlineData(8, 2, 4)]    // 8 = 1000₂
    [InlineData(1, 2, 1)]    // 1 = 1₂
    [InlineData(255, 2, 8)]  // 255 = 11111111₂
    [InlineData(16, 8, 2)]
    [InlineData(0xff, 16, 2)]
    [InlineData(0x0fa, 16, 2)]
    [InlineData(0x0ffa, 16, 3)]
    [InlineData(0xf, 16, 1)]
    [InlineData(0xa, 16, 1)]
    public void TestDigitsCustomBase(int input, int @base, int expected) =>
        Assert.Equal(expected, input.Digits(@base));
}
