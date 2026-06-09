namespace BananaParty.WebSocketRelay
{
    public class LongValueState : IState
    {
        public long Value { get; set; }
        public string StateName { get; private set; }

        public LongValueState(string name, long initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteLong(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadLong(StateName);
    }
}
