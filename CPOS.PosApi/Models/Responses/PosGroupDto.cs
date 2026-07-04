namespace CPOS.PosApi.Models.Responses;

public sealed class PosGroupDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = "";
    public int RankNumber { get; set; }
    public string BackgroundColor { get; set; } = "";
    public string ForegroundColor { get; set; } = "";
}
