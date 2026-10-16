using System.Net;
using System.Net.Sockets;

namespace AboExtensions.IpAddresses;

public static class IpExtensions
{
    public static bool IsValidIp(this string s) => s.IsValidIpV4() || s.IsValidIpV6();

    public static bool IsValidIpV4(this string s) =>
        IPAddress.TryParse(s, out var ip)
        && ip.AddressFamily == AddressFamily.InterNetwork
        && ip.ToString() == s;

    public static bool IsValidIpV6(this string s) =>
        IPAddress.TryParse(s, out var ip)
        && ip.AddressFamily == AddressFamily.InterNetworkV6
        && !s.Contains('[');

    // TODO: IsPrivateIp(this string s) : bool
    //   Descrizione: true se l'IP è in un range privato (RFC 1918 per IPv4):
    //   10.0.0.0/8, 172.16.0.0/12, 192.168.0.0/16, loopback 127.0.0.0/8.
    //   Per IPv6: fc00::/7 (ULA) e ::1 (loopback).
    //   Esempi: "192.168.1.1".IsPrivateIp()  → true
    //           "10.0.0.1".IsPrivateIp()     → true
    //           "8.8.8.8".IsPrivateIp()      → false
    //           "::1".IsPrivateIp()           → true

    public static IPAddress? ToIpAddress(this string s)
    {
        if (IPAddress.TryParse(s, out var _out))
            return _out;
        return null;
    }
}
