using AkkaGameServer.Actors;
using AkkaGameServer.Actors.Actors;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Communication.Incoming;
using AkkaGameServer.API.Communication.Outgoing;
using AkkaGameServer.API.Network;
using AkkaGameServer.Communication.Incoming;
using AkkaGameServer.Communication.Outgoing;
using AkkaGameServer.Models.Player.Connection;
using AkkaGameServer.Models.Rooms.Loading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer;

public static class DependencyUtils
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        // Basic services
        services.AddLogging();
        services.AddSingleton<ILogger>(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("Default"));
    
        // Actor system
        services.AddSingleton<ActorSystemProvider>();
        services.AddSingleton<ActorResolver>();

        // Network and packets
        //services.AddSingleton<NetworkManager>();
        services.AddSingleton<ConnectionManager>(); 
        services.AddSingleton<IOutgoingHandler, OutgoingHandler>();
        
        services.AddSingleton<IOutgoingPacket<PlayerConnectionResponse>, PlayerConnectOutgoingMessagePacket>();
        services.AddSingleton<IOutgoingPacket<RoomLoadResponse>, PlayerLoadRoomOutgoingMessagePacket>();
        services.AddSingleton<IIncomingPacket, PlayerConnectionIncomingMessagePacket>();
        services.AddSingleton<IIncomingPacket, PlayerLoadRoomIncomingMessagePacket>(); 
        
        services.AddSingleton<IIncomingHandler, IncomingHandler>();
        
    
        // Server
        services.AddSingleton<GameServer>();
    }
}