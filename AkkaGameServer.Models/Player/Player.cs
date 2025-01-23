using AkkaGameServer.Models.Player.Connection;

namespace AkkaGameServer.Models.Player;

public class Player(Guid playerId, Guid currentRoomId, string connectionId)
{
    public Guid PlayerId { get; set; } = playerId;
    public Guid CurrentRoomId { get; set; } = currentRoomId;
    public string ConnectionId { get; set; } = connectionId;
}
