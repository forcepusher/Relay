using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class ItemSpawn : MonoBehaviour, IState
    {
        private const float RespawnDelay = 10f;
        private FloatValueState _timeToSpawn = new(nameof(_timeToSpawn), RespawnDelay);

        public string Name => transform.name;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);
            _timeToSpawn.Write(writeGraph);
            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);
            _timeToSpawn.Read(readGraph);
            readGraph.EndObject();
        }
    }
}
