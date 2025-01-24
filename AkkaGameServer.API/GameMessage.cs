namespace AkkaGameServer.API;

public class GameMessage(int header, string payload)
{
    public int Header { get; } = header;
    public string Payload { get; } = payload;
}
