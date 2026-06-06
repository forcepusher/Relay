using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class JsonStateGraph
    {
        private readonly StringBuilder _sb = new();
        private int _depth = 0;
        private bool _hasStarted = false;
        private readonly Stack<bool> _firstItemScopes = new();

        public void StartChildGroup(string name)
        {
            if (!_hasStarted)
            {
                _sb.Append("{");
                _hasStarted = true;
                _depth++;
                _firstItemScopes.Push(true);
            }

            bool isFirst = _firstItemScopes.Pop();
            if (!isFirst)
            {
                _sb.Append(",");
            }
            _firstItemScopes.Push(false);

            if (!string.IsNullOrEmpty(name))
            {
                _sb.Append($"\"{name}\":");
            }

            _sb.Append("{");
            _depth++;
            _firstItemScopes.Push(true);
        }

        public void WriteEntry(string name, string state, bool wrapStateInQuotes)
        {
            if (!_hasStarted)
            {
                _sb.Append("{");
                _hasStarted = true;
                _depth++;
                _firstItemScopes.Push(true);
            }

            bool isFirst = _firstItemScopes.Pop();

            if (!isFirst)
                _sb.Append(",");

            _firstItemScopes.Push(false);

            if (wrapStateInQuotes)
                _sb.Append($"\"{name}\":\"{state}\"");
            else
                _sb.Append($"\"{name}\":{state}");
        }

        public void EndChildGroup()
        {
            if (_depth <= 1) return; // Cannot close implicit root here, handled in ToString

            _sb.Append("}");
            _depth--;
            _firstItemScopes.Pop();
        }

        public override string ToString()
        {
            if (!_hasStarted) return "{}";

            StringBuilder result = new(_sb.ToString());
            int currentDepth = _depth;
            while (currentDepth > 0)
            {
                result.Append("}");
                currentDepth--;
            }
            return result.ToString();
        }
    }
}
