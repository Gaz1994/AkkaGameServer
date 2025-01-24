namespace AkkaGameServer.API.Network;

public interface INetworkClient
{
    ValueTask SendMessage(int header, string message);
    string ConnectionId { get; }
}