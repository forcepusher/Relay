namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void Serialize(StateStorage stateStorage);
        void Deserialize(StateStorage stateStorage);
    }
}
