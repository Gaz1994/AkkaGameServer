using Akka.Actor;
using AkkaGameServer.Actors.Actors;

namespace AkkaGameServer.Actors;

public class ActorSystemProvider
{

    public ActorSystemProvider()
    {
        System = ActorSystem.Create("GameSystem");
        RoomActor = System.ActorOf(Props.Create(() => new RoomActor()), "roomActor");
        PlayerActor = System.ActorOf(Props.Create(() => new PlayerActor(RoomActor)), "playerActor");
    }

    public IActorRef PlayerActor { get; }
    public IActorRef RoomActor { get;  }
    private ActorSystem System { get; }
}
