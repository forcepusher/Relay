using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IJsonState
    {
        [SerializeField]
        private Character _playerCharacter;

        [SerializeField]
        private Character _botCharacter;

        [SerializeField]
        private List<ItemSpawn> _itemSpawns;

        private IntegerValueNode _playTimeNode = new(nameof(_playTimeNode), 0);

        private ArrayNode<ItemSpawn> _itemSpawnsNode;

        private void Awake()
        {
            _itemSpawnsNode = new(nameof(_itemSpawns), _itemSpawns);

            JsonWriteGraph _jsonStateGraph = new();
            WriteToJson(_jsonStateGraph);
            Debug.Log(_jsonStateGraph.ToString());
        }

        public string Name => transform.name;

        public void WriteToJson(JsonWriteGraph jsonWriteGraph)
        {
            jsonWriteGraph.StartObject(Name);

            _playTimeNode.WriteStateToJson(jsonWriteGraph);
            _playerCharacter.WriteToJson(jsonWriteGraph);
            _botCharacter.WriteToJson(jsonWriteGraph);
            _itemSpawnsNode.WriteToJson(jsonWriteGraph);

            jsonWriteGraph.EndObject();
        }

        public void ReadFromJson(JsonReadGraph jsonReadGraph)
        {
            jsonReadGraph.StartObject(Name);

            _playTimeNode.ReadStateFromJson(jsonReadGraph);
            _playerCharacter.ReadFromJson(jsonReadGraph);
            _botCharacter.ReadFromJson(jsonReadGraph);
            _itemSpawnsNode.ReadFromJson(jsonReadGraph);

            jsonReadGraph.EndObject();
        }
    }
}
