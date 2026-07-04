namespace CPOS.PosApi.Security;

public sealed class ApiUserContext
{
    public int UserId { get; set; }
    public string UserName { get; set; } = "";
    public bool IsAdmin { get; set; }
    public int? AgentId { get; set; }
    public int? TreasuryId { get; set; }
}
