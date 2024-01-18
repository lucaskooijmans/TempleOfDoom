using System;

namespace CODE_GameLib.Models;

public readonly struct Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y) => (X, Y) = (x, y);

    public bool Equals(Position other) => X == other.X && Y == other.Y;

    public Position Add(Position other) => new(X + other.X, Y + other.Y);

    public bool IsAdjacentTo(Position other) => Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
}