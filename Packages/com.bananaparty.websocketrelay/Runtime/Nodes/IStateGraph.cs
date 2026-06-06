namespace BananaParty.WebSocketRelay
{
    public interface IStateGraph<T>
    {
        void StartChildGroup(string name);
        void WriteEntry(string name, T data);
        void EndChildGroup();
    }
}
