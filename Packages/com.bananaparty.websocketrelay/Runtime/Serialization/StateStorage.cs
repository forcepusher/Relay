using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class StateStorage
    {
        private readonly Dictionary<string, StateStorage> _childStateStorages = new();

        private readonly Dictionary<string, int> _ints = new();
        private readonly Dictionary<string, float> _floats = new();

        public void WriteInt(string key, int value) => _ints[key] = value;
        public int ReadInt(string key) => _ints[key];
        public bool HasInt(string key) => _ints.ContainsKey(key);

        public void WriteFloat(string key, float value) => _floats[key] = value;
        public float ReadFloat(string key) => _floats[key];
        public bool HasFloat(string key) => _floats.ContainsKey(key);

        public void StartChildGroup(string key, int childCount)
        {
            _childStateStorages
        }

        public void EndChildGroup()
        {

        }
    }
}
