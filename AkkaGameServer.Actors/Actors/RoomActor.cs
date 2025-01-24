using System.Collections.Concurrent;
using Akka.Actor;
using AkkaGameServer.Models.Rooms.Loading;
using Microsoft.Extensions.Logging;

namespace AkkaGameServer.Actors.Actors;

public class RoomActor : ReceiveActor
{
    private readonly ConcurrentBag<Guid> _roomIds = []; // demo list.
    public RoomActor()
    {
        
        Receive<RoomLoadCommand>(message =>
        {
            var findRoom = _roomIds.FirstOrDefault(x => x == message.RoomId);
            if (findRoom == Guid.Empty)
                throw new ArgumentNullException(); // better to return message but we can leave it for now. 

            message.Player.CurrentRoomId = findRoom; 
            
            Sender.Tell(new RoomLoadResponse(message.Player));
        });
    }
}