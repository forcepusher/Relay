using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class QuaternionState : IState
    {
        public string StateName { get; private set; }
        public Quaternion Value { get; set; }

        private readonly FloatState _x = new("x", 0f);
        private readonly FloatState _y = new("y", 0f);
        private readonly FloatState _z = new("z", 0f);
        private readonly FloatState _w = new("w", 1f);
        private readonly List<IState> _components;

        public QuaternionState(string name, Quaternion initialValue)
        {
            StateName = name;
            Value = initialValue;
            _components = new List<IState> { _x, _y, _z, _w };
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

        private void SyncComponentsFromValue()
        {
            _x.Value = Value.x;
            _y.Value = Value.y;
            _z.Value = Value.z;
            _w.Value = Value.w;
        }

        private void SyncValueFromComponents()
        {
            Value = new Quaternion(_x.Value, _y.Value, _z.Value, _w.Value);
        }
    }
}
