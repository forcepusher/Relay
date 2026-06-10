namespace BananaParty.WebSocketRelay
{
    public class DoubleState : IState
    {
        public string StateName { get; private set; }
        public double Value { get; set; }

        public DoubleState(string name, double initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteDouble(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadDouble(StateName);

        public void CopyFrom(IState other)
        {
            if (other is DoubleState otherState)
                this.Value = otherState.Value;
        }
    }
}
