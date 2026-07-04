using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class SendOrderRequest
{
    [Required]
    public int TableId { get; set; }
}
