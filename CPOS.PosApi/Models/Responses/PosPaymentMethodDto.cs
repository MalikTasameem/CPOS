namespace CPOS.PosApi.Models.Responses;

public sealed class PosPaymentMethodDto
{
    public int PaymentId { get; set; }
    public string PaymentName { get; set; } = "";
    public int TreasuryId { get; set; }
    public string TreasuryName { get; set; } = "";
    public bool IsLocked { get; set; }
}
