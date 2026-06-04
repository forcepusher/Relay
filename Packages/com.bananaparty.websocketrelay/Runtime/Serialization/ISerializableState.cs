namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState 
    {
        void Serialize(StateGraph stateStorage);
        void Deserialize(StateGraph stateStorage);
    }
}
