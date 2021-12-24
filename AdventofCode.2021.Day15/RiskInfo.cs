using System.Diagnostics;

[DebuggerDisplay("({Location.X},{Location.Y}): {Value}, IsSet:{IsSet}")]
public class RiskInfo : IComparable
{
    public (int X, int Y) Location { get; }
    public int Value { get; set; }

    public bool IsSet { get; set; }

    public RiskInfo((int X, int Y) location)
    {
        Location = location;
        Value = Int32.MaxValue;
        IsSet = false;
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
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