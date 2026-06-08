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

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public void StartChildGroup(string name) => StartChildContainer('{', false, name);

        public void StartChildArray(string name) => StartChildContainer('[', true, name);

        public string ReadEntry(string name)
        {
            if (InArray)
                return ReadArrayElementValue();

            int index = _json.IndexOf($"\"{name}\"", _pos);
            if (index == -1) return null;

            _pos = index + name.Length + 2;
            SkipWhitespace();
            Match(':');
            SkipWhitespace();

            return ReadValueAtPosition();
        }

        public void EndChildGroup() => EndChildContainer('}');

        public void EndChildArray() => EndChildContainer(']');

        private void StartChildContainer(char openBracket, bool isArray, string name)
        {
            if (!InArray && !string.IsNullOrEmpty(name))
            {
                int index = _json.IndexOf($"\"{name}\"", _pos);
                if (index != -1)
                {
                    _pos = index + name.Length + 2;
                    SkipWhitespace();
                    Match(':');
                }
            }
            else if (InArray)
            {
                SkipArrayComma();
            }

            while (_pos < _json.Length && _json[_pos] != openBracket)
                _pos++;

            if (Match(openBracket))
                _inArrayStack.Push(isArray);
        }

        private void EndChildContainer(char closeBracket)
        {
            while (_pos < _json.Length && _json[_pos] != closeBracket)
                _pos++;

            Match(closeBracket);
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        private string ReadArrayElementValue()
        {
            SkipArrayComma();
            return ReadValueAtPosition();
        }

        private void SkipArrayComma()
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == ',')
                _pos++;
            SkipWhitespace();
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
    }
}
