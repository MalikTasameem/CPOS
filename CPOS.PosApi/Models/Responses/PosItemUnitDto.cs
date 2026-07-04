namespace CPOS.PosApi.Models.Responses;

public sealed class PosItemUnitDto
{
    public int UnitItemId { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; } = "";
    public int UnitId { get; set; }
    public string UnitName { get; set; } = "";
    public decimal UnitCargo { get; set; }
    public decimal Price { get; set; }
    public decimal MinSalesPrice { get; set; }
    public decimal MinSalesPrice2 { get; set; }
    public string Barcode { get; set; } = "";
    public bool IsValid { get; set; }
    public bool IsStore { get; set; }
    public bool IsDefault { get; set; }
}
