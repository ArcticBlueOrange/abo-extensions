using AboExtensions.Dictionaries;

namespace AboExtensions.Tests;

public class TestDictionaryExtensions
{
    // Invert

    [Fact]
    public void Invert_SwapsKeysAndValues()
    {
        var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var inv = d.Invert();
        Assert.Equal("a", inv[1]);
        Assert.Equal("b", inv[2]);
    }

    [Fact]
    public void Invert_ResultHasSameCount()
    {
        var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };
        Assert.Equal(3, d.Invert().Count);
    }

    [Fact]
    public void Invert_EmptyDictionary_ReturnsEmpty() =>
        Assert.Empty(new Dictionary<string, int>().Invert());

    [Fact]
    public void Invert_DuplicateValues_ThrowsByDefault()
    {
        var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 1 };
        Assert.Throws<ArgumentException>(() => d.Invert());
    }

    [Fact]
    public void Invert_DuplicateValues_NoThrow_LastWins()
    {
        var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 1 };
        var inv = d.Invert(throwOnDuplicate: false);
        Assert.Single(inv);
        Assert.True(inv.ContainsKey(1));
    }

    [Fact]
    public void Invert_NullValue_Throws()
    {
        var d = new Dictionary<string, string?> { ["a"] = null };
        Assert.Throws<ArgumentNullException>(() => d.Invert());
    }

    // AddOrUpdate

    [Fact]
    public void AddOrUpdate_AddsNewKey()
    {
        var d = new Dictionary<string, int>();
        d.AddOrUpdate("x", 42);
        Assert.Equal(42, d["x"]);
    }

    [Fact]
    public void AddOrUpdate_UpdatesExistingKey()
    {
        var d = new Dictionary<string, int> { ["x"] = 1 };
        d.AddOrUpdate("x", 99);
        Assert.Equal(99, d["x"]);
    }

    [Fact]
    public void AddOrUpdate_ReturnsSameDictionary()
    {
        var d = new Dictionary<string, int>();
        var result = d.AddOrUpdate("x", 1);
        Assert.Same(d, result);
    }

    [Fact]
    public void AddOrUpdate_Chaining()
    {
        var d = new Dictionary<string, int>()
            .AddOrUpdate("a", 1)
            .AddOrUpdate("b", 2);
        Assert.Equal(1, d["a"]);
        Assert.Equal(2, d["b"]);
    }
}
