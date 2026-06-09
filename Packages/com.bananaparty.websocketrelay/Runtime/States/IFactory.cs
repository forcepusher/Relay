using System;

namespace BananaParty.WebSocketRelay
{
    public interface IFactory<T>
    {
        T Create();
        void Dispose(T entry);
        Guid GetKey(T entry);
    }
}
