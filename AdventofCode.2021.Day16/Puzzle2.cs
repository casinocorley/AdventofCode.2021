using System.Text;

namespace AdventofCode._2021.Day16;

public class Puzzle2 : Puzzle1
{
    public Puzzle2(string hexString)
        : base(hexString) { }

    public override long Solve()
    {
        var packets = ParseToEnd();
        var root = packets[0];
        
        var result = GetValue(root);
        return result;
    }

    public long GetValue(IPacket packet)
    {
        if (packet is LiteralPacket literalPacket)
            return literalPacket.Value;

        if (packet is OperatorPacket operatorPacket)
        {
            var subPackets = operatorPacket.SubPackets
                .Select(x => x is LiteralPacket literalPacket ? literalPacket.Value : GetValue(x))
                .ToArray();
            
            return packet.TypeId switch
            {
                0 => subPackets.Sum(),
                1 => subPackets.Aggregate((x, y) => x * y),
                2 => subPackets.Min(),
                3 => subPackets.Max(),
                5 => subPackets[0] > subPackets[1] ? 1 : 0,
                6 => subPackets[0] < subPackets[1] ? 1 : 0,
                7 => subPackets[0] == subPackets[1] ? 1 : 0,
                _ => 0
            };
        }

        return 0;
    }

}