using System;
using Color = RDE.Models.Color;
using RDE.Models;

namespace RDE.Components;
public sealed class Text : Component
{
    public string value { get; set; } = string.Empty;
    public string color { get; set; } = "ffffff";


    public Text(string value, string color) => FillData(value, color);
    public Text() { }

    public Component FillData(string value, string color){
        this.value = value;
        this.color = color;
        Color.SetTextColor(color.ToString());

        return this;
    }

    public sealed override Component Render() {
        SetCursorPosition(transform.position);
        Console.Write(value);
        return this;
    }

}
