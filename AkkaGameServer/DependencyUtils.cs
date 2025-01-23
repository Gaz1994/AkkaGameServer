using AkkaGameServer.Actors;
using AkkaGameServer.API;
using AkkaGameServer.API.Communication;
using AkkaGameServer.API.Network;
using AkkaGameServer.Communication.Incoming;
using AkkaGameServer.Communication.Outgoing;
using AkkaGameServer.Models.Player.Connection;
using AkkaGameServer.Models.Rooms.Loading;
using Microsoft.Extensions.DependencyInjection;

namespace AkkaGameServer;

public static class DependencyUtils
{
    public static IServiceCollection AddGameSystem(this IServiceCollection services)
    {
        // Register ActorSystem - this will manage all actors globally
        services.AddSingleton<ActorSystemProvider>();

        // Register Network Components
        services.AddSingleton<INetworkSender, NetworkManager>();
        
        // Register Outgoing Packets
        services.AddSingleton<IOutgoingPacket<PlayerConnectionResponse>, PlayerConnectOutgoingMessagePacket>();
        services.AddSingleton<IOutgoingPacket<RoomLoadResponse>, PlayerLoadRoomOutgoingMessagePacket>();

        // Register base MessageHandler
        services.AddSingleton<MessageHandler>();

        // Register packet factories - these will get the correct actor reference based on connectionId
        services.AddSingleton<IIncomingPacket, PlayerConnectIncomingMessagePacket>();
        services.AddSingleton<IIncomingPacket, PlayerLoadRoomIncomingMessagePacket>(); 
        

        return services;
    }
}