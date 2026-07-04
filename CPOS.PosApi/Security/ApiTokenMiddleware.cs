using System.Text.Json;

namespace CPOS.PosApi.Security;

public sealed class ApiTokenMiddleware
{
    private readonly RequestDelegate _next;

    public ApiTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IApiTokenService tokenService)
    {
        if (ShouldSkip(context))
        {
            await _next(context);
            return;
        }

        string token = GetBearerToken(context);
        if (tokenService.TryValidateToken(token, out ApiUserContext? user) == false || user is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = "غير مصرح. الرجاء تسجيل الدخول مرة أخرى."
            }));
            return;
        }

        context.Items["ApiUser"] = user;
        await _next(context);
    }

    private static bool ShouldSkip(HttpContext context)
    {
        PathString path = context.Request.Path;

        if (HttpMethods.IsOptions(context.Request.Method)) return true;
        if (path.StartsWithSegments("/swagger")) return true;
        if (path.StartsWithSegments("/api/auth/login")) return true;

        return false;
    }

    private static string GetBearerToken(HttpContext context)
    {
        string authorization = context.Request.Headers.Authorization.ToString();
        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return authorization["Bearer ".Length..].Trim();
        }

        return "";
    }
}
