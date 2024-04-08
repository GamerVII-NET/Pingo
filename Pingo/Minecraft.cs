using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.Logging.Abstractions;
using Pingo.Networking.Bedrock;
using Pingo.Networking.Java;
using Pingo.Status;

namespace Pingo;

public static class Minecraft
{
    public static async Task<StatusBase?> PingAsync(MinecraftPingOptions options, CancellationToken cancellationToken = default)
    {
        using var timeOutSource = new CancellationTokenSource();
        timeOutSource.CancelAfter(options.TimeOut);

        using var source = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            timeOutSource.Token);

        Socket? socket;
        var endPoint = new IPEndPoint(IPAddress.Parse(options.Address), options.Port);

        try
        {
            // According to my made-up statistics there are more Java servers than Bedrock.
            socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(endPoint, source.Token);
        }
        catch (SocketException)
        {
            socket = new Socket(endPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            await socket.ConnectAsync(endPoint, source.Token);
        }

        switch (socket.ProtocolType)
        {
            case ProtocolType.Tcp:
            {
                using var factory = new SocketConnectionContextFactory(
                    new SocketConnectionFactoryOptions(),
                    NullLogger.Instance);

                await using var java = new JavaClient(factory.Create(socket));
                var status = await java.PingAsync(options.Address, options.Port, source.Token);

                return status is not null
                    ? new JavaStatus(status)
                    : null;
            }

            case ProtocolType.Udp:
            {
                using var bedrock = new BedrockClient(socket);
                return await bedrock.PingAsync(cancellationToken);
            }

            default:
                throw new InvalidOperationException("Unknown protocol type.");
        }
    }
}

public sealed class MinecraftPingOptions
{
    public required string Address { get; init; }

    public required ushort Port { get; init; }

    public TimeSpan TimeOut { get; init; } = TimeSpan.FromSeconds(5);
}