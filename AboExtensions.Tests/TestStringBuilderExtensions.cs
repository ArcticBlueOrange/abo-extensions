using System.Text;
using AboExtensions.StringBuilders;

namespace AboExtensions.Tests;

public class TestStringBuilderExtensions
{
    // AppendIf

    [Fact]
    public void AppendIf_WhenTrue_AppendsText() =>
        Assert.Equal("hello", new StringBuilder().AppendIf(true, "hello").ToString());

    [Fact]
    public void AppendIf_WhenFalse_DoesNotAppend() =>
        Assert.Equal("", new StringBuilder().AppendIf(false, "hello").ToString());

    [Fact]
    public void AppendIf_Chaining() =>
        Assert.Equal("hello", new StringBuilder()
            .AppendIf(true, "hello")
            .AppendIf(false, " world")
            .ToString());

    [Fact]
    public void AppendIf_ReturnsBuilder() =>
        Assert.IsType<StringBuilder>(new StringBuilder().AppendIf(true, "x"));

    // AppendLineIf

    [Fact]
    public void AppendLineIf_WhenTrue_AppendsTextWithNewline() =>
        Assert.Equal("line1" + Environment.NewLine, new StringBuilder().AppendLineIf(true, "line1").ToString());

    [Fact]
    public void AppendLineIf_WhenFalse_DoesNotAppend() =>
        Assert.Equal("", new StringBuilder().AppendLineIf(false, "line1").ToString());

    [Fact]
    public void AppendLineIf_Chaining() =>
        Assert.Equal("line1" + Environment.NewLine, new StringBuilder()
            .AppendLineIf(true, "line1")
            .AppendLineIf(false, "line2")
            .ToString());

    [Fact]
    public void AppendLineIf_ReturnsBuilder() =>
        Assert.IsType<StringBuilder>(new StringBuilder().AppendLineIf(true, "x"));

    // Prepend

    [Fact]
    public void Prepend_InsertsAtStart() =>
        Assert.Equal("worldhello", new StringBuilder("hello").Prepend("world").ToString());

    [Fact]
    public void Prepend_OnEmpty_AppendsText() =>
        Assert.Equal("hello", new StringBuilder().Prepend("hello").ToString());

    [Fact]
    public void Prepend_Chaining() =>
        Assert.Equal("AB", new StringBuilder("B").Prepend("A").ToString());

    [Fact]
    public void Prepend_ReturnsBuilder() =>
        Assert.IsType<StringBuilder>(new StringBuilder().Prepend("x"));

    // IsEmpty

    [Fact]
    public void IsEmpty_WhenEmpty_ReturnsTrue() =>
        Assert.True(new StringBuilder().IsEmpty());

    [Fact]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse() =>
        Assert.False(new StringBuilder("x").IsEmpty());

    [Fact]
    public void IsEmpty_AfterAppend_ReturnsFalse() =>
        Assert.False(new StringBuilder().Append("x").IsEmpty());
}
