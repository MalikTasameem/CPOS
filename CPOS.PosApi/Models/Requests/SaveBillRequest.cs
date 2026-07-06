namespace CPOS.PosApi.Models.Requests;

public sealed class SaveBillRequest
{
    public int? PayId { get; set; }
    public int? TreasuryId { get; set; }
    public decimal? PaidAmount { get; set; }
    public DateTime? DeliverDate { get; set; }
}
