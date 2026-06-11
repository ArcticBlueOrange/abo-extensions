using System.Security.Cryptography;
using System.Text;

namespace AboExtensions.Hashing;

public static class HashingExtensions
{
    public static string ToSha256(this string text) => Hash(text, SHA256.HashData);

    public static string ToSha512(this string text) => Hash(text, SHA512.HashData);

    public static string ToMd5(this string text) => Hash(text, MD5.HashData);

    private static string Hash(string text, Func<byte[], byte[]> hashFn)
        => hashFn(Encoding.UTF8.GetBytes(text)).ToHex();

    public static string ToSha256(this byte[] bytes) => Hash(bytes, SHA256.HashData);

    public static string ToSha512(this byte[] bytes) => Hash(bytes, SHA512.HashData);

    public static string ToMd5(this byte[] bytes) => Hash(bytes, MD5.HashData);

    private static string Hash(byte[] bytes, Func<byte[], byte[]> hashFn)
        => hashFn(bytes).ToHex();

    /// <summary>
    /// Converte un array di byte in stringa esadecimale lowercase.
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string ToHex(this byte[] b) => Convert.ToHexString(b).ToLowerInvariant();
    /// <summary>
    //  Converte una stringa esadecimale in array di byte.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] FromHex(this string s) => Convert.FromHexString(s);

    /// <summary>
    /// Codifica la stringa in Base64 (UTF-8).
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToBase64(this string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));

    /// <summary>
    /// Codifica i bytes in Base64 (UTF-8).
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToBase64(this byte[] b) => Convert.ToBase64String(b);

    // TODO: FromBase64(this string s) : string
    //   Descrizione: decodifica una stringa Base64 in testo (UTF-8).
    //   Lancia FormatException se la stringa non è Base64 valida.
    //   Esempi: "aGVsbG8=".FromBase64() → "hello"
}
