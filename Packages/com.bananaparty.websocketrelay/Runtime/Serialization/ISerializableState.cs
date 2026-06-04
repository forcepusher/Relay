namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState
    {
        void BuildGraph(StateGraph stateGraph);

        void Serialize(StateGraph stateStorage);
        void Deserialize(StateGraph stateStorage);
    }
}
