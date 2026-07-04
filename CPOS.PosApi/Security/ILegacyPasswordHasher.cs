namespace CPOS.PosApi.Security;

public interface ILegacyPasswordHasher
{
    string Encrypt(string clearText);
}
