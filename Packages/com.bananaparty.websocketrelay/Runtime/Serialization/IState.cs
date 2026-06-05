namespace BananaParty.WebSocketRelay
{
    public interface IState
    {
        void Serialize(IStateStream stateStream);
        void Deserialize(IStateStream stateStream);
    }
}
