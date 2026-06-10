using AboExtensions.Chars;

namespace AboExtensions.Tests;

public class TestCharExtensions
{
    // IsUnicodeLetter / IsVowel / IsConsonant / IsAscii / Repeat / Rot13
    // (covered by TODO AGG. TEST note in source — skipped here, focus on Luminosity)

    // Luminosity — extremes

    [Fact]
    public void Luminosity_Space_IsZero() =>
        Assert.Equal(0.0, ' '.Luminosity());

    [Fact]
    public void Luminosity_FullBlock_IsOne() =>
        Assert.Equal(1.0, '█'.Luminosity());

    // Luminosity — shade characters

    [Theory]
    [InlineData('░', 0.25)]
    [InlineData('▒', 0.5)]
    [InlineData('▓', 0.75)]
    public void Luminosity_Shades(char c, double expected) =>
        Assert.Equal(expected, c.Luminosity());

    // Luminosity — lower bar blocks (proportional)

    [Theory]
    [InlineData('▁', 1.0 / 8)]
    [InlineData('▂', 2.0 / 8)]
    [InlineData('▃', 3.0 / 8)]
    [InlineData('▄', 4.0 / 8)]
    [InlineData('▅', 5.0 / 8)]
    [InlineData('▆', 6.0 / 8)]
    [InlineData('▇', 7.0 / 8)]
    [InlineData('█', 8.0 / 8)]
    public void Luminosity_BarBlocks_Proportional(char c, double expected) =>
        Assert.Equal(expected, c.Luminosity(), precision: 10);

    // Luminosity — thin chars are lighter than heavy chars

    [Fact]
    public void Luminosity_ThinLighterThanNormal() =>
        Assert.True('i'.Luminosity() < 'a'.Luminosity());

    [Fact]
    public void Luminosity_NormalLighterThanHeavy() =>
        Assert.True('a'.Luminosity() < 'M'.Luminosity());

    [Fact]
    public void Luminosity_HeavyLighterThanBlock() =>
        Assert.True('@'.Luminosity() < '█'.Luminosity());

    // Luminosity — explicit known values

    [Theory]
    [InlineData('.', 0.05)]
    [InlineData('!', 0.1)]
    [InlineData('i', 0.15)]
    [InlineData('-', 0.2)]
    [InlineData('w', 0.55)]
    [InlineData('W', 0.65)]
    [InlineData('@', 0.75)]
    public void Luminosity_KnownValues(char c, double expected) =>
        Assert.Equal(expected, c.Luminosity());

    // Luminosity — Unicode category fallback

    [Fact]
    public void Luminosity_LowercaseLetter_Is035() =>
        Assert.Equal(0.35, 'é'.Luminosity());

    [Fact]
    public void Luminosity_UppercaseLetter_Is05() =>
        Assert.Equal(0.5, 'Ñ'.Luminosity());

    [Fact]
    public void Luminosity_ControlChar_IsZero() =>
        Assert.Equal(0.0, '\n'.Luminosity());

    // Luminosity — all printable ASCII in [0.0, 1.0]

    [Fact]
    public void Luminosity_AllPrintableAscii_InRange() =>
        Assert.All(
            Enumerable.Range(32, 95).Select(i => (char)i),
            c => Assert.InRange(c.Luminosity(), 0.0, 1.0));

    // Luminosity — monotonicity: space < thin < normal < heavy < block

    [Fact]
    public void Luminosity_Ordering() =>
        Assert.True(
            ' '.Luminosity() < 'i'.Luminosity() &&
            'i'.Luminosity() < 'a'.Luminosity() &&
            'a'.Luminosity() < 'W'.Luminosity() &&
            'W'.Luminosity() < '█'.Luminosity());
}
