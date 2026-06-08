using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Item : MonoBehaviour, IStateNode
    {
        private FloatValueNode _timeToDisappear = new(nameof(_timeToDisappear), 5f);

        public string Name => transform.name;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);
            _timeToDisappear.Write(writeGraph);
            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);
            _timeToDisappear.Read(readGraph);
            readGraph.EndObject();
        }
    }
}
