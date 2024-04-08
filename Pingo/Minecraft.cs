using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.Logging.Abstractions;
using Pingo.Networking.Bedrock;
using Pingo.Networking.Java;
using Pingo.Status;

namespace Pingo;

/// <summary>
/// A helper static class that provides methods for pinging a Minecraft server.
/// </summary>
public static class Minecraft
{
    /// <summary>
    /// Attempts to ping a Minecraft server.
    /// </summary>
    /// <param name="options">Options for the asynchronous ping operation.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous ping operation, which wraps the status of the server.</returns>
    /// <exception cref="InvalidOperationException">Unknown protocol type.</exception>
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

/// <summary>
/// Stores options to ping a Minecraft server.
/// </summary>
public sealed class MinecraftPingOptions
{
    /// <summary>
    /// The server's address.
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// The server's port.
    /// </summary>
    /// <remarks>
    /// 19132 is usually for Bedrock servers, and 25565 is usually for Java servers.
    /// </remarks>
    public required ushort Port { get; init; }

    /// <summary>
    /// Specifies when should the asynchronous ping operation time out.
    /// </summary>
    public TimeSpan TimeOut { get; init; } = TimeSpan.FromSeconds(5);
}