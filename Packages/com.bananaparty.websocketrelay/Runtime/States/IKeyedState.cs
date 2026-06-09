namespace BananaParty.WebSocketRelay
{
    public interface IKeyedState : IState
    {
        GuidState StateKey { get; }
    }
}
