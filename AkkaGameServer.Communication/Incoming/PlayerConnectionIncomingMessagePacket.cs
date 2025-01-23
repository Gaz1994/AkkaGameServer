using Akka.Actor;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.Models.Player.Connection;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Incoming;

public class PlayerConnectIncomingMessagePacket(
    IActorRef playerActor,
    IOutgoingPacket<PlayerConnectionResponse> outgoingPacket) : IIncomingPacket
{
    public int Header => 1000;

    public async ValueTask Decode(string message)
    {
        var connectionData = JsonConvert.DeserializeObject<PlayerConnectionData>(message);
        var response = await playerActor.Ask<PlayerConnectionResponse>(connectionData);
        await outgoingPacket.Send(response, response.ConnectionId);
    }
}