namespace AkkaGameServer.Models.Player.Rooms;

public record PlayerLoadCommand(Guid PlayerId, Guid RoomId);