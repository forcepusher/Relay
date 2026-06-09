namespace BananaParty.WebSocketRelay
{
    public class BoolValueState : IState
    {
        public string StateName { get; private set; }
        public bool Value { get; set; }

        public BoolValueState(string name, bool initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteBool(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadBool(StateName);
    }
}
