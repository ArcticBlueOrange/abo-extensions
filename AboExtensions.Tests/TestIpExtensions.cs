using System.Net;
using AboExtensions.IpAddresses;

namespace AboExtensions.Tests;

public class TestIpExtensions
{
    // IsValidIp

    [Theory]
    [InlineData("192.168.1.1", true)]
    [InlineData("0.0.0.0", true)]
    [InlineData("255.255.255.255", true)]
    [InlineData("::1", true)]
    [InlineData("2001:db8::1", true)]
    [InlineData("256.0.0.1", false)]
    [InlineData("hello", false)]
    [InlineData("", false)]
    [InlineData("192.168.1.1.1", false)]
    public void TestIsValidIp(string input, bool expected) =>
        Assert.Equal(expected, input.IsValidIp());

    // IsValidIpV4

    [Theory]
    [InlineData("192.168.1.1", true)]
    [InlineData("0.0.0.0", true)]
    [InlineData("255.255.255.255", true)]
    [InlineData("127.0.0.1", true)]
    [InlineData("::1", false)]
    [InlineData("2001:db8::1", false)]
    [InlineData("256.0.0.1", false)]
    [InlineData("hello", false)]
    [InlineData("", false)]
    public void TestIsValidIpV4(string input, bool expected) =>
        Assert.Equal(expected, input.IsValidIpV4());

    // IsValidIpV6

    [Theory]
    [InlineData("::1", true)]
    [InlineData("2001:db8::1", true)]
    [InlineData("::", true)]
    [InlineData("fe80::1", true)]
    [InlineData("192.168.1.1", false)]
    [InlineData("0.0.0.0", false)]
    [InlineData("hello", false)]
    [InlineData("", false)]
    public void TestIsValidIpV6(string input, bool expected) =>
        Assert.Equal(expected, input.IsValidIpV6());

    // ToIpAddress

    [Fact]
    public void ToIpAddress_ValidIpV4_ReturnsIpAddress() =>
        Assert.Equal(IPAddress.Parse("192.168.1.1"), "192.168.1.1".ToIpAddress());

    [Fact]
    public void ToIpAddress_ValidIpV6_ReturnsIpAddress() =>
        Assert.Equal(IPAddress.Parse("::1"), "::1".ToIpAddress());

    [Fact]
    public void ToIpAddress_Invalid_ReturnsNull() =>
        Assert.Null("invalid".ToIpAddress());

    [Fact]
    public void ToIpAddress_Empty_ReturnsNull() =>
        Assert.Null("".ToIpAddress());

    [Fact]
    public void ToIpAddress_ReturnsCorrectType() =>
        Assert.IsType<IPAddress>("10.0.0.1".ToIpAddress()!);

    // Bug repro: IPv4/IPv6 detection based on '.' / ':'

    [Theory]
    [InlineData("::ffff:1.2.3.4")]
    [InlineData("127.1")]
    public void IsValidIpV4_NotAStandardIpV4_ReturnsFalse(string input) =>
        Assert.False(input.IsValidIpV4());

    [Fact]
    public void IsValidIp_SingleNumber_IsNotValidIpV4NorIpV6()
    {
        Assert.True("1".IsValidIp());
        Assert.True("1".IsValidIpV4() || "1".IsValidIpV6(),
            "IsValidIp accepts \"1\" but neither IsValidIpV4 nor IsValidIpV6 does");
    }
}
