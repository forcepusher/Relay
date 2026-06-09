using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BananaParty.WebSocketRelay.Tests
{
    public class DynamicArrayStateIntegrationTests
    {
        private static string ServerAddress => $"ws://127.0.0.1:{TestParameters.RelayServerPort}";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return RelayServerLauncher.StartCoroutine();
        }

        [Test]
        public void ShouldWriteAndReadDynamicArrayWithMatchingCount()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Value = 10 },
                new MockEntry { Value = 20 }
            };
            var target = new List<MockEntry>
            {
                new MockEntry(),
                new MockEntry()
            };

            RoundTrip(source, target);

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
        }

        [Test]
        public void ShouldGrowDynamicArrayAndInvokeCreate()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Value = 10 },
                new MockEntry { Value = 20 },
                new MockEntry { Value = 30 }
            };
            var target = new List<MockEntry> { new MockEntry() };
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(3, target.Count);
            Assert.AreEqual(2, factory.CreateCount);
            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
            Assert.AreEqual(30, target[2].Value);
        }

        [Test]
        public void ShouldShrinkDynamicArrayAndInvokeDispose()
        {
            var source = new List<MockEntry> { new MockEntry { Value = 42 } };
            var target = new List<MockEntry>
            {
                new MockEntry { Value = 1 },
                new MockEntry { Value = 2 },
                new MockEntry { Value = 3 }
            };
            MockEntry removedOne = target[1];
            MockEntry removedTwo = target[2];
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(42, target[0].Value);
            Assert.AreEqual(2, factory.DisposeCount);
            Assert.Contains(removedOne, factory.Disposed);
            Assert.Contains(removedTwo, factory.Disposed);
        }

        [Test]
        public void ShouldUpdateExistingEntriesWithoutCreateOrDispose()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Value = 100 },
                new MockEntry { Value = 200 }
            };
            var target = new List<MockEntry>
            {
                new MockEntry { Value = 1 },
                new MockEntry { Value = 2 }
            };
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(0, factory.CreateCount);
            Assert.AreEqual(0, factory.DisposeCount);
            Assert.AreEqual(100, target[0].Value);
            Assert.AreEqual(200, target[1].Value);
        }

        [Test]
        public void ShouldShrinkWithoutFactory()
        {
            var source = new List<MockEntry> { new MockEntry { Value = 7 } };
            var target = new List<MockEntry>
            {
                new MockEntry { Value = 1 },
                new MockEntry { Value = 2 }
            };

            RoundTrip(source, target);

            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(7, target[0].Value);
        }

        [Test]
        public void ShouldThrowWhenGrowingWithoutFactory()
        {
            var target = new List<MockEntry>();
            var itemsState = new DynamicArrayState<MockEntry>("Items", target);
            var root = new ObjectState("Root", new List<IState> { itemsState });
            var input = new JsonStateInput("{\"Root\":{\"Items\":[2,{\"Value\":1},{\"Value\":2}]}}");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => root.ReadState(input));

            Assert.That(exception.Message, Does.Contain("requires 2 entries"));
            Assert.AreEqual(0, target.Count);
        }

        [Test]
        public void ShouldRoundTripThroughBinaryFormat()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Value = 5 },
                new MockEntry { Value = 9 }
            };
            var target = new List<MockEntry> { new MockEntry() };
            var factory = new MockEntryFactory();

            var sourceState = new DynamicArrayState<MockEntry>("Items", source);
            var targetState = new DynamicArrayState<MockEntry>("Items", target, factory);

            var output = new BinaryStateOutput();
            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            byte[] bytes = output.ToArray();

            new ObjectState("Root", new List<IState> { targetState }).ReadState(new BinaryStateInput(bytes));

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(1, factory.CreateCount);
            Assert.AreEqual(5, target[0].Value);
            Assert.AreEqual(9, target[1].Value);
        }

        [UnityTest]
        public IEnumerator ShouldSynchronizeDynamicArrayOverRelay()
        {
            GameObject clientAObj = new GameObject("ClientA");
            GameObject clientBObj = new GameObject("ClientB");

            var stateA = clientAObj.AddComponent<MockGameStateWithDynamicItems>();
            var stateB = clientBObj.AddComponent<MockGameStateWithDynamicItems>();

            stateA.SetItems(10, 20);

            using Socket socketA = new Socket(ServerAddress);
            using Socket socketB = new Socket(ServerAddress);

            socketA.Connect();
            socketB.Connect();

            float timeout = 5f;
            float elapsed = 0;
            while (!socketA.IsConnected || !socketB.IsConnected)
            {
                if (elapsed > timeout)
                    Assert.Fail("Sockets failed to connect within timeout");

                elapsed += Time.deltaTime;
                yield return null;
            }

            JsonStateOutput writeGraph = new();
            stateA.WriteState(writeGraph);
            socketA.Send(Encoding.UTF8.GetBytes(writeGraph.ToString()));

            elapsed = 0;
            while (!socketB.HasUnreadPayloadQueue)
            {
                if (elapsed > timeout)
                    Assert.Fail("Client B did not receive payload within timeout");

                elapsed += Time.deltaTime;
                yield return null;
            }

            stateB.ReadState(new JsonStateInput(Encoding.UTF8.GetString(socketB.ReadPayloadQueue())));

            Assert.AreEqual(2, stateB.Items.Count);
            Assert.AreEqual(2, stateB.CreateCount);
            Assert.AreEqual(10, stateB.Items[0].Value);
            Assert.AreEqual(20, stateB.Items[1].Value);

            UnityEngine.Object.DestroyImmediate(clientAObj);
            UnityEngine.Object.DestroyImmediate(clientBObj);
        }

        private static void RoundTrip(List<MockEntry> source, List<MockEntry> target)
        {
            var sourceState = new DynamicArrayState<MockEntry>("Items", source);
            var targetState = new DynamicArrayState<MockEntry>("Items", target);
            var output = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            new ObjectState("Root", new List<IState> { targetState }).ReadState(new JsonStateInput(output.ToString()));
        }

        private static void RoundTrip(List<MockEntry> source, List<MockEntry> target, IFactory<MockEntry> factory)
        {
            var sourceState = new DynamicArrayState<MockEntry>("Items", source);
            var targetState = new DynamicArrayState<MockEntry>("Items", target, factory);
            var output = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            new ObjectState("Root", new List<IState> { targetState }).ReadState(new JsonStateInput(output.ToString()));
        }

        private class MockEntry : IState
        {
            public string StateName => string.Empty;
            public int Value { get; set; }

            public void WriteState(IStateOutput stateOutput) => stateOutput.WriteInt("Value", Value);

            public void ReadState(IStateInput stateInput) => Value = stateInput.ReadInt("Value");
        }

        private class MockEntryFactory : IFactory<MockEntry>
        {
            public int CreateCount { get; private set; }
            public int DisposeCount { get; private set; }
            public List<MockEntry> Disposed { get; } = new();

            public MockEntry Create()
            {
                CreateCount++;
                return new MockEntry();
            }

            public void Dispose(MockEntry entry)
            {
                DisposeCount++;
                Disposed.Add(entry);
            }
        }

        private class MockGameStateWithDynamicItems : MonoBehaviour, IState, IFactory<MockEntry>
        {
            private readonly List<MockEntry> _items = new();
            private DynamicArrayState<MockEntry> _itemsState;
            private List<IState> _states;

            public IReadOnlyList<MockEntry> Items => _items;
            public int CreateCount { get; private set; }
            public int DisposeCount { get; private set; }

            public string StateName => "MockGameStateWithDynamicItems";

            private void Awake()
            {
                _itemsState = new DynamicArrayState<MockEntry>("Items", _items, this);
                _states = new List<IState> { _itemsState };
            }

            public MockEntry Create()
            {
                CreateCount++;
                return new MockEntry();
            }

            public void Dispose(MockEntry entry) => DisposeCount++;

            public void SetItems(params int[] values)
            {
                _items.Clear();
                foreach (int value in values)
                    _items.Add(new MockEntry { Value = value });
            }

            public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

            public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
        }
    }
}
