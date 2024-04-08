namespace Pingo.Status;

public sealed class BedrockStatus : StatusBase
{
    public required string Edition { get; init; }

    public required string[] MessagesOfTheDay { get; init; }

    public required int Protocol { get; init; }

    public required string Version { get; init; }

    public required int OnlinePlayers { get; init; }

    public required int MaximumPlayers { get; init; }

    public required long ServerIdentifier { get; init; }

    public required string GameMode { get; init; }
}