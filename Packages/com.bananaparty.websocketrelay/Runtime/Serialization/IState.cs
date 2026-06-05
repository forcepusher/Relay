namespace BananaParty.WebSocketRelay
{
    public interface IStateNode
    {
        void Serialize(IStateStream stateStream);
        void Deserialize(IStateStream stateStream);

        string OutputNameAndValue();
    }
}
