using System.Collections.Generic;
using NUnit.Framework;

namespace BananaParty.WebSocketRelay.Tests
{
    public class JsonStateTests
    {
        private class MockValueNode : INode, IValueNode<int>
        {
            public string Name { get; }
            public int Value { get; set; }

            public MockValueNode(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public void WriteStateToJson(JsonWriteStateGraph jsonStateGraph)
            {
                jsonStateGraph.WriteEntry(Name, Value.ToString(), false);
            }

            public void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph)
            {
                string val = jsonReadStateGraph.ReadEntry(Name);
                if (val != null && int.TryParse(val, out int result))
                {
                    Value = result;
                }
            }
        }

        [Test]
        public void ShouldWriteSimpleObjectNode()
        {
            var nodes = new List<INode>
            {
                new MockValueNode("Score", 10),
                new MockValueNode("Level", 5)
            };
            var root = new ObjectNode("GameState", nodes);
            var graph = new JsonWriteStateGraph(prettyPrint: false, bracesOnNewLine: false);

            root.WriteStateToJson(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Score\":10,\"Level\":5}}", json);
        }

        [Test]
        public void ShouldReadSimpleObjectNode()
        {
            var nodes = new List<INode>
            {
                new MockValueNode("Score", 0),
                new MockValueNode("Level", 0)
            };
            var root = new ObjectNode("GameState", nodes);
            var graph = new JsonReadStateGraph("{\"GameState\":{\"Score\":10,\"Level\":5}}");

            root.ReadStateFromJson(graph);

            Assert.AreEqual(10, ((MockValueNode)nodes[0]).Value);
            Assert.AreEqual(5, ((MockValueNode)nodes[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedObjectNodes()
        {
            var playerNodes = new List<INode>
            {
                new MockValueNode("Health", 100),
                new MockValueNode("Mana", 50)
            };
            var player = new ObjectNode("Player", playerNodes);

            var botNodes = new List<INode>
            {
                new MockValueNode("Health", 80),
                new MockValueNode("Mana", 20)
            };
            var bot = new ObjectNode("Bot", botNodes);

            var rootNodes = new List<INode> { player, bot };
            var root = new ObjectNode("GameState", rootNodes);
            var graph = new JsonWriteStateGraph(prettyPrint: false, bracesOnNewLine: false);

            root.WriteStateToJson(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}", json);
        }

        [Test]
        public void ShouldReadNestedObjectNodes()
        {
            var playerNodes = new List<INode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var player = new ObjectNode("Player", playerNodes);

            var botNodes = new List<INode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var bot = new ObjectNode("Bot", botNodes);

            var rootNodes = new List<INode> { player, bot };
            var root = new ObjectNode("GameState", rootNodes);
            var graph = new JsonReadStateGraph("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}");

            root.ReadStateFromJson(graph);

            Assert.AreEqual(100, ((MockValueNode)playerNodes[0]).Value);
            Assert.AreEqual(50, ((MockValueNode)playerNodes[1]).Value);
            Assert.AreEqual(80, ((MockValueNode)botNodes[0]).Value);
            Assert.AreEqual(20, ((MockValueNode)botNodes[1]).Value);
        }

        [Test]
        public void ShouldHandlePrettyPrint()
        {
            var nodes = new List<INode> { new MockValueNode("X", 1) };
            var root = new ObjectNode("Root", nodes);
            var graph = new JsonWriteStateGraph(prettyPrint: true, bracesOnNewLine: true);

            root.WriteStateToJson(graph);
            string json = graph.ToString();

            Assert.IsTrue(json.Contains("\n"));
            Assert.IsTrue(json.Contains("    \"Root\":"));
        }
    }
}
