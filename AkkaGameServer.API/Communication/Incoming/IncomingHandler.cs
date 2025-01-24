using AkkaGameServer.API.Network;

namespace AkkaGameServer.API.Communication.Incoming;
public class IncomingHandler : IIncomingHandler
{
    private readonly Dictionary<int, IIncomingPacket> _messageRegistry;
    private readonly ConnectionManager _connectionManager; 

    public IncomingHandler(IEnumerable<IIncomingPacket> registeredPackets, ConnectionManager connectionManager)
    {
        _messageRegistry = registeredPackets.ToDictionary(x => x.Header);
        _connectionManager = connectionManager; 
    }
    
    
    
    public async ValueTask HandleIncomingMessage(string connectionId, int header, string message)
    {
        try
        {
            var connection = _connectionManager.GetConnection(connectionId);
            if(connection != null)
            {
                await HandleMessage(connection, header, message);
            }
        }
        catch (Exception ex)
        {
            //logger.LogError(ex, "Error handling message with header {Header} from connection {ConnectionId}", 
            //header, connectionId);
        }
    }

    public async ValueTask HandleMessage(INetworkClient connection, int header, string message)
    {
        if (!_messageRegistry.TryGetValue(header, out var packet))
        {
            throw new InvalidOperationException($"No message registered for header: {header}");
        }

        await packet.Decode(connection, message);
    }
}