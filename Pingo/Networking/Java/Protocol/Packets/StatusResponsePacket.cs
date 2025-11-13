namespace Pingo.Networking.Java.Protocol.Packets;

internal sealed class StatusResponsePacket : IIngoingPacket<StatusResponsePacket>
{
    public string Status { get; set; }
    public int Identifier => 0x00;

    public StatusResponsePacket Read(MemoryReader reader)
    {
        return new StatusResponsePacket
        {
            Status = reader.ReadVariableString(true)
        };
    }
}
