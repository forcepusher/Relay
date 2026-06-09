namespace BananaParty.WebSocketRelay
{
    public class StringValueState : IState
    {
        public string StateName { get; private set; }
        public string Value { get; set; }

        public StringValueState(string name, string initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteString(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadString(StateName);
    }
}
