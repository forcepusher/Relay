namespace BananaParty.WebSocketRelay
{
    public interface IObjectNode
    {
        void Serialize(IStateStream stateStream);
        void Deserialize(IStateStream stateStream);

        string OutputNameAndValue();
    }
}
