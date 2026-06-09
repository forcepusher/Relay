namespace BananaParty.WebSocketRelay
{
    public class FloatState : IState
    {
        public string StateName { get; private set; }
        public float Value { get; set; }

        public FloatState(string name, float initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteFloat(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadFloat(StateName);
    }
}
