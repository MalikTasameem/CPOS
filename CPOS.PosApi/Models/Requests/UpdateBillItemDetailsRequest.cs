using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class UpdateBillItemDetailsRequest
{
    [Required]
    public int UnitId { get; set; }

    public string Notes { get; set; } = "";

    public bool OnUpdate { get; set; }

    public int SalesTypeId { get; set; }
}
