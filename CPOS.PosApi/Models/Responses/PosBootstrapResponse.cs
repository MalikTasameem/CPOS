namespace CPOS.PosApi.Models.Responses;

public sealed class PosBootstrapResponse
{
    public IReadOnlyList<PosGroupDto> Groups { get; set; } = Array.Empty<PosGroupDto>();
    public IReadOnlyList<PosItemDto> Items { get; set; } = Array.Empty<PosItemDto>();
    public IReadOnlyList<PosItemUnitDto> Units { get; set; } = Array.Empty<PosItemUnitDto>();
    public IReadOnlyList<PosStoreDto> Stores { get; set; } = Array.Empty<PosStoreDto>();
    public IReadOnlyList<PosBillTypeDto> BillTypes { get; set; } = Array.Empty<PosBillTypeDto>();
    public IReadOnlyList<PosPaymentMethodDto> PaymentMethods { get; set; } = Array.Empty<PosPaymentMethodDto>();
    public int DefaultAgentId { get; set; }
}
