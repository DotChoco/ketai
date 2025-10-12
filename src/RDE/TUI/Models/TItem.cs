using System.Collections.Generic;
namespace RDE.Components;

/// <summary>
/// Tree Item
/// </summary>
public sealed class TItem
{
    public string Color { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<TItem> Children { get; set; } = new();
}
