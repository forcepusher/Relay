using System.Collections.Generic;
using NUnit.Framework;

namespace BananaParty.WebSocketRelay.Tests
{
    public class JsonStateTests
    {
        private class MockValueState : IState
        {
            public string StateName { get; }
            public int Value { get; set; }

            public MockValueState(string name, int value)
            {
                StateName = name;
                Value = value;
            }

            public void WriteState(IStateOutput writeGraph)
            {
                writeGraph.WriteEntry(StateName, Value);
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
                new MockValueState("Score", 10),
                new MockValueState("Level", 5)
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
                new MockValueState("Score", 0),
                new MockValueState("Level", 0)
            };
            var root = new ObjectState("GameState", states);
            var graph = new JsonStateInput("{\"GameState\":{\"Score\":10,\"Level\":5}}");

            root.ReadState(graph);

            Assert.AreEqual(10, ((MockValueState)states[0]).Value);
            Assert.AreEqual(5, ((MockValueState)states[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedObjectStates()
        {
            var playerStates = new List<IState>
            {
                new MockValueState("Health", 100),
                new MockValueState("Mana", 50)
            };
            var player = new ObjectState("Player", playerStates);

            var botStates = new List<IState>
            {
                new MockValueState("Health", 80),
                new MockValueState("Mana", 20)
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
                new MockValueState("Health", 0),
                new MockValueState("Mana", 0)
            };
            var player = new ObjectState("Player", playerStates);

            var botStates = new List<IState>
            {
                new MockValueState("Health", 0),
                new MockValueState("Mana", 0)
            };
            var bot = new ObjectState("Bot", botStates);

            var rootStates = new List<IState> { player, bot };
            var root = new ObjectState("GameState", rootStates);
            var graph = new JsonStateInput("{\"GameState\":{\"Player\":{\"Health\":100,\"Mana\":50},\"Bot\":{\"Health\":80,\"Mana\":20}}}");

            root.ReadState(graph);

            Assert.AreEqual(100, ((MockValueState)playerStates[0]).Value);
            Assert.AreEqual(50, ((MockValueState)playerStates[1]).Value);
            Assert.AreEqual(80, ((MockValueState)botStates[0]).Value);
            Assert.AreEqual(20, ((MockValueState)botStates[1]).Value);
        }

        [Test]
        public void ShouldHandlePrettyPrint()
        {
            var states = new List<IState> { new MockValueState("X", 1) };
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
            var scores = new List<MockValueState>
            {
                new MockValueState("", 10),
                new MockValueState("", 20),
                new MockValueState("", 30)
            };
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<MockValueState>("Scores", scores)
            });
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Scores\":[10,20,30]}}", json);
        }

        [Test]
        public void ShouldReadSimpleArrayState()
        {
            var scores = new List<MockValueState>
            {
                new MockValueState("", 0),
                new MockValueState("", 0),
                new MockValueState("", 0)
            };
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<MockValueState>("Scores", scores)
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
                new MockValueState("Health", 100),
                new MockValueState("Mana", 50)
            });
            var playerTwo = new ObjectState("", new List<IState>
            {
                new MockValueState("Health", 80),
                new MockValueState("Mana", 20)
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
                new MockValueState("Health", 0),
                new MockValueState("Mana", 0)
            };
            var playerTwoStates = new List<IState>
            {
                new MockValueState("Health", 0),
                new MockValueState("Mana", 0)
            };
            var playerOne = new ObjectState("", playerOneStates);
            var playerTwo = new ObjectState("", playerTwoStates);
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<ObjectState>("Players", new List<ObjectState> { playerOne, playerTwo })
            });
            var graph = new JsonStateInput("{\"GameState\":{\"Players\":[{\"Health\":100,\"Mana\":50},{\"Health\":80,\"Mana\":20}]}}");

            root.ReadState(graph);

            Assert.AreEqual(100, ((MockValueState)playerOneStates[0]).Value);
            Assert.AreEqual(50, ((MockValueState)playerOneStates[1]).Value);
            Assert.AreEqual(80, ((MockValueState)playerTwoStates[0]).Value);
            Assert.AreEqual(20, ((MockValueState)playerTwoStates[1]).Value);
        }

        [Test]
        public void ShouldWriteNestedArrayStates()
        {
            var rowOne = new StaticArrayState<MockValueState>("", new List<MockValueState>
            {
                new MockValueState("", 1),
                new MockValueState("", 2)
            });
            var rowTwo = new StaticArrayState<MockValueState>("", new List<MockValueState>
            {
                new MockValueState("", 3),
                new MockValueState("", 4)
            });
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<StaticArrayState<MockValueState>>("Grid", new List<StaticArrayState<MockValueState>> { rowOne, rowTwo })
            });
            var graph = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            root.WriteState(graph);
            string json = graph.ToString();

            Assert.AreEqual("{\"GameState\":{\"Grid\":[[1,2],[3,4]]}}", json);
        }

        [Test]
        public void ShouldReadNestedArrayStates()
        {
            var rowOneValues = new List<MockValueState>
            {
                new MockValueState("", 0),
                new MockValueState("", 0)
            };
            var rowTwoValues = new List<MockValueState>
            {
                new MockValueState("", 0),
                new MockValueState("", 0)
            };
            var rowOne = new StaticArrayState<MockValueState>("", rowOneValues);
            var rowTwo = new StaticArrayState<MockValueState>("", rowTwoValues);
            var root = new ObjectState("GameState", new List<IState>
            {
                new StaticArrayState<StaticArrayState<MockValueState>>("Grid", new List<StaticArrayState<MockValueState>> { rowOne, rowTwo })
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
