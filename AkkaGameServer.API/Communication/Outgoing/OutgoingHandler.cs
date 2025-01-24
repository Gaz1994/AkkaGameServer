using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Network;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer.API.Communication.Outgoing;

public class OutgoingHandler(ILogger logger, ConnectionManager connectionManager) : IOutgoingHandler
{
    public async ValueTask SendToClient(string connectionId, int header, string message)
    {
        var client = connectionManager.GetConnection(connectionId);
        if (client != null)
        {
            await client.SendMessage(header, message);
        }
    }

    public async ValueTask Broadcast(int header, string message)
    {
        var tasks = connectionManager.GetConnections()
            .Select(client => client.SendMessage(header, message).AsTask());
        await Task.WhenAll(tasks);
    }

    public async ValueTask SendToClients(IEnumerable<string> connectionIds, int header, string message)
    {
        var tasks = connectionIds
            .Select(connectionManager.GetConnection)
            .Where(client => client != null)
            .Select(client => client!.SendMessage(header, message).AsTask());
        await Task.WhenAll(tasks);
    }
}
