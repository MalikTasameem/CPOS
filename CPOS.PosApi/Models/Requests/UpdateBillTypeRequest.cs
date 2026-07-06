using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class UpdateBillTypeRequest
{
    [Range(1, int.MaxValue)]
    public int BillTypeId { get; set; }
}
