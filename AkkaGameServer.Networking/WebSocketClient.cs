using AkkaGameServer.API.Network;
using WebSocketSharp;

namespace AkkaGameServer.Networking;

public class WebSocketClient(WebSocket webSocket, string connectionId) : INetworkClient
{
    public string ConnectionId { get; } = connectionId;

    public ValueTask SendMessage(int header, string message)
    {
        webSocket.Send($"{header}|{message}");
        return ValueTask.CompletedTask;
    }
}