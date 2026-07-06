using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class AddBillItemComponentRequest
{
    [Required]
    public int ComponentId { get; set; }
}
