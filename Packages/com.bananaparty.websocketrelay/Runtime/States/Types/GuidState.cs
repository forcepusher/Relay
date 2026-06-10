using System;

namespace BananaParty.WebSocketRelay
{
    public class GuidState : IState
    {
        public string StateName { get; private set; }
        public Guid Value { get; set; }

        public GuidState(string name, Guid initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteGuid(StateName, Value);

        public void ReadState(IStateInput stateInput) => Value = stateInput.ReadGuid(StateName);

        public void CopyFrom(IState other)
        {
            if (other is GuidState otherState)
                this.Value = otherState.Value;
        }
    }
}
