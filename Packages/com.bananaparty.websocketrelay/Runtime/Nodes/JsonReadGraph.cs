using System.Collections.Generic;
using System.Globalization;

namespace BananaParty.WebSocketRelay
{
    public class JsonReadGraph : IReadGraph
    {
        private readonly string _json;
        private int _pos;
        private readonly Stack<bool> _inArrayStack = new();

        public JsonReadGraph(string json)
        {
            _json = json ?? "{}";
        }

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public void StartObject(string name)
        {
            AdvanceIntoContainer('{', name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            ReadContainerClose('}');
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            AdvanceIntoContainer('[', name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            ReadContainerClose(']');
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public string ReadEntry(string name)
        {
            if (!TryAdvanceToEntry(name))
                return null;

            return ReadValueAsString();
        }

        public int ReadIntEntry(string name)
        {
            if (!TryAdvanceToEntry(name))
                return 0;

            return ReadIntAtPosition();
        }

        public float ReadFloatEntry(string name)
        {
            if (!TryAdvanceToEntry(name))
                return 0f;

            return ReadFloatAtPosition();
        }

        public bool ReadBoolEntry(string name)
        {
            if (!TryAdvanceToEntry(name))
                return false;

            return ReadBoolAtPosition();
        }

        public string ReadStringEntry(string name)
        {
            if (!TryAdvanceToEntry(name))
                return null;

            return ReadStringAtPosition();
        }

        public int ReadIntArrayEntry()
        {
            if (!TryAdvanceToEntry(null))
                return 0;

            return ReadIntAtPosition();
        }

        public float ReadFloatArrayEntry()
        {
            if (!TryAdvanceToEntry(null))
                return 0f;

            return ReadFloatAtPosition();
        }

        public bool ReadBoolArrayEntry()
        {
            if (!TryAdvanceToEntry(null))
                return false;

            return ReadBoolAtPosition();
        }

        public string ReadStringArrayEntry()
        {
            if (!TryAdvanceToEntry(null))
                return null;

            return ReadStringAtPosition();
        }

        private void AdvanceIntoContainer(char open, string name)
        {
            if (!InArray && !string.IsNullOrEmpty(name))
            {
                SkipWhitespace();
                if (_pos < _json.Length && _json[_pos] == open)
                    _pos++;

                SkipItemSeparator();
                ReadContainerName(name);
                SkipColon();
            }
            else if (InArray)
            {
                SkipItemSeparator();
            }

            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == open)
                _pos++;
        }

        private void ReadContainerClose(char close)
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == close)
                _pos++;
        }

        private void ReadContainerName(string expectedName)
        {
            ReadQuotedString();
        }

        private bool TryAdvanceToEntry(string expectedName)
        {
            if (InArray)
            {
                SkipItemSeparator();
                return true;
            }

            SkipItemSeparator();
            string entryName = ReadQuotedString();
            if (!string.IsNullOrEmpty(expectedName) && entryName != expectedName)
                return false;

            SkipColon();
            return true;
        }

        private void SkipItemSeparator()
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == ',')
                _pos++;
            SkipWhitespace();
        }

        private void SkipColon()
        {
            SkipWhitespace();
            if (_pos < _json.Length && _json[_pos] == ':')
                _pos++;
            SkipWhitespace();
        }

        private string ReadQuotedString()
        {
            if (_pos >= _json.Length || _json[_pos] != '"')
                return null;

            _pos++;
            int start = _pos;
            while (_pos < _json.Length && _json[_pos] != '"')
                _pos++;

            string value = _json.Substring(start, _pos - start);
            if (_pos < _json.Length)
                _pos++;

            return value;
        }

        private string ReadValueAsString()
        {
            SkipWhitespace();

            if (_pos < _json.Length && _json[_pos] == '"')
                return ReadQuotedString();

            int valueStart = _pos;
            while (_pos < _json.Length && _json[_pos] != ',' && _json[_pos] != '}' && _json[_pos] != ']' && !char.IsWhiteSpace(_json[_pos]))
                _pos++;

            return _json.Substring(valueStart, _pos - valueStart).Trim();
        }

        private int ReadIntAtPosition()
        {
            string value = ReadValueAsString();
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result) ? result : 0;
        }

        private float ReadFloatAtPosition()
        {
            string value = ReadValueAsString();
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : 0f;
        }

        private bool ReadBoolAtPosition()
        {
            string value = ReadValueAsString();
            if (!bool.TryParse(value, out bool result))
                return false;

            return result;
        }

        private string ReadStringAtPosition()
        {
            if (_pos < _json.Length && _json[_pos] == '"')
                return ReadQuotedString();

            return ReadValueAsString();
        }

        private void SkipWhitespace()
        {
            while (_pos < _json.Length && char.IsWhiteSpace(_json[_pos]))
                _pos++;
        }
    }
}
