namespace BananaParty.WebSocketRelay
{
    public interface IStateNode
    {
        void StepInto(string name);
        void StepOut();

        void Write(string name, IObjectNode objectNode);
        void Read(string name, IObjectNode objectNode);
    }
}
