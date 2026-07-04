using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class ChangeBillItemQtyRequest
{
    [Required]
    public int ChangeBy { get; set; }

    public bool OnUpdate { get; set; }
}
