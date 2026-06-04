using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StateGraph
    {
        private readonly Queue<StateEntry> _identitySateQueue = new();

        public void WriteState(string key, object stateObject)
        {
            _identitySateQueue.Enqueue(new StateEntry(key, stateObject));
        }

        public StateEntry ReadState()
        {
            return _identitySateQueue.Dequeue();
        }
    }
}
