using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class StateStorageGraph
    {
        private readonly Stack<StateStorageGraph> _stack = new();

        private readonly Dictionary<string, object> _data = new();

        private StateStorageGraph Current => _stack.Count > 0 ? _stack.Peek() : this;

        public void Write(string key, object value);
        public int Read(string key) => Current._ints[key];

        public void StartChildGroup(string key)
        {
            var current = Current;
            if (!current._childStateStorages.TryGetValue(key, out var child))
            {
                child = new StateStorageGraph();
                current._childStateStorages[key] = child;
            }
            _stack.Push(child);
        }

        public void EndChildGroup()
        {
            if (_stack.Count > 0)
            {
                _stack.Pop();
            }
        }
    }
}
