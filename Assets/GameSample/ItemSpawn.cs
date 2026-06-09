using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState
    {
        private const float RespawnDelay = 10f;
        private FloatValueState _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);

        public string StateName => transform.name;

        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartObject(StateName);
            _timeToSpawn.WriteState(writeGraph);
            writeGraph.EndObject();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartObject(StateName);
            _timeToSpawn.ReadState(readGraph);
            readGraph.EndObject();
        }
    }
}
