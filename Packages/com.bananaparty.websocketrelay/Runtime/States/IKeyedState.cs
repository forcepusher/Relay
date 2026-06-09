using System;

namespace BananaParty.WebSocketRelay
{
    public interface IKeyedState : IState
    {
        Guid StateKey { get; }
    }
}
