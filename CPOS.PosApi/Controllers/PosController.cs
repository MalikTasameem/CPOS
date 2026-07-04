using CPOS.PosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPOS.PosApi.Controllers;

[ApiController]
[Route("api/pos")]
public sealed class PosController : ControllerBase
{
    private readonly PosCatalogService _catalogService;

    public PosController(PosCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("bootstrap")]
    public async Task<IActionResult> GetBootstrap(CancellationToken cancellationToken)
    {
        var result = await _catalogService.GetBootstrapAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("groups")]
    public async Task<IActionResult> GetGroups(CancellationToken cancellationToken)
    {
        var groups = await _catalogService.GetGroupsAsync(cancellationToken);
        return Ok(groups);
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems([FromQuery] int? groupId, [FromQuery] string? search, CancellationToken cancellationToken)
    {
        var items = await _catalogService.GetItemsAsync(groupId, search, cancellationToken);
        return Ok(items);
    }

    [HttpGet("items/{itemId:int}/units")]
    public async Task<IActionResult> GetItemUnits(int itemId, CancellationToken cancellationToken)
    {
        var units = await _catalogService.GetItemUnitsAsync(itemId, cancellationToken);
        return Ok(units);
    }

    [HttpGet("items/by-barcode/{barcode}")]
    public async Task<IActionResult> GetItemByBarcode(string barcode, CancellationToken cancellationToken)
    {
        var item = await _catalogService.GetItemByBarcodeAsync(barcode, cancellationToken);
        if (item is null) return NotFound();

        return Ok(item);
    }
}
