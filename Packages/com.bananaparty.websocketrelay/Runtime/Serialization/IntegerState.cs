namespace BananaParty.WebSocketRelay
{
    public class IntegerState : IState
    {
        private int _value;
        private string _name;

        public IntegerState(int initialValue, string name = nameof(IntegerState))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteInt(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadInt();
        }
    }
}
