namespace BananaParty.WebSocketRelay
{
    public class IntegerState : IState
    {
        public int Value { get; set; }
        public string StateName { get; private set; }

        public IntegerState(string name, int initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteInt(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadInt(StateName);
    }
}
