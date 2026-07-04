using CPOS.PosApi.Models.Requests;
using CPOS.PosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPOS.PosApi.Controllers;

[ApiController]
[Route("api")]
public sealed class BillsController : ControllerBase
{
    private readonly BillsService _billsService;

    public BillsController(BillsService billsService)
    {
        _billsService = billsService;
    }

    [HttpPost("tables/{tableId:int}/open-bill")]
    public async Task<IActionResult> OpenTableBill(int tableId, [FromBody] OpenTableBillRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _billsService.OpenTableBillAsync(tableId, request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("bills/{transactionId:int}")]
    public async Task<IActionResult> GetBill(int transactionId, CancellationToken cancellationToken)
    {
        var bill = await _billsService.GetBillAsync(transactionId, cancellationToken);
        if (bill is null) return NotFound();

        return Ok(bill);
    }

    [HttpPost("bills/{transactionId:int}/items")]
    public async Task<IActionResult> AddItem(int transactionId, [FromBody] AddBillItemRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var bill = await _billsService.AddItemAsync(transactionId, request, cancellationToken);
        return Ok(bill);
    }

    [HttpPatch("bills/items/{detailId:int}/qty")]
    public async Task<IActionResult> ChangeItemQuantity(int detailId, [FromBody] ChangeBillItemQtyRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var bill = await _billsService.ChangeItemQuantityAsync(detailId, request, cancellationToken);
        return Ok(bill);
    }

    [HttpPatch("bills/items/{detailId:int}/details")]
    public async Task<IActionResult> UpdateItemDetails(int detailId, [FromBody] UpdateBillItemDetailsRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var bill = await _billsService.UpdateItemDetailsAsync(detailId, request, cancellationToken);
        return Ok(bill);
    }

    [HttpDelete("bills/items/{detailId:int}")]
    public async Task<IActionResult> DeleteItem(int detailId, [FromQuery] bool onUpdate = false, CancellationToken cancellationToken = default)
    {
        var bill = await _billsService.DeleteItemAsync(detailId, onUpdate, cancellationToken);
        return Ok(bill);
    }

    [HttpPost("bills/{transactionId:int}/send-order")]
    public async Task<IActionResult> SendOrder(int transactionId, [FromBody] SendOrderRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var bill = await _billsService.SendTableOrderAsync(transactionId, request, cancellationToken);
        return Ok(bill);
    }
}
