using System;
using System.Text;
using RDE.Core.Structs;
using RDE.UI.Enums;

namespace RDE.Models;
public abstract class Fields: FieldBorder {

    //Variables
    private StringBuilder _data = new();
    private string[] _lines = Array.Empty<string>();

    protected string _color = Color.DEFAULT_COLOR;
    protected const string DEFAULT_BORDER_COLOR = Color.DEFAULT_COLOR;
    protected FieldStyles Style = FieldStyles.Basic;
    protected StringBuilder SbData { get=> _data; set => _data = value; }
    protected int _width;
    protected int _height;
    protected int _minWidth = 10;
    protected int _minHeight = 1;

    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }

    //Render Methods
    public override Component Render()
    {
        Color.SetTextColor(_color);
        string[] lines = _data.ToString().Split('\n');

        for (int i = 0; i < lines.Length; i++) {
            SetCursorPosition(transform.position.x, transform.position.y + i);
            Console.Write(lines[i]);
        }


        return this;
    }

    public Component SetColor(string hex) {
        _color = hex;
        return this;
    }




    //Style Methods
    protected void MakeMidLine(
        int colSize, int width ,
        int height = 1, bool separator = false
    ) {
        SetStyle(Style);
        char middleChar = separator ? _innerLine : SPACE_CHAR;
        for (int i = 0; i < height; i++)
        {
            //Draw the first item of the line
            _data.Append(DrawFirstItem(middleChar, _leftSideLine, separator));

            //Draw the mid items of the line
            for (int j = 0; j < colSize; j++) {

                //Draw the last item of the line
                if (j == colSize - 1) {
                    _data.Append(DrawLastItem(middleChar, _rightSideLine,
                        width, separator));
                }
                else {
                    _data.Append(DrawLastItem(middleChar, _intersectionLine,
                        width, separator));
                }
            }
            _data.Append(CARRIAGE_RETURN);
        }
    }

    protected void MakeBottonLine(Vector2 innerData) => MakeBottonLine(innerData.x, innerData.y);
    protected void MakeTopLine(int colSize, int width){
        SetStyle(Style);

        //left cornner
        _data.Append(_topLeftCorner);

        for (int i = 0; i < colSize; i++) {
            _data.Append(new string(_innerLine, width));

            if (colSize > 1 && i < colSize - 1)
                _data.Append(_verticalTopLine);
        }
        //right cornner
        _data.Append(_topRightCorner);
        _data.Append(CARRIAGE_RETURN);

    }

    protected void MakeBottonLine(int colSize, int width) {
        SetStyle(Style);

        //left cornner
        _data.Append(_bottonLeftCorner);

        for (int i = 0; i < colSize; i++) {
            _data.Append(new string(_innerLine, width));

            if (colSize > 1 && i < colSize - 1)
                _data.Append(_verticalBottonLine);
        }

        //right cornner
        _data.Append(_bottonRightCorner);
        _data.Append(CARRIAGE_RETURN);

    }


    string DrawLastItem(char middleChar, char focusChar,
        int width, bool separator = false)
    {
        StringBuilder sb = new();
        sb.Append(new string(middleChar, width));

        if (separator)
            sb.Append(focusChar);
        else
            sb.Append(_verticalLine);

        return sb.ToString();
    }
    char DrawFirstItem(char middleChar, char focusChar, bool separator = false)
        => separator ? focusChar: _verticalLine;




}
