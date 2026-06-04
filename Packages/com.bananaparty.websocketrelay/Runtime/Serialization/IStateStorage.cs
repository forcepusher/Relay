namespace BananaParty.WebSocketRelay
{
    public interface IStateStorage
    {
        void Write();
        void Read();
    }
}
