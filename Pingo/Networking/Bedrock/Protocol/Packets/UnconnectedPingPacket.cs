namespace Pingo.Networking.Bedrock.Protocol.Packets;

internal sealed class UnconnectedPingPacket : IOutgoingPacket
{
    public int Identifier => 0x01;

    public required long Time { get; init; }

    public required long Client { get; init; }

    public void Write(ref MemoryWriter writer)
    {
        writer.WriteLong(Time);
        writer.WriteMagic();
        writer.WriteLong(Client);
    }
}