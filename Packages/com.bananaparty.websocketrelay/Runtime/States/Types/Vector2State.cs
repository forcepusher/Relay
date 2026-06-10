using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2State : IState
    {
        public string StateName { get; private set; }
        public Vector2 Value { get; set; }

        private readonly FloatState _x = new("x", 0f);
        private readonly FloatState _y = new("y", 0f);
        private readonly List<IState> _components;

        public Vector2State(string name, Vector2 initialValue)
        {
            StateName = name;
            Value = initialValue;
            _components = new List<IState> { _x, _y };
            SyncComponentsFromValue();
        }

        public void WriteState(IStateOutput stateOutput)
        {
            SyncComponentsFromValue();
            stateOutput.WriteObject(StateName, _components);
        }

        public void ReadState(IStateInput stateInput)
        {
            stateInput.ReadObject(StateName, _components);
            SyncValueFromComponents();
        }

        public void CopyFrom(IState other)
        {
            if (other is Vector2State otherState)
                this.Value = otherState.Value;
        }

        private void SyncComponentsFromValue()
        {
            _x.Value = Value.x;
            _y.Value = Value.y;
        }

        private void SyncValueFromComponents()
        {
            Value = new Vector2(_x.Value, _y.Value);
        }
    }
}
