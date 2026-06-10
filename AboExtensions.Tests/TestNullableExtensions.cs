using AboExtensions.Nullables;

namespace AboExtensions.Tests;

public class TestNullableExtensions
{
    [Fact]
    public void TestIfNotNull_ReferenceType_Invokes()
    {
        string? value = "hello";
        var called = false;
        value.IfNotNull(v => { called = true; Assert.Equal("hello", v); });
        Assert.True(called);
    }

    [Fact]
    public void TestIfNotNull_ReferenceType_Null_DoesNotInvoke()
    {
        string? value = null;
        var called = false;
        value.IfNotNull(_ => called = true);
        Assert.False(called);
    }

    [Fact]
    public void TestIfNotNull_ValueType_Invokes()
    {
        int? value = 42;
        var called = false;
        value.IfNotNull(v => { called = true; Assert.Equal(42, v); });
        Assert.True(called);
    }

    [Fact]
    public void TestIfNotNull_ValueType_Null_DoesNotInvoke()
    {
        int? value = null;
        var called = false;
        value.IfNotNull(_ => called = true);
        Assert.False(called);
    }

    // MapNotNull

    [Fact]
    public void MapNotNull_NotNull_AppliesMap()
    {
        string? value = "hello";
        var result = value.MapNotNull(s => s.Length);
        Assert.Equal(5, result);
    }

    [Fact]
    public void MapNotNull_Null_ReturnsDefault()
    {
        string? value = null;
        var result = value.MapNotNull(s => s.Length);
        Assert.Equal(0, result);
    }

    [Fact]
    public void MapNotNull_Null_ReferenceResult_ReturnsNull()
    {
        string? value = null;
        var result = value.MapNotNull(s => s.ToUpper());
        Assert.Null(result);
    }

    [Fact]
    public void MapNotNull_NotNull_TransformsValue()
    {
        string? value = "world";
        var result = value.MapNotNull(s => s.ToUpper());
        Assert.Equal("WORLD", result);
    }

    [Fact]
    public void MapNotNull_Chaining()
    {
        string? value = "42";
        var result = value.MapNotNull(int.Parse).MapNotNull(n => n * 2);
        Assert.Equal(84, result);
    }
}
