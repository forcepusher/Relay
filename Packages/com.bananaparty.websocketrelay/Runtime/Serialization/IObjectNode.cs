namespace BananaParty.WebSocketRelay
{
    public interface IObjectNode
    {
        void Serialize(IStateNode stateStream);
        void Deserialize(IStateNode stateStream);

        string OutputNameAndValue();
    }
}
