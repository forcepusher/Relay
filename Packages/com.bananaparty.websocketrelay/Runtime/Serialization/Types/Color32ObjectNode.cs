using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Color32ObjectNode : IObjectNode
    {
        private Color32 _value;
        private string _name;

        public Color32ObjectNode(string name, Color32 initialValue)
        {
            _value = initialValue;
            _name = name;
        }

        public void Serialize(IStateNode stateStream)
        {
            stateStream.WriteColor32(_name, _value);
        }

        public void Deserialize(IStateNode stateStream)
        {
            _value = stateStream.ReadColor32(_name);
        }

        public Color32 Value
        {
            get => _value;
            set => _value = value;
        }

        public string OutputNameAndValue()
        {
            return Json.ConvertToText(_name, _value);
        }
    }
}
