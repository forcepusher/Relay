namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        void Serialize();
        void Deserialize();
    }
}
