using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StateGraph
    {
        private readonly Stack<object> _stateStack = new();

        //public void StartChildGroup(int childCount)
        //{
        //    for (int childIndex = 0; childIndex < childCount; childIndex++) {
        //        _stateStack.Push(new State<object>());
        //    }
        //}

        public void PushState(object stateObject)
        {
            _stateStack.Push(stateObject);
        }

        public object PopState()
        {
            return _stateStack.Pop();
        }

        // private readonly Dictionary<string, object> _data = new();

        // private StateGraph Current => _stack.Count > 0 ? _stack.Peek() : this;

        // public void Write(string key, object value);
        // public int Read(string key) => Current._ints[key];

        // public void StartChildGroup(string key)
        // {
        //     var current = Current;
        //     if (!current._childStateStorages.TryGetValue(key, out var child))
        //     {
        //         child = new StateGraph();
        //         current._childStateStorages[key] = child;
        //     }
        //     _stack.Push(child);
        // }

        // public void EndChildGroup()
        // {
        //     if (_stack.Count > 0)
        //     {
        //         _stack.Pop();
        //     }
        // }
    }
}
