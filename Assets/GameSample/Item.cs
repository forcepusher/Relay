using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class Item : MonoBehaviour, IState
    {
        private FloatValueState _timeToDisappear = new(nameof(_timeToDisappear), 5f);

        public string StateName => transform.name;

        public float TimeToDisappear
        {
            get => _timeToDisappear.Value;
            set => _timeToDisappear.Value = value;
        }
        
        public void WriteState(IStateOutput writeGraph)
        {
            writeGraph.StartObject(StateName);
            _timeToDisappear.WriteState(writeGraph);
            writeGraph.EndObject();
        }

        public void ReadState(IStateInput readGraph)
        {
            readGraph.StartObject(StateName);
            _timeToDisappear.ReadState(readGraph);
            readGraph.EndObject();
        }
    }
}
