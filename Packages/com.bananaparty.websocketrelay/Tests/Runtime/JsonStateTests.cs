using System.Collections.Generic;
using NUnit.Framework;

namespace BananaParty.WebSocketRelay.Tests
{
    public class JsonStateTests
    {
        private class MockValueNode : IStateNode
        {
            public string Name { get; }
            public int Value { get; set; }

            public MockValueNode(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public void Write(IWriteGraph writeGraph)
            {
                writeGraph.WriteEntry(Name, Value);
            }

            public void Read(IReadGraph readGraph)
            {
                Value = readGraph.ReadIntEntry(Name);
            }
        }

        [Test]
        public void ShouldWriteSimpleObjectNode()
        {
            var nodes = new List<IStateNode>
            {
                new MockValueNode("Score", 10),
                new MockValueNode("Level", 5)
            };
            var root = new ObjectNode("GameState", nodes);
            var graph = new JsonWriteGraph(prettyPrint: false, bracesOnNewLine: false);

            root.Write(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Score\":10,\"Level\":5}}", json);
        }

        [Test]
        public void ShouldReadSimpleObjectNode()
        {
            var nodes = new List<IStateNode>
            {
                new MockValueNode("Score", 0),
                new MockValueNode("Level", 0)
            };
            var root = new ObjectNode("GameState", nodes);
            var graph = new JsonReadGraph("{\"GameState\":{\"Score\":10,\"Level\":5}}");

            root.Read(graph);

            Assert.AreEqual(10, ((MockValueNode)nodes[0]).Value);
            Assert.AreEqual(5, ((MockValueNode)nodes[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedObjectNodes()
        {
            var playerNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 100),
                new MockValueNode("Mana", 50)
            };
            var player = new ObjectNode("Player", playerNodes);

            var botNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 80),
                new MockValueNode("Mana", 20)
            };
            var bot = new ObjectNode("Bot", botNodes);

            var rootNodes = new List<IStateNode> { player, bot };
            var root = new ObjectNode("GameState", rootNodes);
            var graph = new JsonWriteGraph(prettyPrint: false, bracesOnNewLine: false);

            root.Write(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}", json);
        }

        [Test]
        public void ShouldReadNestedObjectNodes()
        {
            var playerNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var player = new ObjectNode("Player", playerNodes);

            var botNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var bot = new ObjectNode("Bot", botNodes);

            var rootNodes = new List<IStateNode> { player, bot };
            var root = new ObjectNode("GameState", rootNodes);
            var graph = new JsonReadGraph("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}");

            root.Read(graph);

            Assert.AreEqual(100, ((MockValueNode)playerNodes[0]).Value);
            Assert.AreEqual(50, ((MockValueNode)playerNodes[1]).Value);
            Assert.AreEqual(80, ((MockValueNode)botNodes[0]).Value);
            Assert.AreEqual(20, ((MockValueNode)botNodes[1]).Value);
        }

        [Test]
        public void ShouldHandlePrettyPrint()
        {
            var nodes = new List<IStateNode> { new MockValueNode("X", 1) };
            var root = new ObjectNode("Root", nodes);
            var graph = new JsonWriteGraph(prettyPrint: true, bracesOnNewLine: true);

            root.Write(graph);
            string json = graph.ToString();

            Assert.IsTrue(json.Contains("\n"));
            Assert.IsTrue(json.Contains("    \"Root\":"));
        }

        [Test]
        public void ShouldWriteSimpleArrayNode()
        {
            var scores = new List<MockValueNode>
            {
                new MockValueNode("", 10),
                new MockValueNode("", 20),
                new MockValueNode("", 30)
            };
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<MockValueNode>("Scores", scores)
            });
            var graph = new JsonWriteGraph(prettyPrint: false, bracesOnNewLine: false);

            root.Write(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Scores\":[10,20,30]}}", json);
        }

