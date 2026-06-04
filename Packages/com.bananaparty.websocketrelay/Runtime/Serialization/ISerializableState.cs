namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState<TKey>
    {
        void Serialize(IStateStorage<TKey> stateStorage);
        void Deserialize(IStateStorage<TKey> stateStorage);
    }
}
