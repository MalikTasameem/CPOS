namespace CPOS.PosApi.Security;

public sealed class ApiTokenResult
{
    public string Token { get; set; } = "";
    public DateTimeOffset ExpiresAt { get; set; }
}
