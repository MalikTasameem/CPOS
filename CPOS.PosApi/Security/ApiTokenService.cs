using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CPOS.PosApi.Models.Responses;

namespace CPOS.PosApi.Security;

public sealed class ApiTokenService : IApiTokenService
{
    private readonly byte[] _secret;
    private readonly int _expiresMinutes;

    public ApiTokenService(IConfiguration configuration)
    {
        string secret = configuration["ApiToken:Secret"] ?? "";
        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new InvalidOperationException("ApiToken:Secret is missing.");
        }

        _secret = Encoding.UTF8.GetBytes(secret);
        _expiresMinutes = int.TryParse(configuration["ApiToken:ExpiresMinutes"], out int value) && value > 0
            ? value
            : 720;
    }

    public ApiTokenResult CreateToken(LoginResponse user)
    {
        DateTimeOffset expiresAt = DateTimeOffset.UtcNow.AddMinutes(_expiresMinutes);
        ApiTokenPayload payload = new()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            IsAdmin = user.IsAdmin,
            AgentId = user.AgentId,
            TreasuryId = user.TreasuryId,
            ExpiresAtUnix = expiresAt.ToUnixTimeSeconds()
        };

        string payloadJson = JsonSerializer.Serialize(payload);
        string payloadPart = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));
        string signaturePart = Sign(payloadPart);

        return new ApiTokenResult
        {
            Token = $"{payloadPart}.{signaturePart}",
            ExpiresAt = expiresAt
        };
    }

    public bool TryValidateToken(string token, out ApiUserContext? user)
    {
        user = null;
        if (string.IsNullOrWhiteSpace(token)) return false;

        string[] parts = token.Split('.');
        if (parts.Length != 2) return false;

        string expectedSignature = Sign(parts[0]);
        if (FixedEquals(parts[1], expectedSignature) == false) return false;

        ApiTokenPayload? payload;
        try
        {
            byte[] payloadBytes = Base64UrlDecode(parts[0]);
            payload = JsonSerializer.Deserialize<ApiTokenPayload>(Encoding.UTF8.GetString(payloadBytes));
        }
        catch
        {
            return false;
        }

        if (payload is null) return false;
        if (payload.ExpiresAtUnix <= DateTimeOffset.UtcNow.ToUnixTimeSeconds()) return false;

        user = new ApiUserContext
        {
            UserId = payload.UserId,
            UserName = payload.UserName,
            IsAdmin = payload.IsAdmin,
            AgentId = payload.AgentId,
            TreasuryId = payload.TreasuryId
        };

        return true;
    }

    private string Sign(string payloadPart)
    {
        using HMACSHA256 hmac = new(_secret);
        return Base64UrlEncode(hmac.ComputeHash(Encoding.UTF8.GetBytes(payloadPart)));
    }

    private static bool FixedEquals(string first, string second)
    {
        byte[] firstBytes = Encoding.UTF8.GetBytes(first);
        byte[] secondBytes = Encoding.UTF8.GetBytes(second);
        return firstBytes.Length == secondBytes.Length &&
               CryptographicOperations.FixedTimeEquals(firstBytes, secondBytes);
    }

    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private static byte[] Base64UrlDecode(string text)
    {
        string base64 = text.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private sealed class ApiTokenPayload
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public bool IsAdmin { get; set; }
        public int? AgentId { get; set; }
        public int? TreasuryId { get; set; }
        public long ExpiresAtUnix { get; set; }
    }
}
