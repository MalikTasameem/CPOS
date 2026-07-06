namespace CPOS.PosApi.Models.Responses;

public sealed class PosItemComponentOptionDto
{
    public int ComponentId { get; set; }
    public string ComponentName { get; set; } = "";
    public decimal Price { get; set; }
    public bool IsAdd { get; set; }
}
