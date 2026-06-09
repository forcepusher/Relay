using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Item : MonoBehaviour, IState
    {
        private FloatValueState _timeToDisappear = new(nameof(_timeToDisappear), 5f);

        public string Name => transform.name;

        public float TimeToDisappear
        {
            get => _timeToDisappear.Value;
            set => _timeToDisappear.Value = value;
        }
        
        public void Write(IStateOutput writeGraph)
        {
            writeGraph.StartObject(Name);
            _timeToDisappear.Write(writeGraph);
            writeGraph.EndObject();
        }

        public void Read(IStateInput readGraph)
        {
            readGraph.StartObject(Name);
            _timeToDisappear.Read(readGraph);
            readGraph.EndObject();
        }
    }
}
