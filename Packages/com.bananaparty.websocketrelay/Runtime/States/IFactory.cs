using System;

namespace BananaParty.WebSocketRelay
{
    public interface IFactory<T> where T : IKeyedState
    {
        T Create(Guid key);
        void Dispose(T entry);
    }
}
