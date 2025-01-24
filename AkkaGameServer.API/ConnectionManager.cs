using System.Collections.Concurrent;
using AkkaGameServer.API.Network;

namespace AkkaGameServer.API;

public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, INetworkClient> _connections = new();
    
    public void AddConnection(INetworkClient client) => _connections.TryAdd(client.ConnectionId, client);
    public void RemoveConnection(string connectionId) => _connections.TryRemove(connectionId, out _);
    public INetworkClient? GetConnection(string connectionId) => _connections.GetValueOrDefault(connectionId);
    public IEnumerable<INetworkClient> GetConnections() => _connections.Values;
}