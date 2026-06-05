using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayOfObjectsNode : IObjectNode
    {
        public readonly List<ObjectNode> Values;
        public readonly string Name;

        public ArrayOfObjectsNode(List<ObjectNode> initialValues, string name = nameof(ArrayOfObjectsNode))
        {
            Values = initialValues;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            // use integers to specify length of busy objects instead of fixed array length

            foreach (ObjectNode value in Values)
                value.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            // use integers to specify length of busy objects instead of fixed array length

            foreach (ObjectNode value in Values)
                value.Deserialize(stateStream);
        }

        public string OutputNameAndValue()
        {
            //return $"\"{Name}\": {Value.ToString().ToLower()}";
        }
    }
}
