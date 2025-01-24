using AkkaGameServer.API;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using Microsoft.Extensions.Logging;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AkkaGameServer.Networking;


public class WebSocketGameServer : WebSocketBehavior
{
    private readonly IOutgoingHandler _outgoingHandler;
    private readonly IIncomingHandler _incomingHandler; 
    private readonly ConnectionManager _connectionManager; 
    private readonly ILogger _logger;
    private string _connectionId; 

    public WebSocketGameServer(IOutgoingHandler outgoingHandler, IIncomingHandler incomingHandler, ILogger logger, ConnectionManager connectionManager)
    {
        _incomingHandler = incomingHandler; 
        _outgoingHandler = outgoingHandler;
        _logger = logger;
        _connectionManager = connectionManager; 
    }

    protected override void OnOpen()
    {
        _connectionId = ID;
        var client = new WebSocketClient(Context.WebSocket, _connectionId);
        _connectionManager.AddConnection(client);
        _logger.LogInformation("WebSocket client connected: {ConnectionId}", _connectionId);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        _connectionManager.RemoveConnection(_connectionId);
        _logger.LogInformation("WebSocket client disconnected: {ConnectionId}", _connectionId);
    }

    protected override async void OnMessage(MessageEventArgs e)
    {
        var parts = e.Data.Split('|');
        if (parts.Length == 2 && int.TryParse(parts[0], out int header))
        {
            await _incomingHandler.HandleIncomingMessage(_connectionId, header, parts[1]);
        }
    }
}