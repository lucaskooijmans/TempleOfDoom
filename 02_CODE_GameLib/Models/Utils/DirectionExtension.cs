using System;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Models.Utils;

public static class DirectionExtension
{
    public static Position GetDirectionValues(this Direction direction) => direction switch
    {
        Direction.West => new Position(-1, 0),
        Direction.East => new Position(1, 0),
        Direction.South => new Position(0, 1),
        Direction.North => new Position(0, -1),
        _ => new Position(0, 0)
    };
		
    public static bool IsHorizontal (this Direction direction) => direction switch
    {
        Direction.West => true,
        Direction.East => true,
        _ => false
    };

    public static Direction? ToDirectionOrNull(ConsoleKey consoleKey) => consoleKey switch
    {
        ConsoleKey.UpArrow or ConsoleKey.W => Direction.North,
        ConsoleKey.LeftArrow or ConsoleKey.A => Direction.West,
        ConsoleKey.RightArrow or ConsoleKey.D => Direction.East,
        ConsoleKey.DownArrow or ConsoleKey.S => Direction.South,
        _ => null
    };
		
    public static Direction Opposite(this Direction direction) => direction switch
    {
        Direction.West => Direction.East,
        Direction.East => Direction.West,
        Direction.South => Direction.North,
        Direction.North => Direction.South,
        _ => throw new InvalidOperationException("Invalid direction")
    };
}