namespace CPOS.PosApi.Models.Responses;

public sealed class BillItemComponentDto
{
    public int LineId { get; set; }
    public string ComponentName { get; set; } = "";
    public decimal Quantity { get; set; }
}
