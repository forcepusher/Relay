using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class ColorValueState : IState
    {
        public string StateName { get; private set; }
        public Color Value { get; set; }

        private readonly FloatValueState _r = new("r", 0f);
        private readonly FloatValueState _g = new("g", 0f);
        private readonly FloatValueState _b = new("b", 0f);
        private readonly FloatValueState _a = new("a", 1f);
        private readonly List<IState> _components;

        public ColorValueState(string name, Color initialValue)
        {
            StateName = name;
            Value = initialValue;
            _components = new List<IState> { _r, _g, _b, _a };
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
            _r.Value = Value.r;
            _g.Value = Value.g;
            _b.Value = Value.b;
            _a.Value = Value.a;
        }

        private void SyncValueFromComponents()
        {
            Value = new Color(_r.Value, _g.Value, _b.Value, _a.Value);
        }
    }
}
