namespace CPOS.PosApi.Models.Responses;

public sealed class TableDto
{
    public int TableId { get; set; }
    public int FlateId { get; set; }
    public string TableName { get; set; } = "";
    public string FlateName { get; set; } = "";
    public bool IsBusy { get; set; }
    public bool IsCash { get; set; }
}
