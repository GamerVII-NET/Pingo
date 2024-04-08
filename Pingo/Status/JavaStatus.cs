using Pingo.Networking.Java.Protocol.Components;

namespace Pingo.Status;

public sealed class JavaStatus : StatusBase
{
    public string[] MessagesOfTheDay { get; init; }

    public int OnlinePlayers { get; init; }

    public int MaximumPlayers { get; init; }

    public int Protocol { get; init; }

    public string Name { get; init; }

    public string Favicon { get; init; }

    internal JavaStatus(ServerStatus status)
    {
        MessagesOfTheDay =
        [
            ..status.Description.Extra?.Select(extra => extra.Text),
            status.Description.Text
        ];

        OnlinePlayers = status.PlayerInformation.Online;
        MaximumPlayers = status.PlayerInformation.Max;
        Protocol = status.Version.Protocol;
        Name = status.Version.Name;
        Favicon = status.Favicon;
    }
}