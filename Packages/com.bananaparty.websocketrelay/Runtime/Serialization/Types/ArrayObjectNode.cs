using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayObjectNode<T> : IObjectNode where T : IObjectNode
    {
        public readonly List<T> Values;
        public readonly string Name;

        public ArrayObjectNode(List<T> initialValues, string name = nameof(ArrayObjectNode<T>))
        {
            Values = initialValues;
            Name = name;
        }

        public void Serialize(IStateStream stateStream)
        {
            stateStream.WriteInt(Values.Count);

            foreach (T value in Values)
                stateStream.WriteObject(value);
        }

        public void Deserialize(IStateStream stateStream)
        {
            stateStream.ReadInt();
        }

        public string OutputNameAndValue()
        {
            return $"\"{Name}\": {Value.ToString().ToLower()}";
        }
    }
}
