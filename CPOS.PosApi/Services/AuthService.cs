using System.Data;
using CPOS.PosApi.Data;
using CPOS.PosApi.Models.Responses;
using CPOS.PosApi.Security;
using Microsoft.Data.SqlClient;

namespace CPOS.PosApi.Services;

public sealed class AuthService
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ILegacyPasswordHasher _passwordHasher;
    private readonly IApiTokenService _tokenService;

    public AuthService(ISqlConnectionFactory connectionFactory, ILegacyPasswordHasher passwordHasher, IApiTokenService tokenService)
    {
        _connectionFactory = connectionFactory;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(string password, CancellationToken cancellationToken)
    {
        string encryptedPassword = _passwordHasher.Encrypt(password);

        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT TOP 1 user_id, UserName, isAdmin, is_Allow, AG_ID, Tr_ID
FROM dbo.Users
WHERE UserPass = @UserPass";
        cmd.Parameters.Add("@UserPass", SqlDbType.NVarChar).Value = encryptedPassword;

        await cn.OpenAsync(cancellationToken);
        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);

        if (await dr.ReadAsync(cancellationToken) == false)
        {
            return null;
        }

        LoginResponse response = new()
        {
            UserId = GetInt(dr, "user_id"),
            UserName = GetString(dr, "UserName"),
            IsAdmin = GetBool(dr, "isAdmin"),
            IsAllow = GetBool(dr, "is_Allow"),
            AgentId = GetNullableInt(dr, "AG_ID"),
            TreasuryId = GetNullableInt(dr, "Tr_ID")
        };

        if (response.IsAllow)
        {
            ApiTokenResult token = _tokenService.CreateToken(response);
            response.Token = token.Token;
            response.ExpiresAt = token.ExpiresAt;
        }

        return response;
    }

    private static int GetInt(SqlDataReader dr, string name)
    {
        object value = dr[name];
        return value is DBNull ? 0 : Convert.ToInt32(value);
    }

    private static int? GetNullableInt(SqlDataReader dr, string name)
    {
        object value = dr[name];
        if (value is DBNull) return null;

        int intValue = Convert.ToInt32(value);
        return intValue == 0 ? null : intValue;
    }

    private static string GetString(SqlDataReader dr, string name)
    {
        object value = dr[name];
        return value is DBNull ? "" : value.ToString() ?? "";
    }

    private static bool GetBool(SqlDataReader dr, string name)
    {
        object value = dr[name];
        if (value is DBNull) return false;
        if (value is bool boolValue) return boolValue;
        return Convert.ToInt32(value) != 0;
    }
}
