using AboExtensions.Lists;

namespace AboExtensions.Tests;

public class TestListExtensions
{
    // None

    [Fact]
    public void None_EmptyList_ReturnsTrue() =>
        Assert.True(Array.Empty<int>().None(x => x > 0));

    [Fact]
    public void None_NoElementMatchesPredicate_ReturnsTrue() =>
        Assert.True(new[] { 1, 2, 3 }.None(x => x > 10));

    [Fact]
    public void None_SomeElementsMatchPredicate_ReturnsFalse() =>
        Assert.False(new[] { 1, 2, 3 }.None(x => x > 2));

    [Fact]
    public void None_AllElementsMatchPredicate_ReturnsFalse() =>
        Assert.False(new[] { 5, 6, 7 }.None(x => x > 0));

    // IsNullOrEmpty

    [Fact]
    public void IsNullOrEmpty_NullList_ReturnsTrue() =>
        Assert.True(((IEnumerable<int>?)null).IsNullOrEmpty());

    [Fact]
    public void IsNullOrEmpty_EmptyList_ReturnsTrue() =>
        Assert.True(Array.Empty<int>().IsNullOrEmpty());

    [Fact]
    public void IsNullOrEmpty_NonEmptyList_ReturnsFalse() =>
        Assert.False(new[] { 1 }.IsNullOrEmpty());
}
