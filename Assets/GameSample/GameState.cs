using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Samples
{
    public class GameState : MonoBehaviour, IStateNode
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

            JsonWriteGraph jsonWriteGraph = new();
            Write(jsonWriteGraph);
            Debug.Log(jsonWriteGraph.ToString());
        }

        public string Name => transform.name;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartObject(Name);

            _playTimeNode.Write(writeGraph);
            _playerCharacter.Write(writeGraph);
            _botCharacter.Write(writeGraph);
            _itemSpawnsNode.Write(writeGraph);

            writeGraph.EndObject();
        }

        public void Read(IReadGraph readGraph)
        {
            readGraph.StartObject(Name);

            _playTimeNode.Read(readGraph);
            _playerCharacter.Read(readGraph);
            _botCharacter.Read(readGraph);
            _itemSpawnsNode.Read(readGraph);

            readGraph.EndObject();
        }
    }
}
