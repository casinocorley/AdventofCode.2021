namespace AdventofCode._2021.Day16;

public static class PuzzleExtensions
{
    public static string HexToBinary(this string value)
    {
        var result = value
            .Select(c => c.ToInt64(16))
            .Select(x => x.ToString(2))
            .Select(x => x.PadLeft(4, '0'))
            .Aggregate((x,y) => $"{x}{y}");

        return result;
    }

    public static long ToInt64(this string value, int fromBase)
    {
        return Convert.ToInt64(value, fromBase);
    }

    public static long ToInt64(this char value, int fromBase)
    {
        return Convert.ToInt64(value.ToString(), fromBase);
    }

    public static string ToString(this long value, int fromBase)
    {
        return Convert.ToString(value, fromBase);
    }

    public static IEnumerable<IPacket> Flatten(this IEnumerable<IPacket> value)
    {
        return value.SelectMany(x => x.Flatten());
    }

    public static IEnumerable<IPacket> Flatten(this IPacket value)
    {
        if (value is OperatorPacket oPacket)
            return oPacket.SubPackets.SelectMany(p => p.Flatten()).Concat(new[] {value});
        
        return new[] {value};
    }
}