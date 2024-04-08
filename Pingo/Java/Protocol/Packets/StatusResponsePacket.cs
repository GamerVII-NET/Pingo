using System.Text.Json.Serialization;
using Pingo.Java.Protocol.Components;

namespace Pingo.Java.Protocol.Packets;

internal sealed class StatusResponsePacket : IIngoingPacket<StatusResponsePacket>
{
    public static int Identifier => 0x00;

    public required string Status { get; init; }

    public static StatusResponsePacket Read(MemoryReader reader)
    {
        return new StatusResponsePacket
        {
            Status = reader.ReadVariableString()
        };
    }
}

internal sealed class ServerStatus
{
    public required ServerVersion Version { get; set; }

    [JsonPropertyName("players")]
    public required PlayerInformation PlayerInformation { get; set; }

    public required ChatMessage Description { get; set; }

    public string Favicon { get; set; } = string.Empty;
}

internal sealed class ServerVersion
{
    public required string Name { get; set; }

    public required int Protocol { get; set; }
}

internal sealed class PlayerInformation
{
    public required int Max { get; set; }

    public required int Online { get; set; }
}