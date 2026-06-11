using AboExtensions.Exceptions;

namespace AboExtensions.Tests;

public class TestExceptionExtensions
{
    // GetRootCause

    [Fact]
    public void GetRootCause_NoInner_ReturnsSelf()
    {
        var ex = new Exception("only");
        Assert.Same(ex, ex.GetRootCause());
    }

    [Fact]
    public void GetRootCause_OneLevel_ReturnsInner()
    {
        var inner = new Exception("inner");
        var outer = new Exception("outer", inner);
        Assert.Same(inner, outer.GetRootCause());
    }

    [Fact]
    public void GetRootCause_ThreeLevels_ReturnsDeepest()
    {
        var root = new Exception("root");
        var mid = new Exception("mid", root);
        var outer = new Exception("outer", mid);
        Assert.Same(root, outer.GetRootCause());
        Assert.Equal("root", outer.GetRootCause().Message);
    }

    // Flatten

    [Fact]
    public void Flatten_NoInner_ReturnsSingleElement()
    {
        var ex = new Exception("only");
        var flat = ex.Flatten().ToList();
        Assert.Single(flat);
        Assert.Same(ex, flat[0]);
    }

    [Fact]
    public void Flatten_TwoLevels_ReturnsBoth()
    {
        var inner = new Exception("inner");
        var outer = new Exception("outer", inner);
        var flat = outer.Flatten().ToList();
        Assert.Equal(2, flat.Count);
        Assert.Same(outer, flat[0]);
        Assert.Same(inner, flat[1]);
    }

    [Fact]
    public void Flatten_ThreeLevels_ReturnsAllInOrder()
    {
        var root = new Exception("root");
        var mid = new Exception("mid", root);
        var outer = new Exception("outer", mid);
        var messages = outer.Flatten().Select(e => e.Message).ToList();
        Assert.Equal(new[] { "outer", "mid", "root" }, messages);
    }

    [Fact]
    public void Flatten_FirstIsAlwaysOuter()
    {
        var outer = new Exception("outer", new Exception("inner"));
        Assert.Equal("outer", outer.Flatten().First().Message);
    }

    [Fact]
    public void Flatten_LastIsAlwaysRootCause()
    {
        var root = new Exception("root");
        var outer = new Exception("outer", new Exception("mid", root));
        Assert.Same(root, outer.Flatten().Last());
    }

    // IsOfType

    [Fact]
    public void IsOfType_ExactType_ReturnsTrue()
    {
        var ex = new ArgumentNullException();
        Assert.True(ex.IsOfType<ArgumentNullException>());
    }

    [Fact]
    public void IsOfType_BaseType_ReturnsTrue()
    {
        var ex = new ArgumentNullException();
        Assert.True(ex.IsOfType<ArgumentException>());
    }

    [Fact]
    public void IsOfType_DifferentType_ReturnsFalse()
    {
        var ex = new ArgumentException();
        Assert.False(ex.IsOfType<InvalidOperationException>());
    }

    [Fact]
    public void IsOfType_BaseNotAssignableFromDerived_ReturnsFalse()
    {
        var ex = new ArgumentException();
        Assert.False(ex.IsOfType<ArgumentNullException>());
    }

    // ToLogString

    [Fact]
    public void ToLogString_ContainsTypeName()
    {
        var ex = new ArgumentNullException("param");
        var s = ex.ToLogString();
        Assert.Contains("[ArgumentNullException]", s);
    }

    [Fact]
    public void ToLogString_ContainsMessage()
    {
        var ex = new Exception("oops");
        var s = ex.ToLogString();
        Assert.Contains("oops", s);
    }

    [Fact]
    public void ToLogString_WithInner_ContainsBothTypeNames()
    {
        var inner = new InvalidOperationException("inner msg");
        var outer = new Exception("outer msg", inner);
        var s = outer.ToLogString();
        Assert.Contains("[Exception]", s);
        Assert.Contains("[InvalidOperationException]", s);
    }

    [Fact]
    public void ToLogString_WithInner_ContainsBothMessages()
    {
        var inner = new InvalidOperationException("inner msg");
        var outer = new Exception("outer msg", inner);
        var s = outer.ToLogString();
        Assert.Contains("outer msg", s);
        Assert.Contains("inner msg", s);
    }

    [Fact]
    public void ToLogString_WithInner_InnerMarkedWithArrow()
    {
        var inner = new Exception("inner");
        var outer = new Exception("outer", inner);
        var s = outer.ToLogString();
        Assert.Contains("--->", s);
    }

    [Fact]
    public void ToLogString_OuterAppearsBeforeInner()
    {
        var inner = new Exception("inner");
        var outer = new Exception("outer", inner);
        var s = outer.ToLogString();
        Assert.True(s.IndexOf("outer") < s.IndexOf("inner"));
    }
}
