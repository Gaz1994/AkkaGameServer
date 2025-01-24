namespace AkkaGameServer.Models.Rooms.Loading;

public record RoomLoadCommand(Player.Player Player, Guid RoomId); 