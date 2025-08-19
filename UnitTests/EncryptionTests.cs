using System.Reflection;
using System.Text;
using Druware.Extensions;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEncrypt()
    {
        var randomString = "77F7BBD8-B745-4431-8318-415F3BB1C5F2";
        var assembly = Assembly.GetExecutingAssembly(); 
        var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(assembly.FullName ?? "_NoNameProvided_"));
        key += randomString;
        key = key[..32];
        const string iv = "ASimpleIVOfSufficientLenght";

        const string toEncrypt = "The Quick Brown Fox Jumps Over The Lazy Dog";
        var encrypted = toEncrypt.Encrypt(key, iv);
        
        var decrypted = encrypted.Decrypt(key, iv);
        
        Assert.That(decrypted, Is.EqualTo(toEncrypt), "The encrypted and decrypted strings should be the same");
        Assert.Pass();
    }
}