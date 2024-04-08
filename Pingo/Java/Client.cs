using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Connections;
using Pingo.Java.Protocol;
using Pingo.Java.Protocol.Components;
using Pingo.Java.Protocol.Packets;

namespace Pingo.Java;

internal sealed class Client(ConnectionContext connection) : IAsyncDisposable
{
    private readonly JsonSerializerOptions options =
        new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
            }
        };

    public async Task<ServerStatus?> PingAsync(
        string address,
        ushort port,
        CancellationToken cancellationToken)
    {
        IOutgoingPacket[] initial =
        [
            new HandshakePacket
            {
                ProtocolVersion = -1,
                Address = address,
                Port = port,
                NextState = 1
            },
            new StatusRequestPacket()
        ];

        foreach (var packet in initial)
        {
            await connection.Transport.WriteAsync(packet);
        }

        var message = await connection.Transport.ReadAsync(cancellationToken);
        var response = message?.As<StatusResponsePacket>();

        return response is not null
            ? JsonSerializer.Deserialize<ServerStatus>(response.Status, options)
            : null;
    }

    public async ValueTask DisposeAsync()
    {
        await connection.DisposeAsync();
    }
}