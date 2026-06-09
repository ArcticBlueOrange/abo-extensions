using AboExtensions.Guids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboExtensions.Tests;

public class TestGuids
{
    static Guid MakeGuid(bool empty) => empty ? Guid.Empty : Guid.NewGuid();

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void TestIsEmpty(bool empty, bool expected)
    {
        var test = MakeGuid(empty);
        Assert.Equal(test.IsEmpty(), expected);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void TestIsNotEmpty(bool empty, bool expected)
    {
        var test = MakeGuid(empty);
        Assert.Equal(test.IsNotEmpty(), expected);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void TestOrNew(bool makeEmpty, bool shouldEqual)
    {
        var _old = MakeGuid(makeEmpty);
        var _new = _old.OrNew();
        if (shouldEqual)
            Assert.Equal(_new, _old);
        else
            Assert.NotEqual(_new, _old);
    }

}
