namespace Pingo.Networking.Bedrock.Protocol.Packets;

internal sealed class UnconnectedPingPacket : IOutgoingPacket
{
    public long Time { get; set; }

    public long Client { get; set; }
    public int Identifier => 0x01;

    public void Write(ref MemoryWriter writer)
    {
        writer.WriteLong(Time);
        writer.WriteMagic();
        writer.WriteLong(Client);
    }
}
