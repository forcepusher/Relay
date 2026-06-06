using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class Color32ObjectNode : IObjectNode
    {
        private Color32 Value;
        private readonly string Name;

        public Color32ObjectNode(string name, Color32 initialValue)
        {
            Value = initialValue;
            Name = name;
        }
    }
}
