using AkkaGameServer.API.Network;

namespace AkkaGameServer.API.Communication.Incoming;

public interface IIncomingHandler
{
    ValueTask HandleIncomingMessage(string connectionId, int header, string message);
}