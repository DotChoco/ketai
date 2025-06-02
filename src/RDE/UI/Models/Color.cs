using System;
using RDE.Structs;

namespace RDE.Models
{
    public static class Color
    {
        public const string DEFAULT_COLOR = "AC90D8";
        const int HexMask = 0xFF;

        
        public static void SetTextColor(int r, int g, int b) 
            => SetTextColor(new Vector3(r, g, b));
        
        public static void SetTextColor(Vector3 color) 
            => Console.Write($"\u001b[38;2;{color.x};{color.y};{color.z}m");
        
        
        // ejemplo de uso del span
        // public static void SetTextColor(Vector3 color) {
        //     Span<char> buffer = stackalloc char[6]; // 6 caracteres hex (RRGGBB)
        //     
        //     // Convertir cada componente a hexadecimal de 2 dígitos
        //     color.x.TryFormat(buffer.Slice(0, 2), out _, "X2");
        //     color.y.TryFormat(buffer.Slice(2, 2), out _, "X2");
        //     color.z.TryFormat(buffer.Slice(4, 2), out _, "X2");
        //     
        //     SetTextColor(buffer.ToString()); // Convertir a string final y llamar al método
        // }
        
        
        public static void SetTextColor(string hex = DEFAULT_COLOR) {
            if (hex == string.Empty)
                hex = DEFAULT_COLOR;

            // Convert hex string to 24 bits integer
            int color = Convert.ToInt32(hex, 16);

            // Extract RGB components using bitwise operations
            int r = (color >> 16) & HexMask; // Bits 16-23
            int g = (color >> 8) & HexMask;  // Bits 8-15
            int b = color & HexMask;         // Bits 0-7

            // Use ANSI escape to change the color text
            Console.Write($"\u001b[38;2;{r};{g};{b}m");
        }
        
        public static void ResetTextColor() => Console.Write("\u001b[0m");
        
    }
}