namespace Pingo.Networking.Java.Protocol.Packets;

internal sealed class HandshakePacket : IOutgoingPacket
{
    public int Identifier => 0x00;

    public required int ProtocolVersion { get; init; }

    public required string Address { get; init; }

    public required ushort Port { get; init; }

    public required int NextState { get; init; }

    public void Write(ref MemoryWriter writer)
    {
        writer.WriteVariableInteger(ProtocolVersion);
        writer.WriteVariableString(Address);
        writer.WriteUnsignedShort(Port);
        writer.WriteVariableInteger(NextState);
    }
}