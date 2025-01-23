namespace AkkaGameServer.API.Communication;
public class MessageHandler
{
    private readonly Dictionary<int, IIncomingPacket> _messageRegistry;

    public MessageHandler(IEnumerable<IIncomingPacket> registeredPackets)
    {
        _messageRegistry = registeredPackets.ToDictionary(x => x.Header);
    }

    public async ValueTask HandleMessage(int header, string message)
    {
        if (!_messageRegistry.TryGetValue(header, out var packet))
        {
            throw new InvalidOperationException($"No message registered for header: {header}");
        }

        await packet.Decode(message);
    }
}