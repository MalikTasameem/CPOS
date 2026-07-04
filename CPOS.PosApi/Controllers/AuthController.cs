using CPOS.PosApi.Models.Requests;
using CPOS.PosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPOS.PosApi.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var user = await _authService.LoginAsync(request.Password, cancellationToken);
        if (user is null)
        {
            return Unauthorized(new { message = "خطأ في كلمة المرور." });
        }

        if (user.IsAllow == false)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "المستخدم غير مفعل." });
        }

        return Ok(user);
    }
}
