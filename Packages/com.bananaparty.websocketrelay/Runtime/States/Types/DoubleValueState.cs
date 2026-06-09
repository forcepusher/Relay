namespace BananaParty.WebSocketRelay
{
    public class DoubleValueState : IState
    {
        public string StateName { get; private set; }
        public double Value { get; set; }

        public DoubleValueState(string name, double initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteDouble(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadDouble(StateName);
    }
}
