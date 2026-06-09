using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StaticArrayState<T> : IState where T : IState
    {
        public string StateName { get; }
        private readonly List<T> _states;

        public StaticArrayState(string name, List<T> states)
        {
            StateName = name;
            _states = states;
        }

        public void WriteState(IStateOutput stateOutput) => stateOutput.WriteStaticArray(StateName, ToStateList());

        public void ReadState(IStateInput stateInput) => stateInput.ReadStaticArray(StateName, ToStateList());

        private List<IState> ToStateList()
        {
            var states = new List<IState>(_states.Count);
            foreach (T state in _states)
                states.Add(state);

            return states;
        }
    }
}
