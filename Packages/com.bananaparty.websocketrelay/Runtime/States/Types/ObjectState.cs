using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ObjectState : IState
    {
        public string StateName { get; }
        private readonly List<IState> _states;

        public ObjectState(string name, List<IState> states)
        {
            StateName = name;
            _states = states;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

        public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
    }
}
