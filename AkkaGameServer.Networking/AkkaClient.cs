using Akka.Actor;
using AkkaGameServer.API;
using AkkaGameServer.API.Network;

namespace AkkaGameServer.Networking;

public class AkkaClient(IActorRef connection, string connectionId) : INetworkClient
{
    public string ConnectionId { get; } = connectionId;

    public ValueTask SendMessage(int header, string message)
    {
        connection.Tell(new GameMessage(header, message));
        return ValueTask.CompletedTask;
    }
}