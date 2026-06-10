namespace BananaParty.WebSocketRelay
{
    public class StringState : IState
    {
        public string StateName { get; private set; }
        public string Value { get; set; }

        public StringState(string name, string initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteString(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadString(StateName);

        public void CopyFrom(IState other)
        {
            if (other is StringState otherState)
                this.Value = otherState.Value;
        }
    }
}
