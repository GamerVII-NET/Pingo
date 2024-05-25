using System.Text.Json.Serialization;
using Pingo.Converters;

namespace Pingo.Networking.Java.Protocol.Components;

internal sealed class ServerStatus
{
    public required ServerVersion Version { get; set; }

    [JsonPropertyName("players")]
    public required PlayerInformation PlayerInformation { get; set; }

    [JsonConverter(typeof(DescriptionConverter))]
    public required Description Description { get; set; }

    public string Favicon { get; set; } = string.Empty;
}

public class Description
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("extra")] public ChatMessage[] Extra { get; set; } = [];
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
