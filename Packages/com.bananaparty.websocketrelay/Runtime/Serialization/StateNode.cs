using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class StateGraphNode
    {
        private readonly List<StateGraphNode> _children = new();
        private readonly List<IState> _states = new();

        public void AddChildStateGraphNode(StateGraphNode stateGraphNode)
        {
            _children.Add(stateGraphNode);
        }

        public void AddState(IState state)
        {
            _states.Add(state);
        }
    }

    // public class StateGraph
    // {
    //     bool isWritingArray = false;

    //     private readonly Dictionary<string, object> _identitySateQueue = new();

    //     public void WriteObject(string key, object stateObject)
    //     {
    //         _identitySateQueue[key] = stateObject;
    //     }

    //     public void StartArray(string key)
    //     {
    //         isWritingArray = true;
    //     }

    //     public void StopArray()
    //     {
    //         isWritingArray = false;
    //     }

    //     public object ReadState(string key)
    //     {
    //         return _identitySateQueue[key];
    //     }

    //     public bool HasState(string key)
    //     {
    //         return _identitySateQueue.ContainsKey(key);
    //     }
    // }
}
