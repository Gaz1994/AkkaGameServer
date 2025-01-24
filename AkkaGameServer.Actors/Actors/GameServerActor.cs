using Akka.Actor;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Networking;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer.Actors.Actors;

public class GameServerActor : ReceiveActor
{
    private readonly IIncomingHandler _incomingHandler;
    private readonly ConnectionManager _connectionManager; 
    private readonly ILogger _logger;
    private readonly string _connectionId;

    public GameServerActor(IIncomingHandler incomingHandler, ILogger logger, ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager; 
        _incomingHandler = incomingHandler;
        _logger = logger;
        _connectionId = Guid.NewGuid().ToString();

        var client = new AkkaClient(Self, _connectionId); 
        _connectionManager.AddConnection(client);
        _logger.LogInformation("Akka client connected: {ConnectionId}", _connectionId);

        Receive<GameMessage>(async message =>
        {
            await _incomingHandler.HandleIncomingMessage(_connectionId, message.Header, message.Payload);
        });
    }

    protected override void PostStop()
    {
        _connectionManager.RemoveConnection(_connectionId);
        _logger.LogInformation("Akka client disconnected: {ConnectionId}", _connectionId);
        base.PostStop();
    }
}