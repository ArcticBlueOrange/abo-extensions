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

    // Flatten

    [Fact]
    public void Flatten_TwoSubLists_ReturnsConcatenation() =>
        Assert.Equal(new[] { "a", "b", "c", "d" },
            new[] { new[] { "a", "b" }, new[] { "c", "d" } }.Flatten());

    [Fact]
    public void Flatten_EmptyOuter_ReturnsEmpty() =>
        Assert.Empty(Array.Empty<string[]>().Flatten());

    [Fact]
    public void Flatten_InnerEmpty_SkipsEmpty() =>
        Assert.Equal(new[] { "a", "b" },
            new[] { new[] { "a", "b" }, Array.Empty<string>() }.Flatten());

    [Fact]
    public void Flatten_AllEmpty_ReturnsEmpty() =>
        Assert.Empty(new[] { Array.Empty<string>(), Array.Empty<string>() }.Flatten());

    [Fact]
    public void Flatten_PreservesOrder() =>
        Assert.Equal(new[] { "1", "2", "3", "4", "5" },
            new[] { new[] { "1", "2" }, new[] { "3" }, new[] { "4", "5" } }.Flatten());

    [Fact]
    public void Flatten_ReturnsList() =>
        Assert.IsType<List<string>>(new[] { new[] { "x" } }.Flatten());

    // Frequencies

    [Fact]
    public void Frequencies_CountsOccurrences()
    {
        var freq = new[] { "a", "b", "a", "c", "a", "b" }.Frequencies();
        Assert.Equal(3, freq["a"]);
        Assert.Equal(2, freq["b"]);
        Assert.Equal(1, freq["c"]);
    }

    [Fact]
    public void Frequencies_AllUnique_EachCountIsOne()
    {
        var freq = new[] { 1, 2, 3 }.Frequencies();
        Assert.All(freq.Values, v => Assert.Equal(1, v));
    }

    [Fact]
    public void Frequencies_Empty_ReturnsEmptyDictionary() =>
        Assert.Empty(Array.Empty<string>().Frequencies());

    [Fact]
    public void Frequencies_SingleElement_CountIsOne()
    {
        var freq = new[] { "x" }.Frequencies();
        Assert.Equal(1, freq["x"]);
    }

    [Fact]
    public void Frequencies_AllSame_CountEqualsLength()
    {
        var freq = new[] { "z", "z", "z", "z" }.Frequencies();
        Assert.Single(freq);
        Assert.Equal(4, freq["z"]);
    }
}
