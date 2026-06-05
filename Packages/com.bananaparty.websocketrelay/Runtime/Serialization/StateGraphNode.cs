using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class StateGraphNode
    {
        private readonly List<IState> _states = new();
        private readonly List<StateGraphNode> _children = new();

        public void AddChildStateGraphNode(StateGraphNode stateGraphNode)
        {
            _children.Add(stateGraphNode);
        }

        public void AddState(IState state)
        {
            _states.Add(state);
        }

        public void Serialize(IStateStream stateStream)
        {
            foreach (IState state in _states)
                state.Serialize(stateStream);

            foreach (StateGraphNode stateGraphNode in _children)
                stateGraphNode.Serialize(stateStream);
        }

        public void Deserialize(IStateStream stateStream)
        {
            foreach (IState state in _states)
                state.Deserialize(stateStream);

            foreach (StateGraphNode stateGraphNode in _children)
                stateGraphNode.Deserialize(stateStream);
        }

        // // Pile of shit to optimize for
        // public string GetJson()
        // {
        //     var sb = new System.Text.StringBuilder();
        //     sb.Append("{");

        //     sb.Append("\"states\": [");
        //     for (int i = 0; i < _states.Count; i++)
        //     {
        //         var state = _states[i];
        //         object nameObj = GetValueByReflection(state, "Name");
        //         object valueObj = GetValueByReflection(state, "Value");

        //         string name = nameObj?.ToString() ?? "Unknown";

        //         sb.Append($"{{\"name\": \"{EscapeJsonString(name)}\", \"value\": {FormatJsonValue(valueObj)}}}");
        //         if (i < _states.Count - 1) sb.Append(", ");
        //     }
        //     sb.Append("], ");

        //     sb.Append("\"children\": [");
        //     for (int i = 0; i < _children.Count; i++)
        //     {
        //         sb.Append(_children[i].GetJson());
        //         if (i < _children.Count - 1) sb.Append(", ");
        //     }
        //     sb.Append("]");

        //     sb.Append("}");
        //     return sb.ToString();
        // }

        // private object GetValueByReflection(object obj, string memberName)
        // {
        //     var type = obj.GetType();
        //     var prop = type.GetProperty(memberName);
        //     if (prop != null) return prop.GetValue(obj);
        //     var field = type.GetField(memberName);
        //     if (field != null) return field.GetValue(obj);
        //     return null;
        // }

        // private string FormatJsonValue(object value)
        // {
        //     if (value == null) return "null";
        //     if (value is bool b) return b ? "true" : "false";
        //     if (value is int || value is float || value is double || value is long || value is byte) return value.ToString();

        //     return $"\"{EscapeJsonString(value.ToString() ?? "")}\"";
        // }

        // private string EscapeJsonString(string s)
        // {
        //     return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        // }
    }
}
