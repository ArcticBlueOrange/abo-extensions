using System.Net;

namespace AboExtensions.IpAddresses;

public static class IpExtensions
{
    // TODO: IsValidIp(this string s) : bool
    //   Descrizione: true se la stringa è un indirizzo IP valido (IPv4 o IPv6).
    //   Usa IPAddress.TryParse — zero deps, nessuna regex.
    //   Esempi: "192.168.1.1".IsValidIp()    → true
    //           "::1".IsValidIp()             → true   (IPv6 loopback)
    //           "256.0.0.1".IsValidIp()       → false
    //           "hello".IsValidIp()           → false
    //           "".IsValidIp()                → false

    // TODO: IsValidIpv4(this string s) : bool
    //   Descrizione: true solo se è un indirizzo IPv4 valido.
    //   Esempi: "192.168.1.1".IsValidIpv4()  → true
    //           "::1".IsValidIpv4()           → false

    // TODO: IsValidIpv6(this string s) : bool
    //   Descrizione: true solo se è un indirizzo IPv6 valido.
    //   Esempi: "::1".IsValidIpv6()           → true
    //           "192.168.1.1".IsValidIpv6()   → false

    // TODO: IsPrivateIp(this string s) : bool
    //   Descrizione: true se l'IP è in un range privato (RFC 1918 per IPv4):
    //   10.0.0.0/8, 172.16.0.0/12, 192.168.0.0/16, loopback 127.0.0.0/8.
    //   Per IPv6: fc00::/7 (ULA) e ::1 (loopback).
    //   Esempi: "192.168.1.1".IsPrivateIp()  → true
    //           "10.0.0.1".IsPrivateIp()     → true
    //           "8.8.8.8".IsPrivateIp()      → false
    //           "::1".IsPrivateIp()           → true

    // TODO: ToIpAddress(this string s) : IPAddress?
    //   Descrizione: converte la stringa in IPAddress, restituisce null se non valida.
    //   Alternativa null-safe a IPAddress.Parse.
    //   Esempi: "192.168.1.1".ToIpAddress()  → IPAddress { 192.168.1.1 }
    //           "invalid".ToIpAddress()       → null
}
