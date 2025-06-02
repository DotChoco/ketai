using System;
using RDE.Models;

namespace RDE.Components;
public sealed class CheckBox : Component {
    const char checkboxChecked = '\u2611'; // ☑
    const char checkboxUnchecked = '\u2610'; // ☐

    private bool _checked = false;
    private Text _title = new();
    private char _box;
    
    public bool Checked { get => _checked; set => _checked = value; }
    public Text Title { get => _title; set => _title = value; }
    
    public CheckBox() {}
    public CheckBox(Text title) => _title = title;

    public sealed override Component Render() {
        
        Console.CursorVisible = false;
        SetCursorPosition(transform.position);
        _box = SetAscii();
        Console.Write($"{_box}\t{_title.value}");
        return this;
    }

    public void Toggle() {
        _checked = !_checked;
        _box = SetAscii();
        SetCursorPosition(transform.position);
        Console.Write($"{_box}");
    }

    char SetAscii() => _checked ? checkboxChecked : checkboxUnchecked;
    
    
    
}