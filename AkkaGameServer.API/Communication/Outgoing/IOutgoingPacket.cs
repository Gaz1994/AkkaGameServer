namespace AkkaGameServer.API.Communication.Outgoing;

public interface IOutgoingPacket<in T>
{
    int Header { get; }
    
    // reason why we dont use networkClient here instead of connectionId is because of circular dependency
    ValueTask Send(T response, string connectionId);
}