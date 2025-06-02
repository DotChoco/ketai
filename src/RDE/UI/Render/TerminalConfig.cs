using RDE.Structs;

namespace RDE.Statics;

public static class TerminalConfig {
    public static void ExpandTerminalSize(Vector2 size) {
        //Make a new vertical space in the console
        if (size.y >= Console.WindowHeight) {
            Console.BufferHeight = size.y + 1;
            Console.WindowHeight = size.y + 1;
        }

        //Make a new horizontal space in the console
        if (size.x >= Console.WindowWidth) {
            Console.BufferWidth = size.x + 1;
            Console.WindowWidth = size.x + 1;
        }
    }

    public static void ReduceTerminalSize() {
        //Make a new vertical space in the console
        if (Console.WindowHeight > 0) { Console.WindowHeight--; }
        //Make a new horizontal space in the console
        if (Console.WindowWidth > 0) { Console.WindowWidth--; }
    }

}
