using System;
using RDE.Structs;

namespace RDE.Statics;
public static class Cursor
{
    public static void SetCursorPosition(
        Vector2 componentPosition,
        Vector2 newPosition = default
    ){
        //Set Horizontal Axis
        if (newPosition.x > 0)
            newPosition = new(componentPosition.x, newPosition.y);

        //Set Vertical Axis
        if (newPosition.y > 0)
            newPosition = new(newPosition.x, componentPosition.y);
        else
            newPosition = new(newPosition.x, newPosition.y);

        //If dont have space in the terminal, it make more
        TerminalConfig.ExpandTerminalSize(newPosition);

        //Set New cursorPosition
        Console.SetCursorPosition(newPosition.x, newPosition.y);
    }
}
