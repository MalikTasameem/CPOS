namespace CPOS.PosApi.Models.Responses;

public sealed class BillItemDto
{
    public int DetailId { get; set; }
    public int ItemId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public string ItemName { get; set; } = "";
    public string UnitName { get; set; } = "";
    public string Notes { get; set; } = "";
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public int PtrId { get; set; }
}
