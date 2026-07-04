using System.Security.Cryptography;
using System.Text;

namespace CPOS.PosApi.Security;

public sealed class LegacyPasswordHasher : ILegacyPasswordHasher
{
    private static readonly byte[] Salt =
    {
        0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D,
        0x65, 0x64, 0x76, 0x65, 0x64, 0x65,
        0x76
    };

    public string Encrypt(string clearText)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

        using Aes encryptor = Aes.Create();
        using Rfc2898DeriveBytes pdb = new("Seraj94", Salt, 1000, HashAlgorithmName.SHA1);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
        }

        return Convert.ToBase64String(ms.ToArray());
    }
}
