using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class JsonWriteStateGraph
    {
        private readonly bool _prettyPrint;
        private readonly bool _bracesOnNewLine;
        private readonly int _indentationCount;
        private readonly StringBuilder _sb = new();
        private int _depth = 0;
        private bool _hasStarted = false;
        private readonly Stack<bool> _firstItemScopes = new();

        public JsonWriteStateGraph(bool prettyPrint = true, bool bracesOnNewLine = true, int spaceIndentationCount = 4)
        {
            _prettyPrint = prettyPrint;
            _bracesOnNewLine = bracesOnNewLine;
            _indentationCount = spaceIndentationCount;
        }

        private void AppendIndent()
        {
            _sb.Append(new string(' ', _depth * _indentationCount));
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

            if (_prettyPrint && _bracesOnNewLine)
            {
                _sb.Append("\n");
                AppendIndent();
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
                        result.Append(new string(' ', currentDepth * _indentationCount));
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
