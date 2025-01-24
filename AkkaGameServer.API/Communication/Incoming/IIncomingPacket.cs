using AkkaGameServer.API.Network;

namespace AkkaGameServer.API.Communication.Incoming;

public interface IIncomingPacket
{
    int Header { get; }
    ValueTask Decode(INetworkClient connection, string message);
}