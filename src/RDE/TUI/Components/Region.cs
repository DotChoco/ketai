using RDE.Models;
using RDE.Core.Structs;

namespace RDE.Components;

public sealed class Region: Fields
{
    public string Color = Models.Color.DEFAULT_COLOR;
    public Region(){}

    public sealed override Component Render() {
        SetData();
        base.Render();
        return this;
    }

    public Component SetData() {
        MakeTopLine(1,_width);
        MakeMidLine(1,_width, _height,false);
        MakeBottonLine(1,_width);
        return this;
    }

    //Size Methods
    public Component SetSize(int width, int height)
        => SetSize(new(width, height));
    public Component SetSize(Vector2 size) {
        _width = size.x > _minWidth ? size.x : _minWidth;
        _height = size.y > _minHeight ? size.y : _minHeight;
        return this;
    }

    public Vector2 GetSize()  => new (_width, _height);




}
