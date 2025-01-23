using System.Collections.Concurrent;
using Akka.Actor;
using AkkaGameServer.Models.Player;
using AkkaGameServer.Models.Player.Connection;
using AkkaGameServer.Models.Player.Movement;
using AkkaGameServer.Models.Player.Rooms;
using AkkaGameServer.Models.Rooms.Loading;

namespace AkkaGameServer.Actors.Actors;

public class PlayerActor : ReceiveActor
{
    private readonly ConcurrentBag<Player> _players = [];

    public PlayerActor(IActorRef roomActor)
    {
        Receive<PlayerConnectionData>(message =>
        {
            // some demo code. 
            var playerAlreadyConnected = _players.FirstOrDefault(x => x.ConnectionId == message.ConnectionId);
            if (playerAlreadyConnected is not null)
                throw new Exception("Player with: " + message.ConnectionId + " already exists!");

            var newPlayer = new Player(Guid.NewGuid(), Guid.Empty, message.ConnectionId); 
            _players.Add(newPlayer);
            
            Sender.Tell(new PlayerConnectionResponse(message.ConnectionId, newPlayer));
        });

        Receive<PlayerMoveData>(message =>
        {
            // empty for now...
        });

        Receive<PlayerLoadRoom>(async message =>
        {
            var findPlayer = _players.FirstOrDefault(x => x.PlayerId == message.PlayerId); 
            ArgumentNullException.ThrowIfNull(findPlayer);

            var roomLoadData = new RoomLoadData(findPlayer, message.RoomId); 
            // Forward to room actor
            var result = await roomActor.Ask<RoomLoadResponse>(roomLoadData);
            
            // send back to caller packet. 
            Sender.Tell(result);
        });
    }
}