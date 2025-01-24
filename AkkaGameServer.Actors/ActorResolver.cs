using Akka.Actor;

namespace AkkaGameServer.Actors;

public class ActorResolver(ActorSystemProvider actorSystem)
{
    public IActorRef GetPlayerActor() => actorSystem.PlayerActor;
    public IActorRef GetRoomActor() => actorSystem.RoomActor;
}