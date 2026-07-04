namespace CPOS.PosApi.Models.Responses;

public sealed class PosBootstrapResponse
{
    public IReadOnlyList<PosGroupDto> Groups { get; set; } = Array.Empty<PosGroupDto>();
    public IReadOnlyList<PosItemDto> Items { get; set; } = Array.Empty<PosItemDto>();
    public IReadOnlyList<PosItemUnitDto> Units { get; set; } = Array.Empty<PosItemUnitDto>();
    public IReadOnlyList<PosStoreDto> Stores { get; set; } = Array.Empty<PosStoreDto>();
}
