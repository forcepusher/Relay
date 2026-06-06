using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BananaParty.WebSocketRelay;

namespace BananaParty.WebSocketRelay.Tests
{
    public class JsonStateIntegrationTests
    {
        private const string ServerAddress = "ws://127.0.0.1:23144";

        [SetUp]
        public void SetUp()
        {
            RelayServerLauncher.StartAsync().Wait();
        }

        [TearDown]
        public void TearDown()
        {
            RelayServerLauncher.StopAsync().Wait();
        }

        [UnityTest]
        public IEnumerator FullSerializationDeserializationFlow_OverRelay_Success()
        {
            // Arrange: Create two clients and their respective game states
            GameObject clientAObj = new GameObject("ClientA");
            GameObject clientBObj = new GameObject("ClientB");

            var stateA = clientAObj.AddComponent<MockGameState>();
            var stateB = clientBObj.AddComponent<MockGameState>();

            stateA.PlayTime = 10;
            stateA.Health = 80f;
            stateA.Position = new Vector3(1, 2, 3);

            using Socket socketA = new Socket(ServerAddress);
            using Socket socketB = new Socket(ServerAddress);

            socketA.Connect();
            socketB.Connect();

            // Wait for connections
            float timeout = 5f;
            float elapsed = 0;
            while (!socketA.IsConnected || !socketB.IsConnected)
            {
                if (elapsed > timeout) Assert.Fail("Sockets failed to connect within timeout");
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Act: Client A serializes and sends state
            JsonWriteStateGraph writeGraph = new();
            stateA.WriteStateToJson(writeGraph);
            string jsonPayload = writeGraph.ToString();
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonPayload);

            socketA.Send(bytesToSend);

            // Wait for Client B to receive the payload
            elapsed = 0;
            while (!socketB.HasUnreadPayloadQueue)
            {
                if (elapsed > timeout) Assert.Fail("Client B did not receive payload within timeout");
                elapsed += Time.deltaTime;
                yield return null;
            }

            byte[] receivedBytes = socketB.ReadPayloadQueue();
            string receivedJson = Encoding.UTF8.GetString(receivedBytes);

            // Client B deserializes the state
            JsonReadStateGraph readGraph = new JsonReadStateGraph(receivedJson);
            stateB.ReadStateFromJson(readGraph);

            // Assert: Verify values were synchronized
            Assert.AreEqual(stateA.PlayTime, stateB.PlayTime, "PlayTime should be synchronized");
            Assert.AreEqual(stateA.Health, stateB.Health, 0.01f, "Health should be synchronized");
            Assert.AreEqual(stateA.Position, stateB.Position, "Position should be synchronized");

            // Cleanup GameObjects
            UnityEngine.Object.DestroyImmediate(clientAObj);
            UnityEngine.Object.DestroyImmediate(clientBObj);
        }

        private class MockGameState : MonoBehaviour, IObjectNode, IJsonState
        {
            public int PlayTime { get; set; }
            public float Health { get; set; }
            public Vector3 Position { get; set; }

            private IntegerValueNode _playTimeNode => new("PlayTime", PlayTime);
            private FloatValueNode _healthNode => new("Health", Health);
            private Vector3ValueNode _positionNode => new("Position", Position);

            public string Name => "MockGameState";

            public List<INode> GetNodes()
            {
                return new List<INode> { _playTimeNode, _healthNode, _positionNode };
            }

            public void WriteStateToJson(JsonWriteStateGraph jsonStateGraph)
            {
                jsonStateGraph.StartChildGroup(Name);
                _playTimeNode.WriteStateToJson(jsonStateGraph);
                _healthNode.WriteStateToJson(jsonStateGraph);
                _positionNode.WriteStateToJson(jsonStateGraph);
                jsonStateGraph.EndChildGroup();
            }

            public void ReadStateFromJson(JsonReadStateGraph jsonReadStateGraph)
            {
                jsonReadStateGraph.StartChildGroup(Name);
                var pt = new IntegerValueNode("PlayTime", 0);
                pt.ReadStateFromJson(jsonReadStateGraph);
                PlayTime = pt.Value;

                var h = new FloatValueNode("Health", 0f);
                h.ReadStateFromJson(jsonReadStateGraph);
                Health = h.Value;

                var p = new Vector3ValueNode("Position", Vector3.zero);
                p.ReadStateFromJson(jsonReadStateGraph);
                Position = p.Value;

                jsonReadStateGraph.EndChildGroup();
            }
        }
    }
}
