using AboExtensions.Objects;

namespace AboExtensions.Tests;

public class TestObjectExtensions
{
    // IsNull / IsNotNull

    [Fact]
    public void IsNull_Null_ReturnsTrue() =>
        Assert.True(((string?)null).IsNull());

    [Fact]
    public void IsNull_NotNull_ReturnsFalse() =>
        Assert.False("hello".IsNull());

    [Fact]
    public void IsNotNull_NotNull_ReturnsTrue() =>
        Assert.True("hello".IsNotNull());

    [Fact]
    public void IsNotNull_Null_ReturnsFalse() =>
        Assert.False(((string?)null).IsNotNull());

    // In

    [Fact]
    public void In_ValuePresent_ReturnsTrue() =>
        Assert.True("b".In("a", "b", "c"));

    [Fact]
    public void In_ValueAbsent_ReturnsFalse() =>
        Assert.False("x".In("a", "b", "c"));

    [Fact]
    public void In_SingleMatch_ReturnsTrue() =>
        Assert.True(42.In(42));

    [Fact]
    public void In_EmptyArray_ReturnsFalse() =>
        Assert.False("a".In());

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    public void In_Int(int value, bool expected) =>
        Assert.Equal(expected, value.In(1, 2));

    // NotIn

    [Fact]
    public void NotIn_ValueAbsent_ReturnsTrue() =>
        Assert.True("x".NotIn("a", "b", "c"));

    [Fact]
    public void NotIn_ValuePresent_ReturnsFalse() =>
        Assert.False("b".NotIn("a", "b", "c"));

    [Fact]
    public void NotIn_EmptyArray_ReturnsTrue() =>
        Assert.True("a".NotIn());

    [Fact]
    public void NotIn_IsComplementOfIn()
    {
        var candidates = new[] { "a", "b", "c" };
        Assert.All(new[] { "a", "b", "x", "y" }, v =>
            Assert.NotEqual(v.In(candidates), v.NotIn(candidates)));
    }

    // Also

    [Fact]
    public void Also_ExecutesSideEffect()
    {
        var called = false;
        "hello".Also(_ => { called = true; return true; });
        Assert.True(called);
    }

    [Fact]
    public void Also_ReturnsOriginalObject()
    {
        var obj = "hello";
        var result = obj.Also(_ => true);
        Assert.Same(obj, result);
    }

    [Fact]
    public void Also_ReturnsOriginalEvenWhenPredicateFalse()
    {
        var result = "hello".Also(_ => false);
        Assert.Equal("hello", result);
    }

    [Fact]
    public void Also_Chaining()
    {
        var log = new List<string>();
        var result = "hello"
            .Also(s => { log.Add(s); return true; })
            .Also(s => { log.Add(s.ToUpper()); return true; });
        Assert.Equal("hello", result);
        Assert.Equal(new[] { "hello", "HELLO" }, log);
    }

    [Fact]
    public void Also_ReceivesCorrectValue()
    {
        string? received = null;
        "world".Also(s => { received = s; return true; });
        Assert.Equal("world", received);
    }

    // Let

    [Fact]
    public void Let_TransformsValue() =>
        Assert.Equal(5, "hello".Let(s => s.Length));

    [Fact]
    public void Let_ParseInt() =>
        Assert.Equal(42, "42".Let(int.Parse));

    [Fact]
    public void Let_TypeConversion() =>
        Assert.Equal("HELLO", "hello".Let(s => s.ToUpper()));

    [Fact]
    public void Let_Chaining() =>
        Assert.Equal(84, "42".Let(int.Parse).Let(n => n * 2));

    [Fact]
    public void Let_WorksOnValueType() =>
        Assert.Equal("5", 5.Let(n => n.ToString()));

    // In / NotIn — null values

    [Fact]
    public void In_NullValueInArray_ReturnsTrue() =>
        Assert.True(((string?)null).In(null, "a"));

    [Fact]
    public void In_NullValueNotInArray_ReturnsFalse() =>
        Assert.False(((string?)null).In("a", "b"));

    [Fact]
    public void NotIn_NullValueNotInArray_ReturnsTrue() =>
        Assert.True(((string?)null).NotIn("a", "b"));

    [Fact]
    public void NotIn_NullValueInArray_ReturnsFalse() =>
        Assert.False(((string?)null).NotIn(null, "a"));
}
