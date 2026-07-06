namespace CPOS.PosApi.Models.Responses;

public sealed class LoginResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } = "";
    public bool IsAdmin { get; set; }
    public bool IsAllow { get; set; }
    public bool CanUseSalesPriceInfo { get; set; }
    public bool CanSellWholesale { get; set; }
    public bool CanSellWholesale2 { get; set; }
    public bool IsMinSalesPriceEnabled { get; set; }
    public int? AgentId { get; set; }
    public int? TreasuryId { get; set; }
    public string Token { get; set; } = "";
    public DateTimeOffset? ExpiresAt { get; set; }
}
