namespace AdventofCode._2021.Day16;

public interface IPacket
{
    long Version { get; }
    long TypeId { get; }
}

public abstract class Packet : IPacket
{
    public long Version { get; set; }
    public long TypeId { get; set; }

    protected Packet(long version, long typeId)
    {
        Version = version;
        TypeId = typeId;
    }
}

public abstract class OperatorPacket : Packet
{
    public long LengthTypeId { get; set; }
    public List<IPacket> SubPackets { get; set; } = new();

    protected OperatorPacket(long version, long typeId, long lengthTypeId)
        : base(version, typeId)
    {
        LengthTypeId = lengthTypeId;
    }
}

public class LiteralPacket : Packet
{
    public long Value { get; set; }

    public LiteralPacket(long version, long typeId, long value)
        : base(version, typeId)
    {
        Value = value;
    }
}

public class Operator0Packet : OperatorPacket
{
    public long LengthOfSubPackets { get; set; }

    public Operator0Packet(long version, long typeId, long lengthTypeId, long lengthOfSubPackets)
        : base(version, typeId, lengthTypeId)
    {
        LengthOfSubPackets = lengthOfSubPackets;
    }
}

public class Operator1Packet : OperatorPacket
{
    public long NumOfSubPackets { get; set; }

    public Operator1Packet(long version, long typeId, long lengthTypeId, long numOfSubPackets)
        : base(version, typeId, lengthTypeId)
    {
        NumOfSubPackets = numOfSubPackets;
    }
}