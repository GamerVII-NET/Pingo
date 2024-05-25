﻿using Pingo.Networking.Java.Protocol.Components;

namespace Pingo.Status;

/// <summary>
/// Represents a Minecraft: Java edition server status.
/// </summary>
public sealed class JavaStatus : StatusBase
{
    /// <summary>
    /// Stores possibly multiple lines of MOTDs.
    /// </summary>
    public string[] MessagesOfTheDay { get; init; }

    /// <summary>
    /// Stores the online amount of players.
    /// </summary>
    public int OnlinePlayers { get; init; }

    /// <summary>
    /// Stores the maximum amount of players.
    /// </summary>
    public int MaximumPlayers { get; init; }

    /// <summary>
    /// Stores the protocol version of the server.
    /// </summary>
    public int Protocol { get; init; }

    /// <summary>
    /// Stores the server software's name of the server.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Stores the server's icon encoded in base-64.
    /// </summary>
    public string Favicon { get; init; }

    internal JavaStatus(ServerStatus status)
    {
        MessagesOfTheDay =
        [
            ..status.Description.Extra.Select(extra => extra.Text),
            status.Description.Text
        ];

        OnlinePlayers = status.PlayerInformation.Online;
        MaximumPlayers = status.PlayerInformation.Max;
        Protocol = status.Version.Protocol;
        Name = status.Version.Name;
        Favicon = status.Favicon;
    }
}
