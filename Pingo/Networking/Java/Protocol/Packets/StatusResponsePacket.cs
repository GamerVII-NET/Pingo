namespace Pingo.Networking.Java.Protocol.Packets;

internal sealed class StatusResponsePacket : IIngoingPacket<StatusResponsePacket>
{
    public static int Identifier => 0x00;

    public required string Status { get; init; }

    public static StatusResponsePacket Read(MemoryReader reader)
    {
        return new StatusResponsePacket
        {
            Status = reader.ReadVariableString(true)
        };
    }
}