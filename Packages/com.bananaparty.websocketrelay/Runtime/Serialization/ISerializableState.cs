namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void Serialize(IStateStorage stateStorage);
        void Deserialize(IStateStorage stateStorage);
    }
}
