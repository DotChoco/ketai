using System;
using System.Collections.Generic;
using RDE.Models;
using RDE.Structs;

namespace RDE.Components;
/// <summary>
/// Tree Item
/// </summary>
// ReSharper disable once InconsistentNaming
public sealed class TItem
{
    public string Color { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<TItem> Children { get; set; } = new();
}


public sealed class Tree : Component
{

    public List<TItem> Content { get => _content; set => _content = value; }
    private List<TItem> _content = new();
    int spaces = 1;

    public Tree(List<TItem> items, Vector2 position)
    {
        _content = items;
        transform.position = position;
    }

    public Tree() { }

    public sealed override Component Render()
    {
        RenderFathers(_content);
        return this;
    }

    void RenderFathers(List<TItem> items)
    {
        int posy = transform.position.y;
        SetCursorPosition(transform.position);

        Text text = new();
        foreach (var item in items) {
            text.FillData(item.Content, item.Color).Render();

            posy = CursorPosition.y + 1;
            SetCursorPosition(new(CursorPosition.x, posy));
            if (item.Children != null && item.Children.Count > 0)
            {
                RenderChildren(item.Children);
                spaces--;
            }
        }
    }

    void RenderChildren(List<TItem> items)
    {
        spaces++;
        int posy = CursorPosition.y;
        SetCursorPosition(CursorPosition);

        foreach (var item in items)
        {
            Color.SetTextColor(item.Color);
            if (item.Content != null && item.Content != string.Empty)
            {
                var tabs = new string(' ', spaces);
                var lines = new string('-', (spaces / 2));
                Console.Write($"{tabs}|{lines}{item.Content}\n");
            }

            posy = CursorPosition.y + 1;
            SetCursorPosition(new(CursorPosition.x, posy));
            if (item.Children != null && item.Children.Count > 0)
            {
                RenderChildren(item.Children);
                spaces--;
            }
        }
    }
}