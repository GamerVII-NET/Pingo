using System.Text.Json.Serialization;
using Pingo.Networking.Java.Protocol.Components;

namespace Pingo.Networking;

[JsonSerializable(typeof(ChatMessage))]
[JsonSerializable(typeof(ServerStatus))]
[JsonSourceGenerationOptions(
    AllowTrailingCommas = true,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters =
    [
        typeof(JsonStringEnumConverter<Color>)
    ])]
internal sealed partial class SourceGenerationContext : JsonSerializerContext;