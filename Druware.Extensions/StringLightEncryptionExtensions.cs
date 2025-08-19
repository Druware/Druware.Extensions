using System.Security.Cryptography;
using System.Text;

namespace Druware.Extensions;

public static class StringLightEncryptionExtensions
{
    // WARNING: Being as this code is published publicly, it would be
    // advisable NOT to use this as is sits for anything that is truly
    // intended to be secure...
    private static readonly string Len = "4D584868CCA84221823D53D80DB30FCB";

    public static string Encrypt(this string value, string key, string iv)
    {
        byte[] keyBytes =
            Encoding.UTF8.GetBytes((key + Len).Substring(0, Len.Length));
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv.Substring(0, 16));

        using var aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = ivBytes;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor,
                   CryptoStreamMode.Write))
        {
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(value);
            }
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(this string cipherText, string key, string iv)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes((key + Len).Substring(0, Len.Length));
        aes.IV = Encoding.UTF8.GetBytes(iv.Substring(0, 16));

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream =
            new MemoryStream(Convert.FromBase64String(cipherText));
        using var cryptoStream =
            new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }

}
