using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Color32State : IStateObject
    {
        private Color32 _value;
        private string _name;

        public Color32State(Color32 initialValue, string name = nameof(Color32State))
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteColor32(_value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            _value = stateStream.ReadColor32();
        }

        public Color32 Value
        {
            get => _value;
            set => _value = value;
        }

        public string OutputNameAndValue()
        {
            return $"\"{_name}\": \"{_value}\"";
        }
    }
}
