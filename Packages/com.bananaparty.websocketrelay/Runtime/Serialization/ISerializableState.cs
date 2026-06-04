namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void Serialize(StateStorageGraph stateStorage);
        void Deserialize(StateStorageGraph stateStorage);
    }
}
