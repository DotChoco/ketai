using RDE.Components;
using RDE.UI.Enums;

namespace RDE.Models;

public sealed class InputFieldData {
    public Text Placeholder = new();
    public string ContentColor = Color.DEFAULT_COLOR;
    public string ComponentColor = Color.DEFAULT_COLOR;
    public FieldStyles Style = FieldStyles.Basic;
}
