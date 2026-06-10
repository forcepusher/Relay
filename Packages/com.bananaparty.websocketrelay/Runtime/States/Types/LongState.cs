namespace BananaParty.WebSocketRelay
{
    public class LongState : IState
    {
        public long Value { get; set; }
        public string StateName { get; private set; }

        public LongState(string name, long initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteLong(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadLong(StateName);

        public void CopyFrom(IState other)
        {
            if (other is LongState otherState)
                this.Value = otherState.Value;
        }
    }
}
