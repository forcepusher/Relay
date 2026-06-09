using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public readonly struct Vector4Int
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }
        public int w { get; }

        public Vector4Int(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    public class Vector4IntValueState : IState
    {
        public string StateName { get; private set; }
        public Vector4Int Value { get; set; }

        private readonly IntegerValueState _x = new("x", 0);
        private readonly IntegerValueState _y = new("y", 0);
        private readonly IntegerValueState _z = new("z", 0);
        private readonly IntegerValueState _w = new("w", 0);
        private readonly List<IState> _components;

        public Vector4IntValueState(string name, Vector4Int initialValue)
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
            Value = new Vector4Int(_x.Value, _y.Value, _z.Value, _w.Value);
        }
    }
}
