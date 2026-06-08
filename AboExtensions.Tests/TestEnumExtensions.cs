using AboExtensions.Enums;

namespace AboExtensions.Tests;

public class TestEnumExtensions
{
    private enum Color { Red, Green, Blue }

    [Fact]
    public void GetValues_ReturnsAllValues()
    {
        var values = EnumExtensions.GetValues<Color>().ToList();
        Assert.Equal(3, values.Count);
        Assert.Contains(Color.Red, values);
        Assert.Contains(Color.Green, values);
        Assert.Contains(Color.Blue, values);
    }

    [Fact]
    public void GetValues_PreservesOrder()
    {
        var values = EnumExtensions.GetValues<Color>().ToList();
        Assert.Equal(new[] { Color.Red, Color.Green, Color.Blue }, values);
    }
}
