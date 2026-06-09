namespace BananaParty.WebSocketRelay
{
    public class ByteState : IState
    {
        public byte Value { get; set; }
        public string StateName { get; private set; }

        public ByteState(string name, byte initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteByte(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadByte(StateName);
    }
}
