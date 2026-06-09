using System.Collections.Generic;
using NUnit.Framework;

namespace BananaParty.WebSocketRelay.Tests
{
    public class JsonStateTests
    {
        private class MockState : IState
        {
            public string StateName { get; }
            public int Value { get; set; }

            public MockState(string name, int value)
            {
                StateName = name;
                Value = value;
            }

            public void WriteState(IStateOutput writeGraph)
            {
                writeGraph.WriteInt(StateName, Value);
            }

            public void ReadState(IStateInput readGraph)
            {
                Value = readGraph.ReadInt(StateName);
            }
        }

        [Test]
        public void ShouldWriteSimpleObjectState()
        {
            var states = new List<IState>
            {
                new MockState("Score", 10),
                new MockState("Level", 5)
            };
            var root = new ObjectState("GameState", states);
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Score\":10,\"Level\":5}}", json);
        }

        [Test]
        public void ShouldReadSimpleObjectState()
        {
            var states = new List<IState>
            {
                new MockState("Score", 0),
                new MockState("Level", 0)
            };
            var root = new ObjectState("GameState", states);
            var graph = new JsonStateInput("{\"GameState\":{\"Score\":10,\"Level\":5}}");

            root.ReadState(graph);

            Assert.AreEqual(10, ((MockState)states[0]).Value);
            Assert.AreEqual(5, ((MockState)states[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedObjectStates()
        {
            var playerStates = new List<IState>
            {
                new MockState("Health", 100),
                new MockState("Mana", 50)
            };
            var player = new ObjectState("Player", playerStates);

            var botStates = new List<IState>
            {
                new MockState("Health", 80),
                new MockState("Mana", 20)
            };
            var bot = new ObjectState("Bot", botStates);

            var rootStates = new List<IState> { player, bot };
            var root = new ObjectState("GameState", rootStates);
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}", json);
        }

        [Test]
        public void ShouldReadNestedObjectStates()
        {
            var playerStates = new List<IState>
            {
                new MockState("Health", 0),
                new MockState("Mana", 0)
            };
            var player = new ObjectState("Player", playerStates);

            var botStates = new List<IState>
            {
                new MockState("Health", 0),
                new MockState("Mana", 0)
            };
            var bot = new ObjectState("Bot", botStates);

            var rootStates = new List<IState> { player, bot };
            var root = new ObjectState("GameState", rootStates);
            var graph = new JsonStateInput("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}");

            root.ReadState(graph);

            Assert.AreEqual(100, ((MockState)playerStates[0]).Value);
            Assert.AreEqual(50, ((MockState)playerStates[1]).Value);
            Assert.AreEqual(80, ((MockState)botStates[0]).Value);
            Assert.AreEqual(20, ((MockState)botStates[1]).Value);
        }

        [Test]
        public void ShouldHandlePrettyPrint()
        {
            var states = new List<IState> { new MockState("X", 1) };
            var root = new ObjectState("Root", states);
            var graph = new JsonStateOutput(prettyPrint: true, bracesOnNewLine: true);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.IsTrue(json.Contains("\n"));
            Assert.IsTrue(json.Contains("    \"Root\":"));
        }

        [Test]
        public void ShouldWriteSimpleArrayState()
        {
            var scores = new List<MockState>
            {
                new MockState("", 10),
                new MockState("", 20),
                new MockState("", 30)
            };
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<MockState>("Scores", scores)
            });
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Scores\":[10,20,30]}}", json);
        }

        [Test]
        public void ShouldReadSimpleArrayState()
        {
            var scores = new List<MockState>
            {
                new MockState("", 0),
                new MockState("", 0),
                new MockState("", 0)
            };
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<MockState>("Scores", scores)
            });
            var graph = new JsonStateInput("{\"GameState\":{\"Scores\":[10,20,30]}}");

            root.ReadState(graph);

            Assert.AreEqual(10, scores[0].Value);
            Assert.AreEqual(20, scores[1].Value);
            Assert.AreEqual(30, scores[2].Value);
        }

        [Test]
        public void ShouldWriteArrayOfObjectStates()
        {
            var playerOne = new ObjectState("", new List<IState>
            {
                new MockState("Health", 100),
                new MockState("Mana", 50)
            });
            var playerTwo = new ObjectState("", new List<IState>
            {
                new MockState("Health", 80),
                new MockState("Mana", 20)
            });
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<ObjectState>("Players", new List<ObjectState> { playerOne, playerTwo })
            });
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Players\":[{\"Health\":100,\"Mana\":50},{\"Health\":80,\"Mana\":20}]}}", json);
        }

        [Test]
        public void ShouldReadArrayOfObjectStates()
        {
            var playerOneStates = new List<IState>
            {
                new MockState("Health", 0),
                new MockState("Mana", 0)
            };
            var playerTwoStates = new List<IState>
            {
                new MockState("Health", 0),
                new MockState("Mana", 0)
            };
            var playerOne = new ObjectState("", playerOneStates);
            var playerTwo = new ObjectState("", playerTwoStates);
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<ObjectState>("Players", new List<ObjectState> { playerOne, playerTwo })
            });
            var graph = new JsonStateInput("{\"GameState\":{\"Players\":[{\"Health\":100,\"Mana\":50},{\"Health\":80,\"Mana\":20}]}}");

            root.ReadState(graph);

            Assert.AreEqual(100, ((MockState)playerOneStates[0]).Value);
            Assert.AreEqual(50, ((MockState)playerOneStates[1]).Value);
            Assert.AreEqual(80, ((MockState)playerTwoStates[0]).Value);
            Assert.AreEqual(20, ((MockState)playerTwoStates[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedArrayStates()
        {
            var rowOne = new StaticArrayState<MockState>("", new List<MockState>
            {
                new MockState("", 1),
                new MockState("", 2)
            });
            var rowTwo = new StaticArrayState<MockState>("", new List<MockState>
            {
                new MockState("", 3),
                new MockState("", 4)
            });
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<StaticArrayState<MockState>>("Grid", new List<StaticArrayState<MockState>> { rowOne, rowTwo })
            });
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Grid\":[[1,2],[3,4]]}}", json);
        }

        [Test]
        public void ShouldReadNestedArrayStates()
        {
            var rowOneValues = new List<MockState>
            {
                new MockState("", 0),
                new MockState("", 0)
            };
            var rowTwoValues = new List<MockState>
            {
                new MockState("", 0),
                new MockState("", 0)
            };
            var rowOne = new StaticArrayState<MockState>("", rowOneValues);
            var rowTwo = new StaticArrayState<MockState>("", rowTwoValues);
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<StaticArrayState<MockState>>("Grid", new List<StaticArrayState<MockState>> { rowOne, rowTwo })
            });
            var graph = new JsonStateInput("{\"GameState\":{\"Grid\":[[1,2],[3,4]]}}");

            root.ReadState(graph);

            Assert.AreEqual(1, rowOneValues[0].Value);
            Assert.AreEqual(2, rowOneValues[1].Value);
            Assert.AreEqual(3, rowTwoValues[0].Value);
            Assert.AreEqual(4, rowTwoValues[1].Value);
        }
    }
}
