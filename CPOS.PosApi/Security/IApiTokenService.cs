using CPOS.PosApi.Models.Responses;

namespace CPOS.PosApi.Security;

public interface IApiTokenService
{
    ApiTokenResult CreateToken(LoginResponse user);
    bool TryValidateToken(string token, out ApiUserContext? user);
}