        [Test]
        public void ShouldReadSimpleArrayNode()
        {
            var scores = new List<MockValueNode>
            {
                new MockValueNode("", 0),
                new MockValueNode("", 0),
                new MockValueNode("", 0)
            };
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<MockValueNode>("Scores", scores)
            });
            var graph = new JsonReadGraph("{\"GameState\":{\"Scores\":[10,20,30]}}");

            root.Read(graph);

            Assert.AreEqual(10, scores[0].Value);
            Assert.AreEqual(20, scores[1].Value);
            Assert.AreEqual(30, scores[2].Value);
        }

        [Test]
        public void ShouldWriteArrayOfObjectNodes()
        {
            var playerOne = new ObjectNode("", new List<IStateNode>
            {
                new MockValueNode("Health", 100),
                new MockValueNode("Mana", 50)
            });
            var playerTwo = new ObjectNode("", new List<IStateNode>
            {
                new MockValueNode("Health", 80),
                new MockValueNode("Mana", 20)
            });
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<ObjectNode>("Players", new List<ObjectNode> { playerOne, playerTwo })
            });
            var graph = new JsonWriteGraph(prettyPrint: false, bracesOnNewLine: false);

            root.Write(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Players\":[{\"Health\":100,\"Mana\":50},{\"Health\":80,\"Mana\":20}]}}", json);
        }

        [Test]
        public void ShouldReadArrayOfObjectNodes()
        {
            var playerOneNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var playerTwoNodes = new List<IStateNode>
            {
                new MockValueNode("Health", 0),
                new MockValueNode("Mana", 0)
            };
            var playerOne = new ObjectNode("", playerOneNodes);
            var playerTwo = new ObjectNode("", playerTwoNodes);
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<ObjectNode>("Players", new List<ObjectNode> { playerOne, playerTwo })
            });
            var graph = new JsonReadGraph("{\"GameState\":{\"Players\":[{\"Health\":100,\"Mana\":50},{\"Health\":80,\"Mana\":20}]}}");

            root.Read(graph);

            Assert.AreEqual(100, ((MockValueNode)playerOneNodes[0]).Value);
            Assert.AreEqual(50, ((MockValueNode)playerOneNodes[1]).Value);
            Assert.AreEqual(80, ((MockValueNode)playerTwoNodes[0]).Value);
            Assert.AreEqual(20, ((MockValueNode)playerTwoNodes[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedArrayNodes()
        {
            var rowOne = new StaticArrayNode<MockValueNode>("", new List<MockValueNode>
            {
                new MockValueNode("", 1),
                new MockValueNode("", 2)
            });
            var rowTwo = new StaticArrayNode<MockValueNode>("", new List<MockValueNode>
            {
                new MockValueNode("", 3),
                new MockValueNode("", 4)
            });
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<StaticArrayNode<MockValueNode>>("Grid", new List<StaticArrayNode<MockValueNode>> { rowOne, rowTwo })
            });
            var graph = new JsonWriteGraph(prettyPrint: false, bracesOnNewLine: false);

            root.Write(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Grid\":[[1,2],[3,4]]}}", json);
        }

        [Test]
        public void ShouldReadNestedArrayNodes()
        {
            var rowOneValues = new List<MockValueNode>
            {
                new MockValueNode("", 0),
                new MockValueNode("", 0)
            };
            var rowTwoValues = new List<MockValueNode>
            {
                new MockValueNode("", 0),
                new MockValueNode("", 0)
            };
            var rowOne = new StaticArrayNode<MockValueNode>("", rowOneValues);
            var rowTwo = new StaticArrayNode<MockValueNode>("", rowTwoValues);
            var root = new ObjectNode("GameState", new List<IStateNode>
            {
                new StaticArrayNode<StaticArrayNode<MockValueNode>>("Grid", new List<StaticArrayNode<MockValueNode>> { rowOne, rowTwo })
            });
            var graph = new JsonReadGraph("{\"GameState\":{\"Grid\":[[1,2],[3,4]]}}");

            root.Read(graph);

            Assert.AreEqual(1, rowOneValues[0].Value);
            Assert.AreEqual(2, rowOneValues[1].Value);
            Assert.AreEqual(3, rowTwoValues[0].Value);
            Assert.AreEqual(4, rowTwoValues[1].Value);
        }
    }
}
