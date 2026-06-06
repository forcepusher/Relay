using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public static class Json
    {
        /// <summary>
        /// Builds a graph of nodes by analyzing the provided JSON string.
        /// </summary>
        public static INode Parse(string json)
        {
            var parser = new SimpleJsonParser(json);
            return parser.ParseNode("");
        }

        private class SimpleJsonParser
        {
            private readonly string _json;
            private int _pos;

            public SimpleJsonParser(string json)
            {
                _json = json;
                _pos = 0;
            }

            public INode ParseNode(string name)
            {
                SkipWhitespace();
                if (Peek() == '{')
                {
                    return ParseObject(name);
                }
                else
                {
                    var value = ParseValue();
                    return CreateValueNode(name, value);
                }
            }

            private IObjectNode ParseObject(string name)
            {
                var objNode = new GenericObjectNode(name);
                Consume('{');
                SkipWhitespace();

                while (Peek() != '}')
                {
                    SkipWhitespace();
                    string key = ParseString();
                    SkipWhitespace();
                    Consume(':');
                    SkipWhitespace();

                    objNode.AddNode(ParseNode(key));
                    SkipWhitespace();
                    if (Peek() == ',')
                    {
                        Consume(',');
                        SkipWhitespace();
                    }
                }
                Consume('}');
                return objNode;
            }

            private object ParseValue()
            {
                SkipWhitespace();
                char c = Peek();
                if (c == '"') return ParseString();
                if (char.IsDigit(c) || c == '-') return ParseNumber();
                if (c == '{')
                {
                    // Special check for Vector3: if it's an object with x, y, z
                    int startPos = _pos;
                    var obj = ParseObject("temp");
                    var nodes = obj.GetNodes();
                    bool isVector3 = nodes.Count == 3;
                    float x = 0, y = 0, z = 0;
                    foreach (var node in nodes)
                    {
                        if (node is FloatValueNode fv)
                        {
                            if (node.Name == "x") x = fv.Value;
                            else if (node.Name == "y") y = fv.Value;
                            else if (node.Name == "z") z = fv.Value;
                        }
                    }

                    if (isVector3) return new Vector3(x, y, z);

                    // Otherwise it's just a generic object node
                    return obj;
                }
                throw new Exception($"Unexpected character {c} at position {_pos}");
            }

            private string ParseString()
            {
                Consume('"');
                var sb = new StringBuilder();
                while (Peek() != '"')
                {
                    char c = Consume();
                    if (c == '\\') // simple escape
                    {
                        _pos++;
                        sb.Append(_json[_pos]);
                    }
                    else sb.Append(c);
                }
                Consume('"');
                return sb.ToString();
            }

            private object ParseNumber()
            {
                var sb = new StringBuilder();
                while (_pos < _json.Length && (char.IsDigit(_json[_pos]) || _json[_pos] == '.' || _json[_pos] == '-'))
                {
                    sb.Append(Consume());
                }
                string s = sb.ToString();
                if (s.Contains('.')) return float.Parse(s, CultureInfo.InvariantCulture);
                return int.Parse(s, CultureInfo.InvariantCulture);
            }

            private INode CreateValueNode(string name, object value)
            {
                if (value is int i) return new IntegerValueNode(name, i);
                if (value is float f) return new FloatValueNode(name, f);
                if (value is Vector3 v) return new Vector3ValueNode(name, v);
                if (value is string s) return new StringValueNode(name, s);
                if (value is IObjectNode obj)
                {
                    // Since we can't easily rename the GenericObjectNode after creation in this simple parser,
                    // and our ParseObject already set the name, we just return it.
                    return obj;
                }
                throw new Exception($"Cannot create node for value type {value?.GetType()}");
            }

            private void SkipWhitespace()
            {
                while (_pos < _json.Length && char.IsWhiteSpace(_json[_pos])) _pos++;
            }

            private char Peek() => _pos < _json.Length ? _json[_pos] : '\0';

            private char Consume() => _json[_pos++];

            private void Consume(char expected)
            {
                if (Peek() != expected) throw new Exception($"Expected {expected} at {_pos}, found {Peek()}");
                _pos++;
            }
        }
    }
}
