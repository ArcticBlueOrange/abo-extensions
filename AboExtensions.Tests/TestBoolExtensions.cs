using AboExtensions.Booleans;

namespace AboExtensions.Tests;

public class TestBoolExtensions
{
    [Fact]
    public void Toggle_True_ReturnsFalse() => Assert.False(true.Toggle());

    [Fact]
    public void Toggle_False_ReturnsTrue() => Assert.True(false.Toggle());

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Toggle_IsOpposite(bool b) => Assert.Equal(b, !b.Toggle());
}
