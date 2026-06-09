using System;

namespace BananaParty.WebSocketRelay
{
    public interface IFactory<T> where T : IState
    {
        T Create(Guid key);
        void Dispose(T entry);
        Guid GetKey(T entry);
    }
}
