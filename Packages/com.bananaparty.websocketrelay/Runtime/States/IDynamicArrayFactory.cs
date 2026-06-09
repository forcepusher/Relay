namespace BananaParty.WebSocketRelay
{
    public interface IDynamicArrayFactory<T> where T : IState
    {
        T Create();
        void Dispose(T entry);
    }
}
