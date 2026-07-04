namespace CPOS.PosApi.Models.Responses;

public sealed class TableLayoutElementDto
{
    public int LayoutId { get; set; }
    public int FlateId { get; set; }
    public int? TableId { get; set; }
    public string ElementType { get; set; } = "Table";
    public string ElementText { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Rotation { get; set; }
    public int SeatsCount { get; set; }
    public int? BackColorArgb { get; set; }
    public int? ForeColorArgb { get; set; }
    public int ZIndex { get; set; }
    public bool IsBusy { get; set; }
    public bool IsCash { get; set; }
}
