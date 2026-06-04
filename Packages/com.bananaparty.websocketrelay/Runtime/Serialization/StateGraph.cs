using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StateGraph
    {
        private readonly Dictionary<string, object> _identitySateQueue = new();

        public void WriteState(string key, object stateObject)
        {
            _identitySateQueue[key] = stateObject;
        }

        public object ReadState(string key)
        {
            return _identitySateQueue[key];
        }

        public bool HasState(string key)
        {
            return _identitySateQueue.ContainsKey(key);
        }
    }
}
