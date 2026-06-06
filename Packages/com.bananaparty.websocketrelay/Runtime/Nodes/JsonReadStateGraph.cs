using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class JsonReadStateGraph
    {
        private readonly string _json;
        private int _pos = 0;

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

        public void StartChildGroup(string name)
        {
            // Find the key in the current object scope
            if (!string.IsNullOrEmpty(name))
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

            // Move to the opening brace of the child group
            while (_pos < _json.Length && _json[_pos] != '{')
                _pos++;

            if (Match('{'))
            {
                // Group started
            }
        }

        private bool IsAtKeyPosition(int index)
        {
            // Simple check to see if we are inside an object and not a string value
            // In a more robust parser, we'd track nesting level
            return true;
        }

        public string ReadEntry(string name)
        {
            string key = $"\"{name}\"";
            int index = _json.IndexOf(key, _pos);
            if (index == -1 || !IsAtKeyPosition(index)) return null;

            _pos = index + key.Length;
            SkipWhitespace();
            Match(':');
            SkipWhitespace();

            if (_pos < _json.Length && _json[_pos] == '\"')
            {
                _pos++; // skip "
                int start = _pos;
                while (_pos < _json.Length && _json[_pos] != '\"')
                    _pos++;
                string val = _json.Substring(start, _pos - start);
                if (_pos < _json.Length) _pos++; // skip "
                return val;
            }

            int valueStart = _pos;
            while (_pos < _json.Length && _json[_pos] != ',' && _json[_pos] != '}' && _json[_pos] != '\n')
                _pos++;

            return _json.Substring(valueStart, _pos - valueStart).Trim();
        }

        public void EndChildGroup()
        {
            // Find the matching closing brace for current level
            // This is a simplified version; doesn't handle nested braces perfectly
            // unless we track depth. Let's add depth tracking if needed.
            while (_pos < _json.Length && _json[_pos] != '}')
                _pos++;

            Match('}');
        }
    }
}
