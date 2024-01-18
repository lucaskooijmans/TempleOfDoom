using System;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Models.Utils;

public static class ColorExtension
{
    public static ConsoleColor? ToConsoleColorOrNull(this Color color) => color switch
        {
            Color.Red => ConsoleColor.Red,
            Color.Blue => ConsoleColor.Blue,
            Color.Green => ConsoleColor.Green,
            Color.Yellow => ConsoleColor.Yellow,
            Color.White => ConsoleColor.White,
            _ => null
        };
    
}