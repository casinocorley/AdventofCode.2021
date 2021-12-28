using System.Text;

namespace AdventofCode._2021.Day16;

public class Puzzle1
{
    protected string HexString;
    public Puzzle1(string hexString)
    {
        this.HexString = hexString;
    }

    public virtual long Solve()
    {
        var binaryString = HexString.HexToBinary();
        var packets = ParseToEnd(binaryString);

        var flattenPackets = packets.Flatten();
        return flattenPackets.Sum(x => x.Version);
    }

    public List<IPacket> ParseToEnd()
    {
        var binaryString = HexString.HexToBinary();
        return ParseToEnd(binaryString);
    }

    public List<IPacket> ParseToEnd(string binaryString)
    {
        List<IPacket> packets = new();
        bool notEmpty;
        do
        {
            (var packet, binaryString) = ParseNextPacket(binaryString);
            packets.Add(packet);
            notEmpty = !(string.IsNullOrEmpty(binaryString) ||
                       binaryString.All(c => c == '0'));

        } while (notEmpty);

        return packets;
    }

    public (IPacket Packet, string Unparsed) ParseNextPacket(string binaryString)
    {
        var version = Pop(ref binaryString, 3).ToInt64(2);
        var typeId = Pop(ref binaryString, 3).ToInt64(2);

        if (typeId == 4) // type literal value
        {
            var value = HandleLiteralValue(ref binaryString);
            return (new LiteralPacket(version, typeId, value), binaryString);
        }
        
        // type operator
        var lengthTypeId = Pop(ref binaryString, 1).ToInt64(2);

        switch (lengthTypeId)
        {
            case 0:
                var length = (int) Pop(ref binaryString, 15).ToInt64(2);
                var subPackets0 = ParseToEnd(binaryString[0..length]);
                binaryString = binaryString[length..];
                var packet0 = new Operator0Packet(version, typeId, lengthTypeId, length)
                    {SubPackets = subPackets0};
                return (packet0, binaryString);
            case 1:
                List<IPacket> subPackets1 = new();
                var numOfPackets = Pop(ref binaryString, 11).ToInt64(2);
                for (var i = 0; i < numOfPackets; i++)
                {
                    (var packet, binaryString) = ParseNextPacket(binaryString);
                    subPackets1.Add(packet);
                }
                var packet1 = new Operator1Packet(version, typeId, lengthTypeId, numOfPackets)
                    {SubPackets = subPackets1};
                return (packet1, binaryString);
            default:
                throw new ArgumentException($"Unknown lengthTypeId {lengthTypeId}");
        }
    }

    public long HandleLiteralValue(ref string value)
    {
        StringBuilder binaryValue = new();
        long keepGoing;
        do
        {
            keepGoing = Pop(ref value, 1).ToInt64(2);
            binaryValue.Append(Pop(ref value, 4));
        } while (keepGoing == 1);

        return binaryValue.ToString().ToInt64(2);
    }

    public string Pop(ref string value, int numOfChars)
    {
        var results = value[0..numOfChars];
        value = value[numOfChars..];

        return results;
    }
}