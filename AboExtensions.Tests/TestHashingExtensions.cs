using AboExtensions.Hashing;
using System.Text;

namespace AboExtensions.Tests;

public class TestHashingExtensions
{
    // ToSha256(string)

    [Fact]
    public void ToSha256_KnownValue_ReturnsExpected()
    {
        Assert.Equal("2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", "hello".ToSha256());
    }

    [Fact]
    public void ToSha256_EmptyString_ReturnsExpected()
    {
        Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", "".ToSha256());
    }

    [Fact]
    public void ToSha256_OutputLength_Is64()
    {
        Assert.Equal(64, "hello".ToSha256().Length);
    }

    [Fact]
    public void ToSha256_OutputIsLowercase()
    {
        var result = "Hello World".ToSha256();
        Assert.Equal(result, result.ToLowerInvariant());
    }

    [Fact]
    public void ToSha256_SameInput_SameOutput()
    {
        Assert.Equal("hello".ToSha256(), "hello".ToSha256());
    }

    [Fact]
    public void ToSha256_DifferentInputs_DifferentOutputs()
    {
        Assert.NotEqual("hello".ToSha256(), "world".ToSha256());
    }

    // ToSha512(string)

    [Fact]
    public void ToSha512_KnownValue_ReturnsExpected()
    {
        Assert.Equal("9b71d224bd62f3785d96d46ad3ea3d73319bfbc2890caadae2dff72519673ca72323c3d99ba5c11d7c7acc6e14b8c5da0c4663475c2e5c3adef46f73bcdec043", "hello".ToSha512());
    }

    [Fact]
    public void ToSha512_OutputLength_Is128()
    {
        Assert.Equal(128, "hello".ToSha512().Length);
    }

    [Fact]
    public void ToSha512_OutputIsLowercase()
    {
        var result = "Hello World".ToSha512();
        Assert.Equal(result, result.ToLowerInvariant());
    }

    // ToMd5(string)

    [Fact]
    public void ToMd5_KnownValue_ReturnsExpected()
    {
        Assert.Equal("5d41402abc4b2a76b9719d911017c592", "hello".ToMd5());
    }

    [Fact]
    public void ToMd5_EmptyString_ReturnsExpected()
    {
        Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", "".ToMd5());
    }

    [Fact]
    public void ToMd5_OutputLength_Is32()
    {
        Assert.Equal(32, "hello".ToMd5().Length);
    }

    [Fact]
    public void ToMd5_OutputIsLowercase()
    {
        var result = "Hello World".ToMd5();
        Assert.Equal(result, result.ToLowerInvariant());
    }

    // byte[] overloads — consistenza con string

    [Fact]
    public void ToSha256_Bytes_MatchesStringOverload()
    {
        var bytes = Encoding.UTF8.GetBytes("hello");
        Assert.Equal("hello".ToSha256(), bytes.ToSha256());
    }

    [Fact]
    public void ToSha512_Bytes_MatchesStringOverload()
    {
        var bytes = Encoding.UTF8.GetBytes("hello");
        Assert.Equal("hello".ToSha512(), bytes.ToSha512());
    }

    [Fact]
    public void ToMd5_Bytes_MatchesStringOverload()
    {
        var bytes = Encoding.UTF8.GetBytes("hello");
        Assert.Equal("hello".ToMd5(), bytes.ToMd5());
    }

    [Fact]
    public void ToSha256_Bytes_KnownValue()
    {
        Assert.Equal("039058c6f2c0cb492c533b0a4d14ef77cc0f78abccced5287d84a1a2011cfb81", new byte[] { 1, 2, 3 }.ToSha256());
    }
}
