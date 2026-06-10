using AboExtensions.Randoms;

namespace AboExtensions.Tests;

public class TestRandomExtensions
{
    // NextBool()

    [Fact]
    public void NextBool_ReturnsBoolean()
    {
        var r = new Random(42);
        var result = r.NextBool();
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void NextBool_DistributionIsRoughly50Percent()
    {
        var r = new Random(42);
        var trueCount = Enumerable.Range(0, 10000).Count(_ => r.NextBool());
        Assert.InRange(trueCount, 4500, 5500);
    }

    // NextBool(probability)

    [Fact]
    public void NextBool_Probability1_AlwaysTrue()
    {
        var r = new Random(42);
        Assert.All(Enumerable.Range(0, 100), _ => Assert.True(r.NextBool(1.0)));
    }

    [Fact]
    public void NextBool_Probability0_AlwaysFalse()
    {
        var r = new Random(42);
        Assert.All(Enumerable.Range(0, 100), _ => Assert.False(r.NextBool(0.0)));
    }

    [Fact]
    public void NextBool_HighProbability_MostlyTrue()
    {
        var r = new Random(42);
        var trueCount = Enumerable.Range(0, 10000).Count(_ => r.NextBool(0.9));
        Assert.InRange(trueCount, 8700, 9300);
    }

    [Fact]
    public void NextBool_LowProbability_MostlyFalse()
    {
        var r = new Random(42);
        var trueCount = Enumerable.Range(0, 10000).Count(_ => r.NextBool(0.1));
        Assert.InRange(trueCount, 700, 1300);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(-1.0)]
    [InlineData(2.0)]
    public void NextBool_OutOfRangeProbability_Throws(double probability) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Random().NextBool(probability));

    // NextItem

    [Fact]
    public void NextItem_ReturnsElementFromList()
    {
        var list = new List<string> { "a", "b", "c" };
        var result = new Random(42).NextItem(list);
        Assert.Contains(result, list);
    }

    [Fact]
    public void NextItem_WithSingleElement_ReturnsThatElement() =>
        Assert.Equal("only", new Random(42).NextItem(new List<string> { "only" }));

    [Fact]
    public void NextItem_EmptyList_Throws() =>
        Assert.Throws<ArgumentException>(() => new Random().NextItem(new List<string>()));

    [Fact]
    public void NextItem_NeverReturnsOutOfBoundsElement()
    {
        var list = new List<int> { 10, 20, 30 };
        var r = new Random(0);
        Assert.All(Enumerable.Range(0, 1000), _ => Assert.Contains(r.NextItem(list), list));
    }

    // RandomItem

    [Fact]
    public void RandomItem_ReturnsElementFromList()
    {
        var list = new List<string> { "x", "y", "z" };
        Assert.Contains(list.RandomItem(), list);
    }

    [Fact]
    public void RandomItem_EmptyList_Throws() =>
        Assert.Throws<ArgumentException>(() => new List<string>().RandomItem());

    // NextEnum

    [Fact]
    public void NextEnum_ReturnsValidEnumValue()
    {
        var r = new Random(42);
        var result = r.NextEnum<DayOfWeek>();
        Assert.True(Enum.IsDefined(result));
    }

    [Fact]
    public void NextEnum_EventuallyReturnsAllValues()
    {
        var r = new Random(42);
        var seen = Enumerable.Range(0, 1000).Select(_ => r.NextEnum<DayOfWeek>()).Distinct().ToList();
        Assert.Equal(7, seen.Count);
    }

    // NextString

    [Fact]
    public void NextString_ReturnsCorrectLength() =>
        Assert.Equal(8, new Random(42).NextString(8).Length);

    [Fact]
    public void NextString_ReturnsCorrectLengthCustomChars() =>
        Assert.Equal(4, new Random(42).NextString(4, "AEIOU").Length);

    [Fact]
    public void NextString_OnlyContainsCharsFromPool()
    {
        var pool = "AEIOU";
        var result = new Random(42).NextString(100, pool);
        Assert.All(result, c => Assert.Contains(c, pool));
    }

    [Fact]
    public void NextString_ZeroLength_ReturnsEmptyString() =>
        Assert.Equal("", new Random(42).NextString(0));

    [Fact]
    public void NextString_DefaultPoolOnlyContainsAlphanumericAndSpecial()
    {
        var result = new Random(42).NextString(1000);
        Assert.All(result, c => Assert.True(char.IsLetterOrDigit(c) || c == '-' || c == '_'));
    }
}
