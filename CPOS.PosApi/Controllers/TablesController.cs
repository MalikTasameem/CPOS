using CPOS.PosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPOS.PosApi.Controllers;

[ApiController]
[Route("api/tables")]
public sealed class TablesController : ControllerBase
{
    private readonly TablesService _tablesService;

    public TablesController(TablesService tablesService)
    {
        _tablesService = tablesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTables(
        [FromQuery] int flateId = 0,
        [FromQuery] string status = "all",
        [FromQuery] bool includeLayout = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _tablesService.GetTablesAsync(flateId, status, includeLayout, cancellationToken);
        return Ok(result);
    }
}
