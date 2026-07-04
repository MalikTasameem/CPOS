namespace CPOS.PosApi.Models.Responses;

public sealed class OpenBillResponse
{
    public int TransactionId { get; set; }
    public int DailyBillNumber { get; set; }
    public int SalesBillId { get; set; }
    public bool IsNew { get; set; }
    public BillDto? Bill { get; set; }
}
