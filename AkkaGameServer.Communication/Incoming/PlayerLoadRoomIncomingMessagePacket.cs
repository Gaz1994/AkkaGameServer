using Akka.Actor;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.Models.Player.Rooms;
using AkkaGameServer.Models.Rooms.Loading;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Incoming;

public class PlayerLoadRoomIncomingMessagePacket(
    IActorRef playerActor,
    IOutgoingPacket<RoomLoadResponse> outgoingPacket) : IIncomingPacket
{
    public int Header => 2000; 

    public async ValueTask Decode(string message)
    {
        var roomLoadData = JsonConvert.DeserializeObject<PlayerLoadRoom>(message);
        var response = await playerActor.Ask<RoomLoadResponse>(roomLoadData);
        await outgoingPacket.Send(response, response.Player.ConnectionId);  // Pass connectionId to Send
    }
}