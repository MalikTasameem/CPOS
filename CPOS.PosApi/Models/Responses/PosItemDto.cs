namespace CPOS.PosApi.Models.Responses;

public sealed class PosItemDto
{
    public int ItemId { get; set; }
    public int GroupId { get; set; }
    public string ItemName { get; set; } = "";
    public string SalesName { get; set; } = "";
    public bool IsValid { get; set; }
    public bool HasPhoto { get; set; }
    public string BackgroundColor { get; set; } = "";
    public string ForegroundColor { get; set; } = "";
}
