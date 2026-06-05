namespace BananaParty.WebSocketRelay
{
    public interface IStateObject
    {
        void Serialize(IStateStream stateStream);
        void Deserialize(IStateStream stateStream);

        string OutputNameAndValue();
    }
}
