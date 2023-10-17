using System.Security.Cryptography;
using System.Text;
#pragma warning disable SYSLIB0021

namespace NeZoviReg.Domain.Extensions;

public static class Helpers
{
    private const string hash = "A!9NeZovi%XjjYY4YP2@Nob009X";

    public static string Encode(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    public static string Decode(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));

    public static string Encrypt(this string value)
    {
        byte[] data = Encoding.UTF8.GetBytes(value);
        using var md5 = MD5.Create();
        byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
        var transform = new TripleDESCryptoServiceProvider
        {
            Key = keys,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        }.CreateEncryptor();
        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        return Convert.ToBase64String(results, 0, results.Length);
    }

    public static string Decrypt(this string value)
    {
        byte[] data = Convert.FromBase64String(value);
        using var md5 = MD5.Create();
        byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
        var transform = new TripleDESCryptoServiceProvider
        {
            Key = keys,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        }.CreateDecryptor();
        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        return Encoding.UTF8.GetString(results);
    }
}