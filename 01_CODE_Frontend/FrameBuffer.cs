using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models;

namespace CODE_Frontend;

public class FrameBuffer
{
    private readonly Dictionary<Position, string> _frame = new();
    private int MinX => _frame.Keys.Select(position => position.X).Min();
    private int MaxX => _frame.Keys.Select(position => position.X).Max();
    private int MinY => _frame.Keys.Select(position => position.Y).Min();
    private int MaxY => _frame.Keys.Select(position => position.Y).Max();
    
    public void Clear()
    {
        _frame.Clear();
    }
    
    public void SetPixel(Position pos, string value)
    {
        _frame[pos] = value;
    }
    
    public void Render()
    {
        for (int y = MinY; y <= MaxY; y++)
        {
            for (int x = MinX; x <= MaxX; x++)
            {
                Position position = new Position(x, y);
                Console.Write(_frame.TryGetValue(position, out string value) ? value : " ");
            }
            Console.WriteLine();
        }
    }
}