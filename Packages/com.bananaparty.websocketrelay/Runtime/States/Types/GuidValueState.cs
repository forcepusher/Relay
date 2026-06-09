using System;

namespace BananaParty.WebSocketRelay
{
    public class GuidValueState : IState
    {
        public string StateName { get; private set; }
        public Guid Value { get; set; }

        public GuidValueState(string name, Guid initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteGuid(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadGuid(StateName);
    }
}
