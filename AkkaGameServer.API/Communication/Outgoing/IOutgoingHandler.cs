namespace AkkaGameServer.API.Communication.Outgoing;

public interface IOutgoingHandler
{
    
    ValueTask SendToClient(string connectionId, int header, string message);
    ValueTask Broadcast(int header, string message);
    ValueTask SendToClients(IEnumerable<string> connectionIds, int header, string message);
}