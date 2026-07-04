using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class OpenTableBillRequest
{
    [Required]
    public int UserId { get; set; }

    public int? PeriodId { get; set; }

    public int BillTypeId { get; set; } = 3;

    public int AgentId { get; set; } = 0;
}
