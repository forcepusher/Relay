using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class JsonStateGraph
    {
        private readonly bool _prettyPrint;
        private readonly StringBuilder _sb = new();
        private int _depth = 0;
        private bool _hasStarted = false;
        private readonly Stack<bool> _firstItemScopes = new();

        public JsonStateGraph(bool prettyPrint = true)
        {
            _prettyPrint = prettyPrint;
        }

        private void AppendIndent()
        {
            _sb.Append(new string(' ', _depth * 2));
        }

        public void StartChildGroup(string name)
        {
            if (!_hasStarted)
            {
                _sb.Append("{");
                _hasStarted = true;
                _depth++;
                _firstItemScopes.Push(true);

                if (_prettyPrint)
                {
                    _sb.Append("\n");
                    AppendIndent();
                }
            }

            bool isFirst = _firstItemScopes.Pop();
            if (!isFirst)
            {
                if (_prettyPrint)
                {
                    _sb.Append(",\n");
                    AppendIndent();
                }
                else
                {
                    _sb.Append(",");
                }
            }
            _firstItemScopes.Push(false);

            if (!string.IsNullOrEmpty(name))
            {
                _sb.Append($"\"{name}\":");
            }

            _sb.Append("{");
            _depth++;
            _firstItemScopes.Push(true);

            if (_prettyPrint)
            {
                _sb.Append("\n");
                AppendIndent();
            }
        }

        public void WriteEntry(string name, string state, bool wrapStateInQuotes)
        {
            if (!_hasStarted)
            {
                _sb.Append("{");
                _hasStarted = true;
                _depth++;
                _firstItemScopes.Push(true);

                if (_prettyPrint)
                {
                    _sb.Append("\n");
                    AppendIndent();
                }
            }

            bool isFirst = _firstItemScopes.Pop();

            if (!isFirst)
            {
                if (_prettyPrint)
                {
                    _sb.Append(",\n");
                    AppendIndent();
                }
                else
                {
                    _sb.Append(",");
                }
            }

            _firstItemScopes.Push(false);

            if (wrapStateInQuotes)
                _sb.Append($"\"{name}\":\"{state}\"");
            else
                _sb.Append($"\"{name}\":{state}");
        }

        public void EndChildGroup()
        {
            if (_depth <= 1) return; // Cannot close implicit root here, handled in ToString

            if (_prettyPrint)
            {
                _sb.Append("\n");
                _depth--;
                AppendIndent();
            }
            else
            {
                _depth--;
            }

            _sb.Append("}");
            _firstItemScopes.Pop();
        }

        public override string ToString()
        {
            if (!_hasStarted) return "{}";

            StringBuilder result = new(_sb.ToString());
            int currentDepth = _depth;
            while (currentDepth > 0)
            {
                if (_prettyPrint)
                {
                    result.Append("\n");
                    currentDepth--;
                    if (currentDepth > 0)
                    {
                        result.Append(new string(' ', currentDepth * 2));
                    }
                }
                else
                {
                    currentDepth--;
                }
                result.Append("}");
            }
            return result.ToString();
        }
    }
}
