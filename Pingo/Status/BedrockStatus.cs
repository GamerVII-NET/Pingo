namespace Pingo.Status;

/// <summary>
/// Represents a Minecraft: Bedrock edition server status.
/// </summary>
public sealed class BedrockStatus : StatusBase
{
    /// <summary>
    /// Stores the game edition of the server, for example Minecraft: Education edition.
    /// </summary>
    public required string Edition { get; init; }

    /// <summary>
    /// Stores possibly multiple lines of MOTDs.
    /// </summary>
    public required string[] MessagesOfTheDay { get; init; }

    /// <summary>
    /// Stores the protocol version of the server.
    /// </summary>
    public required int Protocol { get; init; }

    /// <summary>
    /// Stores the game version that is supported by the server.
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// Stores the online amount of players.
    /// </summary>
    public int OnlinePlayers { get; init; }

    /// <summary>
    /// Stores the maximum amount of players.
    /// </summary>
    public int MaximumPlayers { get; init; }

    /// <summary>
    /// Stores the server's unique identifier.
    /// </summary>
    public required long ServerIdentifier { get; init; }

    /// <summary>
    /// Stores the server's game mode.
    /// </summary>
    public required string GameMode { get; init; }
}