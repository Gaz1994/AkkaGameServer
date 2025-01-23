using System.Collections.Concurrent;
using AkkaGameServer.API.Communication;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer.API.Network;

public class NetworkManager(
    MessageHandler messageHandler,
    ILogger<NetworkManager> logger)
    : INetworkSender
{

    public async ValueTask HandleIncomingMessage(int header, string message)
    {
        try
        {
            await messageHandler.HandleMessage(header, message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling message with header {Header} from connection {ConnectionId}", header);
        }
    }

    public async ValueTask SendToClient(string connectionId, int header, string message)
    {
        //
    }

    public async ValueTask Broadcast(int header, string message)
    {
        //
    }

    public async ValueTask SendToClients(IEnumerable<string> connectionIds, int header, string message)
    {
        //
    }
}
