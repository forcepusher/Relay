using System;

namespace BananaParty.WebSocketRelay
{
    public interface IKeyedState : IState
    {
        Guid Key { get; }
    }
}
