using AboExtensions.Lists;

namespace AboExtensions.Tests;

public class TestListExtensions
{
    // ForEach

    [Fact]
    public void ForEach_InvokesActionForEachElement()
    {
        var result = new List<int>();
        new[] { 1, 2, 3 }.ForEach(result.Add);
        Assert.Equal(new[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void ForEach_EmptySource_DoesNotInvoke()
    {
        var called = false;
        Array.Empty<int>().ForEach(_ => called = true);
        Assert.False(called);
    }

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

    // Batch

    [Fact]
    public void Batch_EvenSplit()
    {
        var result = new[] { 1, 2, 3, 4 }.Batch(2).ToList();
        Assert.Equal(2, result.Count);
        Assert.Equal(new[] { 1, 2 }, result[0]);
        Assert.Equal(new[] { 3, 4 }, result[1]);
    }

    [Fact]
    public void Batch_UnevenSplit_LastBatchSmaller()
    {
        var result = new[] { 1, 2, 3, 4, 5 }.Batch(2).ToList();
        Assert.Equal(3, result.Count);
        Assert.Equal(new[] { 5 }, result[2]);
    }

    [Fact]
    public void Batch_SizeGreaterThanList_ReturnsSingleBatch()
    {
        var result = new[] { 1, 2 }.Batch(10).ToList();
        Assert.Single(result);
        Assert.Equal(new[] { 1, 2 }, result[0]);
    }

    [Fact]
    public void Batch_EmptySource_ReturnsEmpty() =>
        Assert.Empty(Array.Empty<int>().Batch(3));

    [Fact]
    public void Batch_InvalidSize_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new[] { 1 }.Batch(0).ToList());

    // Shuffle

    [Fact]
    public void Shuffle_ReturnsSameElements()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        var result = list.Shuffle();
        Assert.Equal(list.OrderBy(x => x), result.OrderBy(x => x));
    }

    [Fact]
    public void Shuffle_ReturnsSameCount()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        Assert.Equal(list.Count, list.Shuffle().Count);
    }

    [Fact]
    public void Shuffle_DoesNotMutateOriginal()
    {
        var list = new List<int> { 1, 2, 3 };
        var original = list.ToList();
        list.Shuffle();
        Assert.Equal(original, list);
    }

    [Fact]
    public void Shuffle_EmptyList_ReturnsEmpty() =>
        Assert.Empty(new List<int>().Shuffle());

    // WhereNotNull (reference types)

    [Fact]
    public void WhereNotNull_FiltersNullStrings() =>
        Assert.Equal(new[] { "a", "b" }, new[] { "a", null, "b" }.WhereNotNull());

    [Fact]
    public void WhereNotNull_AllNull_ReturnsEmpty() =>
        Assert.Empty(new string?[] { null, null }.WhereNotNull());

    [Fact]
    public void WhereNotNull_NoNull_ReturnsAll() =>
        Assert.Equal(new[] { "x", "y" }, new[] { "x", "y" }.WhereNotNull());

    [Fact]
    public void WhereNotNull_EmptySource_ReturnsEmpty() =>
        Assert.Empty(Array.Empty<string?>().WhereNotNull());

    // WhereNotNull (value types)

    [Fact]
    public void WhereNotNull_FiltersNullInts() =>
        Assert.Equal(new[] { 1, 3 }, new int?[] { 1, null, 3 }.WhereNotNull());

    [Fact]
    public void WhereNotNull_AllNullInts_ReturnsEmpty() =>
        Assert.Empty(new int?[] { null, null }.WhereNotNull());

    [Fact]
    public void WhereNotNull_NoNullInts_ReturnsAll() =>
        Assert.Equal(new[] { 1, 2 }, new int?[] { 1, 2 }.WhereNotNull());

    [Fact]
    public void WhereNotNull_EmptyIntSource_ReturnsEmpty() =>
        Assert.Empty(Array.Empty<int?>().WhereNotNull());
}
