namespace BananaParty.WebSocketRelay
{
    public class IntegerValueState : IState
    {
        public int Value { get; set; }
        public string StateName { get; private set; }

        public IntegerValueState(string name, int initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteInt(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadInt(StateName);
    }
}
