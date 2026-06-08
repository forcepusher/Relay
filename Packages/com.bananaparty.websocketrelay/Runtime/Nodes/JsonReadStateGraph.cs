using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class JsonReadStateGraph
    {
        private readonly string _json;
        private int _pos = 0;
        private readonly Stack<bool> _inArrayStack = new();

        public JsonReadStateGraph(string json)
        {
            _json = json ?? "{}";
        }

        private void SkipWhitespace()
        {
            while (_pos < _json.Length && char.IsWhiteSpace(_json[_pos]))
                _pos++;
        }

        private bool Match(char c)
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == c)
            {
                _pos++;
                return true;
            }
            return false;
        }

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public void StartChildGroup(string name)
        {
            if (!InArray && !string.IsNullOrEmpty(name))
            {
                string key = $"\"{name}\"";
                int index = _json.IndexOf(key, _pos);
                if (index != -1 && IsAtKeyPosition(index))
                {
                    _pos = index + key.Length;
                    SkipWhitespace();
                    Match(':');
                }
            }
            else if (InArray)
            {
                SkipWhitespace();
                if (_pos < _json.Length && _json[_pos] == ',')
                    _pos++;
                SkipWhitespace();
            }

            while (_pos < _json.Length && _json[_pos] != '{')
                _pos++;

            if (Match('{'))
                _inArrayStack.Push(false);
        }

        public void StartChildArray(string name)
        {
            if (!InArray && !string.IsNullOrEmpty(name))
            {
                string key = $"\"{name}\"";
                int index = _json.IndexOf(key, _pos);
                if (index != -1 && IsAtKeyPosition(index))
                {
                    _pos = index + key.Length;
                    SkipWhitespace();
                    Match(':');
                }
            }
            else if (InArray)
            {
                SkipWhitespace();
                if (_pos < _json.Length && _json[_pos] == ',')
                    _pos++;
                SkipWhitespace();
            }

            while (_pos < _json.Length && _json[_pos] != '[')
                _pos++;

            if (Match('['))
                _inArrayStack.Push(true);
        }

        private bool IsAtKeyPosition(int index)
        {
            return true;
        }

        public string ReadEntry(string name)
        {
            if (InArray)
                return ReadArrayElementValue();

            string key = $"\"{name}\"";
            int index = _json.IndexOf(key, _pos);
            if (index == -1 || !IsAtKeyPosition(index)) return null;

            _pos = index + key.Length;
            SkipWhitespace();
            Match(':');
            SkipWhitespace();

            return ReadValueAtPosition();
        }

        private string ReadArrayElementValue()
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == ',')
                _pos++;
            SkipWhitespace();

            return ReadValueAtPosition();
        }

        private string ReadValueAtPosition()
        {
            if (_pos < _json.Length && _json[_pos] == '"')
            {
                _pos++;
                int start = _pos;
                while (_pos < _json.Length && _json[_pos] != '"')
                    _pos++;
                string val = _json.Substring(start, _pos - start);
                if (_pos < _json.Length) _pos++;
                return val;
            }

            int valueStart = _pos;
            while (_pos < _json.Length && _json[_pos] != ',' && _json[_pos] != '}' && _json[_pos] != ']' && _json[_pos] != '\n')
                _pos++;

            return _json.Substring(valueStart, _pos - valueStart).Trim();
        }

        public void EndChildGroup()
        {
            while (_pos < _json.Length && _json[_pos] != '}')
                _pos++;

            Match('}');
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void EndChildArray()
        {
            while (_pos < _json.Length && _json[_pos] != ']')
                _pos++;

            Match(']');
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }
    }
}
