using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, INode, IJsonState
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

            JsonWriteStateGraph _jsonStateGraph = new();
            WriteStateToJson(_jsonStateGraph);
            Debug.Log(_jsonStateGraph.ToString());
        }

        public string Name => transform.name;

        public void WriteStateToJson(JsonWriteStateGraph jsonStateGraph)
        {
            jsonStateGraph.StartObject(Name);

            _playTimeNode.WriteStateToJson(jsonStateGraph);
            _playerCharacter.WriteStateToJson(jsonStateGraph);
            _botCharacter.WriteStateToJson(jsonStateGraph);
            _itemSpawnsNode.WriteStateToJson(jsonStateGraph);

            jsonStateGraph.EndObject();
        }

        public void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph)
        {
            jsonReadStateGraph.StartObject(Name);

            _playTimeNode.ReadStateFromJson(jsonReadStateGraph);
            _playerCharacter.ReadStateFromJson(jsonReadStateGraph);
            _botCharacter.ReadStateFromJson(jsonReadStateGraph);
            _itemSpawnsNode.ReadStateFromJson(jsonReadStateGraph);

            jsonReadStateGraph.EndObject();
        }
    }
}
