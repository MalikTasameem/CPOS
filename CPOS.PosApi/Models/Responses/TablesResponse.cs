namespace CPOS.PosApi.Models.Responses;

public sealed class TablesResponse
{
    public IReadOnlyList<TableDto> Tables { get; set; } = Array.Empty<TableDto>();
    public IReadOnlyList<TableLayoutElementDto> Layout { get; set; } = Array.Empty<TableLayoutElementDto>();
}
