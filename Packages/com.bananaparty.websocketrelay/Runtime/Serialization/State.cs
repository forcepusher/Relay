namespace BananaParty.WebSocketRelay
{
    public class IntegerState : IState
    {
        private int _value;

        public IntegerState(int initialValue)
        {
            _value = initialValue;
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
