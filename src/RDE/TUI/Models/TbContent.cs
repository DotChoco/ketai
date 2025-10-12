using RDE.Components;
using RDE.UI.Enums;

namespace RDE.Models;
public sealed class TbContent
{
    public Align ItemAlign { get; set; } = Align.Center;
    public Text[,] Content { get; set; }
    public string TbColor { get; set; } = "AC90D8";
    public byte FieldsHeight { get; set; } = 1;
    public byte FieldsWidth { get; set; } = 10;
    public bool SeparetedRows { get; set; } = false;
    public TbContent(Text[,] content) => Content = content;
    public FieldStyles Style { get; set; }
    public TbContent() { }

}
