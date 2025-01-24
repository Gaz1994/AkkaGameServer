using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Models.Player.Connection;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Outgoing;

public class PlayerConnectOutgoingMessagePacket(IOutgoingHandler outgoingHandler) : IOutgoingPacket<PlayerConnectionResponse>
{
    public int Header => 1010;

    public async ValueTask Send(PlayerConnectionResponse response, string connectionId)
    {
        var encoded = JsonConvert.SerializeObject(response);
        await outgoingHandler.SendToClient(connectionId, Header, encoded);
    }
}