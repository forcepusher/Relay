namespace BananaParty.WebSocketRelay
{
    public interface IStateNode
    {
        void StepInto(string name);
        void StepOut();
    }
}
