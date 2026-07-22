using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class SaveBillRequest
{
    [Range(1, int.MaxValue)]
    public int? PayId { get; set; }

    [Range(1, int.MaxValue)]
    public int? TreasuryId { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? PaidAmount { get; set; }

    public DateTime? DeliverDate { get; set; }
}
