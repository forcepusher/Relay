namespace BananaParty.WebSocketRelay
{
    public interface ISerializeState
    {
        void Serialize(IStateStorage stateStorage);
        void Deserialize(IStateStorage stateStorage);
    }
}
