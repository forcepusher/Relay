using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector3State : IState
    {
        public string StateName { get; private set; }
        public Vector3 Value { get; set; }

        private readonly FloatState _x = new("x", 0f);
        private readonly FloatState _y = new("y", 0f);
        private readonly FloatState _z = new("z", 0f);
        private readonly List<IState> _components;

        public Vector3State(string name, Vector3 initialValue)
        {
            StateName = name;
            Value = initialValue;
            _components = new List<IState> { _x, _y, _z };
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
            if (other is Vector3State otherState)
                this.Value = otherState.Value;
        }

        private void SyncComponentsFromValue()
        {
            _x.Value = Value.x;
            _y.Value = Value.y;
            _z.Value = Value.z;
        }

        private void SyncValueFromComponents()
        {
            Value = new Vector3(_x.Value, _y.Value, _z.Value);
        }
    }
}
