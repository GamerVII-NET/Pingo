using System.Net.Sockets;
using Pingo.Networking.Bedrock.Protocol;
using Pingo.Networking.Bedrock.Protocol.Packets;
using Pingo.Status;

namespace Pingo.Networking.Bedrock;

internal sealed class BedrockClient(Socket socket) : IDisposable
{
    public async Task<BedrockStatus> PingAsync(CancellationToken cancellationToken)
    {
        await socket.WriteAsync(
            new UnconnectedPingPacket
            {
                Time = DateTime.UtcNow.Millisecond,
                Client = Random.Shared.Next()
            },
            cancellationToken);

        var message = await socket.ReadAsync(cancellationToken);
        var pong = message.As<UnconnectedPongPacket>();

        return new BedrockStatus
        {
            Message = pong.Message,
            Server = pong.Server,
            Time = pong.Time
        };
    }

    public void Dispose()
    {
        socket.Dispose();
    }
}