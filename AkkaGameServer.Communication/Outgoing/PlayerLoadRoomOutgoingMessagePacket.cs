using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Network;
using AkkaGameServer.Models.Player.Connection;
using AkkaGameServer.Models.Rooms.Loading;
using Newtonsoft.Json;

namespace AkkaGameServer.Communication.Outgoing;

public class PlayerLoadRoomOutgoingMessagePacket(INetworkSender networkSender) : IOutgoingPacket<RoomLoadResponse>
{
    public int Header => 1010;

    public async ValueTask Send(RoomLoadResponse response, string connectionId)
    {
        var encoded = JsonConvert.SerializeObject(response);
        await networkSender.SendToClient(connectionId, Header, encoded);
    }
}