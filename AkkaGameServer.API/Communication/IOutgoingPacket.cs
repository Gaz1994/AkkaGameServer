namespace AkkaGameServer.API.Communication;

public interface IOutgoingPacket<in T>
{
    int Header { get; }
    ValueTask Send(T response, string connectionId);
}