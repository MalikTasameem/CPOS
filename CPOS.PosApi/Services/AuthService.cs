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
SELECT TOP 1 u.user_id, u.UserName, u.isAdmin, u.is_Allow, u.AG_ID, u.Tr_ID,
       CASE WHEN ISNULL(u.isAdmin, 0) = 1 THEN 1 ELSE ISNULL(v.SB_Show_Price_Info, 0) END AS SB_Show_Price_Info,
       CASE WHEN ISNULL(u.isAdmin, 0) = 1 THEN 1 ELSE ISNULL(v.Sell_Under_Min_SP, 0) END AS Sell_Under_Min_SP,
       CASE WHEN ISNULL(u.isAdmin, 0) = 1 THEN 1 ELSE ISNULL(v.Sell_Under_Min_SP_2, 0) END AS Sell_Under_Min_SP_2,
       ISNULL((SELECT TOP 1 Allow_MinSP FROM dbo.Sys_Features ORDER BY T_ID ASC), 0) AS Allow_MinSP
FROM dbo.Users u
LEFT JOIN dbo.Users_Validations_V v ON v.User_id = u.user_id
WHERE u.UserPass = @UserPass";
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
            CanUseSalesPriceInfo = GetBool(dr, "SB_Show_Price_Info"),
            CanSellWholesale = GetBool(dr, "Sell_Under_Min_SP"),
            CanSellWholesale2 = GetBool(dr, "Sell_Under_Min_SP_2"),
            IsMinSalesPriceEnabled = GetBool(dr, "Allow_MinSP"),
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
