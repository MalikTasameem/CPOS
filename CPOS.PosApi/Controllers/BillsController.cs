using CPOS.PosApi.Models.Requests;
using CPOS.PosApi.Security;
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

        ApiUserContext user = GetApiUser();
        request.UserId = user.UserId;
        var result = await _billsService.OpenTableBillAsync(tableId, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("bills/open-direct")]
    public async Task<IActionResult> OpenDirectBill([FromBody] OpenTableBillRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        ApiUserContext user = GetApiUser();
        request.UserId = user.UserId;

        try
        {
            var result = await _billsService.OpenDirectBillAsync(request, user.CanUseSalesPriceInfo, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
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

        try
        {
            var bill = await _billsService.AddItemAsync(transactionId, request, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("bills/items/{detailId:int}/qty")]
    public async Task<IActionResult> ChangeItemQuantity(int detailId, [FromBody] ChangeBillItemQtyRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var bill = await _billsService.ChangeItemQuantityAsync(detailId, request, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("bills/items/{detailId:int}/details")]
    public async Task<IActionResult> UpdateItemDetails(int detailId, [FromBody] UpdateBillItemDetailsRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var bill = await _billsService.UpdateItemDetailsAsync(detailId, request, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("bills/items/{detailId:int}/component-options")]
    public async Task<IActionResult> GetItemComponentOptions(int detailId, [FromQuery] bool isAdd = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _billsService.GetItemComponentOptionsAsync(detailId, isAdd, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("bills/items/{detailId:int}/components")]
    public async Task<IActionResult> GetItemComponents(int detailId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _billsService.GetBillItemComponentsAsync(detailId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("bills/items/{detailId:int}/components")]
    public async Task<IActionResult> AddItemComponent(int detailId, [FromBody] AddBillItemComponentRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var result = await _billsService.AddBillItemComponentAsync(detailId, request, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("bills/items/{detailId:int}/components/{componentLineId:int}/qty")]
    public async Task<IActionResult> ChangeItemComponentQuantity(int detailId, int componentLineId, [FromBody] ChangeBillItemComponentQtyRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var result = await _billsService.ChangeBillItemComponentQuantityAsync(detailId, componentLineId, request, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("bills/items/{detailId:int}/components/{componentLineId:int}")]
    public async Task<IActionResult> DeleteItemComponent(int detailId, int componentLineId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _billsService.DeleteBillItemComponentAsync(detailId, componentLineId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("bills/items/{detailId:int}/components")]
    public async Task<IActionResult> ClearItemComponents(int detailId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _billsService.ClearBillItemComponentsAsync(detailId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("bills/{transactionId:int}/type")]
    public async Task<IActionResult> UpdateBillType(int transactionId, [FromBody] UpdateBillTypeRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var bill = await _billsService.UpdateBillTypeAsync(transactionId, request, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("bills/items/{detailId:int}")]
    public async Task<IActionResult> DeleteItem(int detailId, [FromQuery] bool onUpdate = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var bill = await _billsService.DeleteItemAsync(detailId, onUpdate, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("bills/{transactionId:int}/send-order")]
    public async Task<IActionResult> SendOrder(int transactionId, [FromBody] SendOrderRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var bill = await _billsService.SendTableOrderAsync(transactionId, request, cancellationToken);
            return Ok(bill);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("bills/{transactionId:int}/save")]
    public async Task<IActionResult> SaveBill(int transactionId, [FromBody] SaveBillRequest request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            ApiUserContext user = GetApiUser();
            var result = await _billsService.SaveBillAsync(transactionId, request, user, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private ApiUserContext GetApiUser()
    {
        return HttpContext.Items["ApiUser"] as ApiUserContext ?? new ApiUserContext();
    }
}
