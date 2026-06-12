using AboExtensions.ComplexNumbers;
using System.Numerics;

namespace AboExtensions.Tests;

public class TestComplexNumbers
{
    [Theory]
    [InlineData(1, 2, "1+2i")]
    [InlineData(1, -2, "1-2i")]
    [InlineData(0, 2, "2i")]
    [InlineData(1, 0, "1")]
    [InlineData(0, 0, "0")]
    [InlineData(0, 1, "i")]
    [InlineData(0, -1, "-i")]
    [InlineData(1, 1, "1+i")]
    [InlineData(1, -1, "1-i")]
    [InlineData(-1, 2, "-1+2i")]
    public void TestToMathString(double real, double imaginary, string expected)
    {
        var c = new Complex(real, imaginary);
        Assert.Equal(expected, c.ToMathString());
    }

    // NthRoots

    private static void AssertComplex(Complex expected, Complex actual, double tolerance = 1e-10)
    {
        Assert.True(Math.Abs(expected.Real - actual.Real) < tolerance,
            $"Real: expected {expected.Real}, got {actual.Real}");
        Assert.True(Math.Abs(expected.Imaginary - actual.Imaginary) < tolerance,
            $"Imaginary: expected {expected.Imaginary}, got {actual.Imaginary}");
    }

    [Fact]
    public void NthRoots_Count_EqualsN()
    {
        Assert.Equal(5, Complex.One.NthRoots(5).Count());
    }

    [Fact]
    public void NthRoots_SquareRootsOfOne_Returns1AndMinus1()
    {
        var roots = Complex.One.NthRoots(2).ToList();
        AssertComplex(Complex.One, roots[0]);
        AssertComplex(new Complex(-1, 0), roots[1]);
    }

    [Fact]
    public void NthRoots_SquareRootsOfMinusOne_ReturnsIAndMinusI()
    {
        var roots = new Complex(-1, 0).NthRoots(2).ToList();
        AssertComplex(Complex.ImaginaryOne, roots[0]);
        AssertComplex(-Complex.ImaginaryOne, roots[1]);
    }

    [Fact]
    public void NthRoots_FourthRootsOfOne_Returns1IMinusOneMinusI()
    {
        var roots = Complex.One.NthRoots(4).ToList();
        AssertComplex(new Complex(1, 0), roots[0]);
        AssertComplex(new Complex(0, 1), roots[1]);
        AssertComplex(new Complex(-1, 0), roots[2]);
        AssertComplex(new Complex(0, -1), roots[3]);
    }

    [Fact]
    public void NthRoots_AllRootsMagnitudeEqualsExpected()
    {
        var c = new Complex(8, 0);
        var roots = c.NthRoots(3).ToList();
        foreach (var root in roots)
            Assert.Equal(2.0, root.Magnitude, 10);
    }

    [Fact]
    public void NthRoots_EachRootRaisedToN_EqualsOriginal()
    {
        var c = new Complex(3, 4);
        var roots = c.NthRoots(3).ToList();
        foreach (var root in roots)
            AssertComplex(c, Complex.Pow(root, 3));
    }

    // NthRootsOfUnity

    [Fact]
    public void NthRootsOfUnity_Count_EqualsN()
    {
        Assert.Equal(6, ComplexExtensions.NthRootsOfUnity(6).Count());
    }

    [Fact]
    public void NthRootsOfUnity_AllHaveMagnitudeOne()
    {
        foreach (var root in ComplexExtensions.NthRootsOfUnity(8))
            Assert.Equal(1.0, root.Magnitude, 10);
    }

    [Fact]
    public void NthRootsOfUnity_FirstRootIsAlwaysOne()
    {
        foreach (var n in new[] { 1, 2, 3, 4, 5, 6 })
            AssertComplex(Complex.One, ComplexExtensions.NthRootsOfUnity(n).First());
    }

    [Fact]
    public void NthRootsOfUnity_N1_ReturnsOne()
    {
        var roots = ComplexExtensions.NthRootsOfUnity(1).ToList();
        Assert.Single(roots);
        AssertComplex(Complex.One, roots[0]);
    }

    [Fact]
    public void NthRootsOfUnity_N2_Returns1AndMinus1()
    {
        var roots = ComplexExtensions.NthRootsOfUnity(2).ToList();
        AssertComplex(new Complex(1, 0), roots[0]);
        AssertComplex(new Complex(-1, 0), roots[1]);
    }

    // NthRoots with c = (1, 1)

    [Fact]
    public void NthRoots_1PlusI_SquareRoots_Count()
    {
        Assert.Equal(2, new Complex(1, 1).NthRoots(2).Count());
    }

