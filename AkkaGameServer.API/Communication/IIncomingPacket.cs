namespace AkkaGameServer.API.Communication;

public interface IIncomingPacket
{
    int Header { get; }
    ValueTask Decode(string message);
}