using System;
using RDE.Models;
using RDE.Structs.Table;
using Color = RDE.Models.Color;

namespace RDE.Components;


public sealed class Table : Fields
{

    #region Variables

    //Private
    private TbContent _tb;
    private int rowSize;
    private int colSize;
    private ItemData[] tableData = Array.Empty<ItemData>();
    private string[] _tbRender = Array.Empty<string>();
    private string _textColor = Color.DEFAULT_COLOR;

    //public
    public string PlaceHolder = string.Empty;

    #endregion


    public Table() =>  _tb = null;
    public Table(TbContent tb) => _tb = tb;

    public Component SetData(TbContent tb) {
        _tb = tb;
        _color = _tb.TbColor;
        Style = _tb.Style;
        SbData.Clear();
        return this;
    }

    private void BorderMapping() {
        rowSize = _tb.Content.GetLength(0);
        colSize = _tb.Content.GetLength(1);
        int indexTbData = 0;

        tableData = new ItemData[rowSize];
        ItemData actualItem = new(){ column = 0, row = 0 , length = 0 };

        //Get the Longer Item Length by each Row
        for (int i = 0; i < _tb.Content.GetLength(0); i++) {
            for (int j = 0; j < _tb.Content.GetLength(1); j++) {
                actualItem = new() {
                    length = (ushort)_tb.Content[i, j].value.Length,
                    row = (ushort)i,
                    column = (ushort)j,
                    height = _tb.FieldsHeight
                };

                if (IsHigherThanPreview(tableData[indexTbData], actualItem)) {
                    if (actualItem.length > _tb.FieldsWidth)
                    {
                        int lines = actualItem.length > _tb.FieldsWidth
                            ? (actualItem.length / _tb.FieldsWidth) + _minHeight
                            : _minHeight;

                        actualItem.height = (ushort)lines;
                    }

                    tableData[indexTbData] = actualItem;
                }
            }
            indexTbData++;
        }


        //Make TopSeparator
        MakeTopLine(colSize, _tb.FieldsWidth);

        //Make the rows
        for (int i = 0; i < rowSize; i++) {
            MakeMidLine(colSize, _tb.FieldsWidth,
                tableData[i].height);

            //Make a separator bettwen Rows
            if (_tb.SeparetedRows && i < rowSize - 1) {
                MakeMidLine(colSize, _tb.FieldsWidth, 1, true);
            }
        }

        //Make Botton Separator
        MakeBottonLine(colSize, _tb.FieldsWidth);
    }


    bool IsHigherThanPreview(ItemData savedItem, ItemData actualItem)
        => actualItem.length > savedItem.length;

    private void RenderText()
    {
        int offsetX = 1;
        int offsetY = 1;

        int offsetIntoFieldsY = 2;


        int innerOffsetX = 0;
        int innerOffsetY = 0;
        int dataLength = 0;

        int textPosX = transform.position.x;
        int textPosY = transform.position.y;

        int lastPosY;
        int lastPosX;

        for (int i = 0; i < rowSize; i++) {
            for (int j = 0; j < colSize; j++)
            {
                dataLength = _tb.Content[i, j].value.Length;
                innerOffsetY = tableData[i].height;

                lastPosY = textPosY + offsetY;
                lastPosX = textPosX + offsetX;

                //If the height it doesn't higher than 1
                if (tableData[i].height == _minHeight)
                {
                    if (i > 0) { lastPosY += offsetIntoFieldsY; }


                    SetCursorPosition(lastPosX, lastPosY);
                    Color.SetTextColor(_tb.Content[i, j].color);
                    Console.Write(_tb.Content[i, j].value);

                    innerOffsetX = _tb.FieldsWidth - dataLength;
                    textPosX += innerOffsetX + dataLength + offsetX;
                    _height++;
                }
                else if (tableData[i].height > _minHeight){
                    int newPosY = lastPosY + offsetIntoFieldsY;

                    if (dataLength > _width) {
                        string[] dataParts = TrimByWidth(_tb.FieldsWidth, _tb.Content[i, j].value);

                        for (int k = 0; k < dataParts.Length; k++)
                        {
                            SetCursorPosition(textPosX + offsetX, newPosY);

                            Color.SetTextColor(_tb.Content[i, j].color);
                            Console.Write(dataParts[k]);

                            dataLength = dataParts[k].Length;
                            newPosY++;
                            _height++;
                        }

                        textPosX += _tb.FieldsWidth + offsetX;
                    }
                    else {
                        SetCursorPosition(textPosX + offsetX, lastPosY);

                        Color.SetTextColor(_tb.Content[i, j].color);
                        Console.Write(_tb.Content[i, j].value);


                        _height++;
                        innerOffsetX = _tb.FieldsWidth - dataLength;
                        textPosX += innerOffsetX + dataLength + offsetX;
                    }


                }


            }


            if (innerOffsetY > _minHeight)
                { textPosY += innerOffsetY + offsetY; }

            textPosX = transform.position.x;
            SetCursorPosition(textPosX + offsetX, textPosY + offsetY);
        }

        SetCursorPosition(transform.position.x, _height + offsetY);
    }


    string[] TrimByWidth(int width, string text) {
        int lines = text.Length > width ? (text.Length / width) + _minHeight : _minHeight;
        int index = 0;
        int dataLength = text.Length;

        string[] dataParts = new string[lines];

        if (text.Length <= width) {
            dataParts[0] = text;
            return dataParts;
        }
        else {
            for (int i = 0; i < lines; i++) {
                if (index + width < text.Length) {
                    dataParts[i] = text.Substring(index, width);
                    index += width;
                }
                else {
                    dataLength = text.Length - index;
                    dataParts[i] = text.Substring(index, dataLength);
                }
            }
        }

        return dataParts;
    }


    public sealed override Component Render()
    {
        if (_tb != null)
        {
            SetData(_tb);

            //This method makes the mapping before the rendering
            BorderMapping();

            //This method makes the rendering of the table
            base.Render();

            //This method makes the content table rendering
            RenderText();
        }
        return this;
    }

}

