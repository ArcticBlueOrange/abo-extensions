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
}
