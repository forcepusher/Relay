using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Vector2IntState : IState
    {
        public string StateName { get; private set; }
        public Vector2Int Value { get; set; }

        private readonly IntegerState _x = new("x", 0);
        private readonly IntegerState _y = new("y", 0);
        private readonly List<IState> _components;

        public Vector2IntState(string name, Vector2Int initialValue)
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

        private void SyncComponentsFromValue()
        {
            _x.Value = Value.x;
            _y.Value = Value.y;
        }

        private void SyncValueFromComponents()
        {
            Value = new Vector2Int(_x.Value, _y.Value);
        }
    }
}