    [Fact]
    public void NthRoots_1PlusI_SquareRoots_EachRaisedTo2_EqualsC()
    {
        var c = new Complex(1, 1);
        foreach (var root in c.NthRoots(2))
            AssertComplex(c, Complex.Pow(root, 2));
    }

    [Fact]
    public void NthRoots_1PlusI_SquareRoots_MagnitudeIsSqrt2ToThe1Over2()
    {
        var expected = Math.Pow(Math.Sqrt(2), 0.5);
        foreach (var root in new Complex(1, 1).NthRoots(2))
            Assert.Equal(expected, root.Magnitude, 10);
    }

    [Fact]
    public void NthRoots_1PlusI_FourthRoots_EachRaisedTo4_EqualsC()
    {
        var c = new Complex(1, 1);
        foreach (var root in c.NthRoots(4))
            AssertComplex(c, Complex.Pow(root, 4));
    }

    [Fact]
    public void NthRoots_1PlusI_RootsAreDistinct()
    {
        var roots = new Complex(1, 1).NthRoots(3).ToList();
        for (int i = 0; i < roots.Count; i++)
            for (int j = i + 1; j < roots.Count; j++)
                Assert.False(
                    Math.Abs(roots[i].Real - roots[j].Real) < 1e-10 &&
                    Math.Abs(roots[i].Imaginary - roots[j].Imaginary) < 1e-10);
    }

    // Rotate

    [Fact]
    public void Rotate_ByZero_ReturnsSameComplex()
    {
        var c = new Complex(3, 4);
        AssertComplex(c, c.Rotate(0));
    }

    [Fact]
    public void Rotate_RealUnit_ByPiOver2_ReturnsI()
    {
        AssertComplex(Complex.ImaginaryOne, Complex.One.Rotate(Math.PI / 2));
    }

    [Fact]
    public void Rotate_RealUnit_ByPi_ReturnsMinusOne()
    {
        AssertComplex(new Complex(-1, 0), Complex.One.Rotate(Math.PI));
    }

    [Fact]
    public void Rotate_By2Pi_ReturnsSameComplex()
    {
        var c = new Complex(2, 3);
        AssertComplex(c, c.Rotate(2 * Math.PI));
    }

    [Fact]
    public void Rotate_PreservesMagnitude()
    {
        var c = new Complex(3, 4);
        Assert.Equal(c.Magnitude, c.Rotate(1.23).Magnitude, 10);
    }

    [Fact]
    public void Rotate_RealUnit_ByPiOver4_KnownValue()
    {
        var expected = new Complex(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2);
        AssertComplex(expected, Complex.One.Rotate(Math.PI / 4));
    }

    [Theory]
    [InlineData(1, 0, 0, 1, 0)]   // nessuna rotazione
    [InlineData(1, 0, 0.5, 0, 1)]   // π/2 → i
    [InlineData(1, 0, 1, -1, 0)]   // π → -1
    [InlineData(1, 0, 1.5, 0, -1)]   // 3π/2 → -i
    [InlineData(0, 1, 0.5, -1, 0)]   // i ruotato di π/2 → -1
    [InlineData(2, 0, 0.5, 0, 2)]   // magnitudine conservata
    public void Rotate_Theory(double inR, double inI, double piMultiple, double expR, double expI)
    {
        AssertComplex(new Complex(expR, expI), new Complex(inR, inI).Rotate(piMultiple * Math.PI));
    }

    [Theory]
    [InlineData(1, 0, 2, 0, 1, 0)]   // √1,  k=0 →  1
    [InlineData(1, 0, 2, 1, -1, 0)]   // √1,  k=1 → -1
    [InlineData(-1, 0, 2, 0, 0, 1)]   // √-1, k=0 →  i
    [InlineData(-1, 0, 2, 1, 0, -1)]   // √-1, k=1 → -i
    [InlineData(1, 0, 4, 0, 1, 0)]   // ⁴√1, k=0 →  1
    [InlineData(1, 0, 4, 1, 0, 1)]   // ⁴√1, k=1 →  i
    [InlineData(1, 0, 4, 2, -1, 0)]   // ⁴√1, k=2 → -1
    [InlineData(1, 0, 4, 3, 0, -1)]   // ⁴√1, k=3 → -i
    public void NthRoots_Theory(double cR, double cI, int n, int k, double expR, double expI)
    {
        var root = new Complex(cR, cI).NthRoots(n).ElementAt(k);
        AssertComplex(new Complex(expR, expI), root);
    }
}
