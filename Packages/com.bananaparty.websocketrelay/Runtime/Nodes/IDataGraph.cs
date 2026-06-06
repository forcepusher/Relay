namespace BananaParty.WebSocketRelay
{
    public interface IDataGraph<T>
    {
        void StartChildGroup(string name);
        void Write(string name, T data);
        void EndChildGroup();
    }
}
