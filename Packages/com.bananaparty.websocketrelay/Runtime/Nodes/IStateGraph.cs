namespace BananaParty.WebSocketRelay
{
    public interface IStateGraph<T>
    {
        void StartChildGroup(string name);
        void Write(string name, T data);
        void EndChildGroup();
    }
}
