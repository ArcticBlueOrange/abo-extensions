using AboExtensions.Reflections;

namespace AboExtensions.Tests;

public class TestReflections
{
    private record Person(string FirstName, string LastName, int Age);

    // GetPropertyByName

    [Fact]
    public void GetPropertyByName_ExistingStringProperty_ReturnsValue()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario", p.GetPropertyByName("FirstName"));
    }

    [Fact]
    public void GetPropertyByName_ExistingIntProperty_ReturnsValue()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal(30, p.GetPropertyByName("Age"));
    }

    [Fact]
    public void GetPropertyByName_NonExistingProperty_Throws() =>
        Assert.Throws<NullReferenceException>(() => new Person("Mario", "Rossi", 30).GetPropertyByName("Missing"));

    // GetPropertiesToString

    [Fact]
    public void GetPropertiesToString_SingleProperty_ReturnsValue()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario", ReflectionExtensions.GetPropertiesToString(p, "FirstName"));
    }

    [Fact]
    public void GetPropertiesToString_MultipleProperties_JoinsWithDefaultSeparator()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario Rossi", ReflectionExtensions.GetPropertiesToString(p, "FirstName,LastName"));
    }

    [Fact]
    public void GetPropertiesToString_MultipleProperties_CustomOutSeparator()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario-Rossi", ReflectionExtensions.GetPropertiesToString(p, "FirstName,LastName", outSep: '-'));
    }

    [Fact]
    public void GetPropertiesToString_MultipleProperties_CustomInSeparator()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario Rossi", ReflectionExtensions.GetPropertiesToString(p, "FirstName;LastName", inSep: ';'));
    }

    [Fact]
    public void GetPropertiesToString_PropNamesWithWhitespaceEntries_IgnoresBlanks()
    {
        var p = new Person("Mario", "Rossi", 30);
        Assert.Equal("Mario Rossi", ReflectionExtensions.GetPropertiesToString(p, "FirstName, ,LastName"));
    }
}
