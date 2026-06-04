using System;
using UnityEditor;

namespace BananaParty.WebSocketRelay
{
    public interface ISerializableState 
    {
        string GetKey()
        {
            return new Guid().ToString();
        }

        void Serialize(StateGraph stateStorage);
        void Deserialize(StateGraph stateStorage);
    }
}
