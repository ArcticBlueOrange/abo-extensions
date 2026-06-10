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
}
