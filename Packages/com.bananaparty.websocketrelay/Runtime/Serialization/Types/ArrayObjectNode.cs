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
            // use integers to specify length of busy objects instead of fixed array length

            foreach (T value in Values)
                value.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            // use integers to specify length of busy objects instead of fixed array length

            foreach (T value in Values)
                value.Deserialize(stateStream);
        }

        public string OutputNameAndValue()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"\"{Name}\": [");
            for (int i = 0; i < Values.Count; i++)
            {
                sb.Append(Values[i].OutputNameAndValue());
                if (i < Values.Count - 1) sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
