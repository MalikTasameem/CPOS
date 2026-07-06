namespace CPOS.PosApi.Models.Responses;

public sealed class SaveBillResponse
{
    public string Action { get; set; } = "";
    public string Message { get; set; } = "";
    public BillDto? Bill { get; set; }
}
