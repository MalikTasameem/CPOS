using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class LoginRequest
{
    [Required]
    public string Password { get; set; } = "";
}
