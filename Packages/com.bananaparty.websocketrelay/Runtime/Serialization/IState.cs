namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        void Save();
        void Load();
    }
}
