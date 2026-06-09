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
        private static readonly Guid Id1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid Id2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid Id3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

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
                new MockEntry { Id = Id1, Value = 10 },
                new MockEntry { Id = Id2, Value = 20 }
            };
            var target = new List<MockEntry>
            {
                new MockEntry { Id = Id1 },
                new MockEntry { Id = Id2 }
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
                new MockEntry { Id = Id1, Value = 10 },
                new MockEntry { Id = Id2, Value = 20 },
                new MockEntry { Id = Id3, Value = 30 }
            };
            MockEntry existing = new MockEntry { Id = Id1 };
            var target = new List<MockEntry> { existing };
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(3, target.Count);
            Assert.AreEqual(5, factory.CreateCount);
            Assert.AreEqual(3, factory.DisposeCount);
            Assert.AreSame(existing, target[0]);
            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
            Assert.AreEqual(30, target[2].Value);
        }

        [Test]
        public void ShouldShrinkDynamicArrayAndInvokeDispose()
        {
            var source = new List<MockEntry> { new MockEntry { Id = Id1, Value = 42 } };
            var target = new List<MockEntry>
            {
                new MockEntry { Id = Id1, Value = 1 },
                new MockEntry { Id = Id2, Value = 2 },
                new MockEntry { Id = Id3, Value = 3 }
            };
            MockEntry removedOne = target[1];
            MockEntry removedTwo = target[2];
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(42, target[0].Value);
            Assert.AreEqual(3, factory.DisposeCount);
            Assert.Contains(removedOne, factory.Disposed);
            Assert.Contains(removedTwo, factory.Disposed);
        }

        [Test]
        public void ShouldUpdateExistingEntriesWithoutCreateOrDispose()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Id = Id1, Value = 100 },
                new MockEntry { Id = Id2, Value = 200 }
            };
            MockEntry first = new MockEntry { Id = Id1, Value = 1 };
            MockEntry second = new MockEntry { Id = Id2, Value = 2 };
            var target = new List<MockEntry> { first, second };
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(2, factory.CreateCount);
            Assert.AreEqual(2, factory.DisposeCount);
            Assert.AreSame(first, target[0]);
            Assert.AreSame(second, target[1]);
            Assert.AreEqual(100, target[0].Value);
            Assert.AreEqual(200, target[1].Value);
        }

        [Test]
        public void ShouldShrinkWithoutFactory()
        {
            var source = new List<MockEntry> { new MockEntry { Id = Id1, Value = 7 } };
            var target = new List<MockEntry>
            {
                new MockEntry { Id = Id1, Value = 1 },
                new MockEntry { Id = Id2, Value = 2 }
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
            var input = new JsonStateInput("{\"Root\":{\"Items\":[2,{\"Id\":\"11111111-1111-1111-1111-111111111111\",\"Value\":1},{\"Id\":\"22222222-2222-2222-2222-222222222222\",\"Value\":2}]}}");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => root.ReadState(input));

            Assert.That(exception.Message, Does.Contain("requires 2 entries"));
            Assert.AreEqual(0, target.Count);
        }

        [Test]
        public void ShouldRoundTripThroughBinaryFormat()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Id = Id1, Value = 5 },
                new MockEntry { Id = Id2, Value = 9 }
            };
            var target = new List<MockEntry> { new MockEntry { Id = Id1 } };
            var factory = new MockEntryFactory();

            var sourceState = new DynamicArrayState<MockEntry>("Items", source);
            var targetState = new DynamicArrayState<MockEntry>("Items", target, factory);

            var output = new BinaryStateOutput();
            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            byte[] bytes = output.ToArray();

            new ObjectState("Root", new List<IState> { targetState }).ReadState(new BinaryStateInput(bytes));

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(3, factory.CreateCount);
            Assert.AreEqual(2, factory.DisposeCount);
            Assert.AreEqual(5, target[0].Value);
            Assert.AreEqual(9, target[1].Value);
        }

        [Test]
        public void ShouldReconcileByKeyAndPreserveExistingInstances()
        {
            var source = new List<MockEntry>
            {
                new MockEntry { Id = Id1, Value = 10 },
                new MockEntry { Id = Id2, Value = 20 }
            };
            MockEntry idTwo = new MockEntry { Id = Id2, Value = 0 };
            MockEntry idOne = new MockEntry { Id = Id1, Value = 0 };
            var target = new List<MockEntry> { idTwo, idOne };
            var factory = new MockEntryFactory();

            RoundTrip(source, target, factory);

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(2, factory.CreateCount);
            Assert.AreEqual(2, factory.DisposeCount);
            Assert.AreSame(idOne, target[0]);
            Assert.AreSame(idTwo, target[1]);
            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
        }

        [UnityTest]
        public IEnumerator ShouldSynchronizeDynamicArrayOverRelay()
        {
            GameObject clientAObj = new GameObject("ClientA");
            GameObject clientBObj = new GameObject("ClientB");

            var stateA = clientAObj.AddComponent<MockGameStateWithDynamicItems>();
            var stateB = clientBObj.AddComponent<MockGameStateWithDynamicItems>();

            stateA.SetItems((Id1, 10), (Id2, 20));

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
            Assert.AreEqual(4, stateB.CreateCount);
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

        private class MockEntry : IKeyedState
        {
            public string StateName => string.Empty;
            public Guid Id { get; set; }
            public Guid StateKey => Id;
            public int Value { get; set; }

            public void WriteState(IStateOutput stateOutput)
            {
                stateOutput.WriteGuid("Id", Id);
                stateOutput.WriteInt("Value", Value);
            }

            public void ReadState(IStateInput stateInput)
            {
                Id = stateInput.ReadGuid("Id");
                Value = stateInput.ReadInt("Value");
            }
        }

        private class MockEntryFactory : IFactory<MockEntry>
        {
            public int CreateCount { get; private set; }
            public int DisposeCount { get; private set; }
            public List<MockEntry> Disposed { get; } = new();

            public MockEntry Create(Guid id)
            {
                CreateCount++;
                return new MockEntry { Id = id };
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

            public MockEntry Create(Guid id)
            {
                CreateCount++;
                return new MockEntry { Id = id };
            }

            public void Dispose(MockEntry entry) => DisposeCount++;

            public void SetItems(params (Guid id, int value)[] items)
            {
                _items.Clear();
                foreach ((Guid id, int value) in items)
                    _items.Add(new MockEntry { Id = id, Value = value });
            }

            public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, _states);

            public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, _states);
        }
    }
}
