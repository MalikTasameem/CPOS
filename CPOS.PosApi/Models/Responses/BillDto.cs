namespace CPOS.PosApi.Models.Responses;

public sealed class BillDto
{
    public int TransactionId { get; set; }
    public int SalesBillId { get; set; }
    public int DailyBillNumber { get; set; }
    public int BillTypeId { get; set; }
    public int AgentId { get; set; }
    public string AgentName { get; set; } = "";
    public int TableId { get; set; }
    public string TableName { get; set; } = "";
    public string Barcode { get; set; } = "";
    public string BillDate { get; set; } = "";
    public string CustomerPhone { get; set; } = "";
    public bool IsDepended { get; set; }
    public bool IsVoid { get; set; }
    public bool IsPaid { get; set; }
    public bool IsOrdered { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Pure { get; set; }
    public IReadOnlyList<BillItemDto> Items { get; set; } = Array.Empty<BillItemDto>();
}
