using Akka.Actor;
using AkkaGameServer.Actors;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Models.Player.Rooms;
using AkkaGameServer.Models.Rooms.Loading;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Incoming;

public class PlayerLoadRoomIncomingMessagePacket : IIncomingPacket
{

    private readonly IActorRef _playerActor;
    private readonly IOutgoingPacket<RoomLoadResponse> _outgoingPacket; 
    public PlayerLoadRoomIncomingMessagePacket(ActorResolver actorResolver,
        IOutgoingPacket<RoomLoadResponse> outgoingPacket)
    {
        _playerActor = actorResolver.GetPlayerActor();
        _outgoingPacket = outgoingPacket; 

    }
    public int Header => 2000; 

    public async ValueTask Decode(INetworkClient client, string message)
    {
        var roomLoadData = JsonConvert.DeserializeObject<PlayerLoadRoom>(message);
        var response = await _playerActor.Ask<RoomLoadResponse>(roomLoadData);
        await _outgoingPacket.Send(response, client.ConnectionId);  // Pass connectionId to Send
    }
}