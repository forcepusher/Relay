namespace BananaParty.WebSocketRelay
{
    public interface IDynamicArrayLifecycle<T> where T : IState
    {
        T Create();
        void Delete(T entry);
    }
}
