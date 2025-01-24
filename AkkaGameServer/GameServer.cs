using Akka.Actor;
using AkkaGameServer.Actors.Actors;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Networking;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using WebSocketSharp.Server;

namespace AkkaGameServer;

public class GameServer
{
    private readonly IIncomingHandler _incomingHandler;
    private readonly IOutgoingHandler _outgoingHandler; 
    private readonly ConnectionManager _connectionManager; 
    private readonly ILogger _logger;
    private readonly MultithreadEventLoopGroup _bossGroup;
    private readonly MultithreadEventLoopGroup _workerGroup;

    public GameServer(IIncomingHandler incomingHandler, IOutgoingHandler outgoingHandler, ILogger logger, ConnectionManager connectionManager)
    {
        _incomingHandler = incomingHandler;
        _connectionManager = connectionManager; 
        _logger = logger;
        _bossGroup = new MultithreadEventLoopGroup(1);
        _workerGroup = new MultithreadEventLoopGroup();
    }
    
    public void SetupDotNetty()
    {
        var bootstrap = new ServerBootstrap();
        bootstrap.Group(_bossGroup, _workerGroup)
            .Channel<TcpServerSocketChannel>()
            .Option(ChannelOption.SoBacklog, 100)
            .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
            {
                var pipeline = channel.Pipeline;
                pipeline.AddLast(new DotNettyGameServerHandler(_outgoingHandler, _incomingHandler, _logger, _connectionManager));
            }));
    }

    public void SetupWebSocket() 
    {
        var wssv = new WebSocketServer(8080);
        wssv.AddWebSocketService("/game", () => new WebSocketGameServer(_outgoingHandler, _incomingHandler, _logger, _connectionManager));
        wssv.Start();
    }

    public void SetupAkka()
    {
        var system = ActorSystem.Create("GameSystem");
        
        // TODO: Move to resolver (this not best practice).
        var gameServer = system.ActorOf(Props.Create(() => 
            new GameServerActor(_incomingHandler, _logger, _connectionManager)));
    }
}