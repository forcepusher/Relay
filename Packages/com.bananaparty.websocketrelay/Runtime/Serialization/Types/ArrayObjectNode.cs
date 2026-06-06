using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class ArrayObjectNode<T> : IObjectNode where T : IObjectNode
    {
        public readonly List<T> Values;
        public readonly string Name;

        public ArrayObjectNode(string name, List<T> initialValues)
        {
            Values = initialValues;
            Name = name;
        }

        // public void Serialize(IStateNode stateStream)
        // {
        //     // use integers to specify length of busy objects instead of fixed array length

        //     foreach (T value in Values)
        //         value.Serialize(stateStream);
        // }

        // public void Deserialize(IStateNode stateStream)
        // {
        //     // use integers to specify length of busy objects instead of fixed array length

        //     foreach (T value in Values)
        //         value.Deserialize(stateStream);
        // }

        // public string OutputNameAndValue()
        // {
        //     var sb = new System.Text.StringBuilder();
        //     sb.Append("[");
        //     for (int i = 0; i < Values.Count; i++)
        //     {
        //         sb.Append(Values[i].OutputNameAndValue());
        //         if (i < Values.Count - 1) sb.Append(", ");
        //     }
        //     sb.Append("]");
        //     return Json.ConvertToText(Name, (object)sb.ToString());
        // }
    }
}
