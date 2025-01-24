using AkkaGameServer.API;
using AkkaGameServer.API.Network;
using DotNetty.Transport.Channels;

namespace AkkaGameServer.Networking;

// DotNetty
public class DotNettyClient(IChannelHandlerContext context, string connectionId) : INetworkClient
{
    public string ConnectionId { get; } = connectionId;

    public ValueTask SendMessage(int header, string message) => new(context.WriteAndFlushAsync(new GameMessage(header, message)));
}