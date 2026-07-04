using System.ComponentModel.DataAnnotations;

namespace CPOS.PosApi.Models.Requests;

public sealed class AddBillItemRequest
{
    [Required]
    public int ItemId { get; set; }

    [Required]
    public int StoreId { get; set; }

    [Required]
    public int UnitItemId { get; set; }

    public string Barcode { get; set; } = "";

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public string? ValidDate { get; set; }

    public bool OnUpdate { get; set; }

    public int SalesTypeId { get; set; }
}
