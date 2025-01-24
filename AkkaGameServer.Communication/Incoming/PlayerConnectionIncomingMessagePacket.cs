using System.Runtime.InteropServices.Marshalling;
using Akka.Actor;
using AkkaGameServer.Actors;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Models.Player.Connection;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Incoming;

public class PlayerConnectionIncomingMessagePacket : IIncomingPacket
{
    private readonly IOutgoingPacket<PlayerConnectionResponse> _outgoingPacket;
    private readonly IActorRef _playerActor; 
    public PlayerConnectionIncomingMessagePacket(IOutgoingPacket<PlayerConnectionResponse> outgoingPacket, ActorResolver actorResolver)
    {
        _outgoingPacket = outgoingPacket;
        _playerActor = actorResolver.GetPlayerActor(); 
    }

    public int Header => 1000;

    public async ValueTask Decode(INetworkClient client, string message) // no need to use message in this scenario...
    {
        var connectionData = new PlayerConnectionData(client.ConnectionId); 
        var response = await _playerActor.Ask<PlayerConnectionResponse>(connectionData);
        await _outgoingPacket.Send(response, client.ConnectionId);
    }
}
