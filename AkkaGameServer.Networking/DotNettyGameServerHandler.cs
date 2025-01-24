using AkkaGameServer.API;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer.Networking;

public class DotNettyGameServerHandler(IOutgoingHandler outgoingHandler, IIncomingHandler incomingHandler,  ILogger logger, ConnectionManager connectionManager) : ChannelHandlerAdapter
{
    public override void ChannelActive(IChannelHandlerContext context)
    {
        var connectionId = context.Channel.Id.AsLongText();
        var client = new DotNettyClient(context, connectionId);
        connectionManager.AddConnection(client);
        logger.LogInformation("DotNetty client connected: {ConnectionId}", connectionId);
    }

    public override void ChannelInactive(IChannelHandlerContext context)
    {
        var connectionId = context.Channel.Id.AsLongText();
        connectionManager.RemoveConnection(connectionId);
        logger.LogInformation("DotNetty client disconnected: {ConnectionId}", connectionId);
    }

    public override async void ChannelRead(IChannelHandlerContext context, object message)
    {
        if (message is not GameMessage gameMessage) 
            return;
        
        var connectionId = context.Channel.Id.AsLongText();
        await incomingHandler.HandleIncomingMessage(connectionId, gameMessage.Header, gameMessage.Payload);
    }
}