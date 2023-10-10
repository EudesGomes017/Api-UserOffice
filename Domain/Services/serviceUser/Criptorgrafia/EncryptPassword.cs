using System.Security.Cryptography;
using System.Text;

namespace Domain.Services.serviceUser.Criptorgrafia;

public class EncryptPassword
{

    private readonly string _chaveEncryptPassword;

    public EncryptPassword(string chaveEncryptPassword)
    {
        _chaveEncryptPassword = chaveEncryptPassword;
    }

    public string encrypt(string password)
    {
        var passwordAdicional = $"{password}{_chaveEncryptPassword}"; // senha mais a chave de encriptação

        var bytes = Encoding.UTF8.GetBytes(passwordAdicional);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);

        return StringToBytes(hashBytes);
    }

    public string encryptTeste(string password)
    {
        var passwordAdicional = $"{password}{"vJyf-9$27j#0"}"; // senha mais a chave de encriptação

        var bytes = Encoding.UTF8.GetBytes(passwordAdicional);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);

        return StringToBytes(hashBytes);
    }

    public string StringToBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}

