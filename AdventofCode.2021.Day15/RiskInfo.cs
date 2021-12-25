using System.Diagnostics;

namespace AdventofCode._2021.Day15;

[DebuggerDisplay("({Location.X},{Location.Y}): {Value}")]
public class RiskInfo
{
    public (int X, int Y) Location { get; }
    public int Value { get; set; }

    public RiskInfo((int X, int Y) location)
    {
        Location = location;
        Value = Int32.MaxValue;
    }
}

[DebuggerDisplay("({X},{Y})")]
public class Location
{
    public int X { get; set; }
    public int Y { get; set; }

    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}