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
        private static string ServerAddress => $"ws://127.0.0.1:{TestParameters.RelayServerPort}";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return RelayServerLauncher.StartCoroutine();
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
            JsonStateOutput writeGraph = new();
            stateA.Write(writeGraph);
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
            JsonStateInput readGraph = new JsonStateInput(receivedJson);
            stateB.Read(readGraph);

            // Assert: Verify values were synchronized
            Assert.AreEqual(stateA.PlayTime, stateB.PlayTime, "PlayTime should be synchronized");
            Assert.AreEqual(stateA.Health, stateB.Health, 0.01f, "Health should be synchronized");
            Assert.AreEqual(stateA.Position, stateB.Position, "Position should be synchronized");

            // Cleanup GameObjects
            UnityEngine.Object.DestroyImmediate(clientAObj);
            UnityEngine.Object.DestroyImmediate(clientBObj);
        }

        private class MockGameState : MonoBehaviour, IState
        {
            public int PlayTime { get; set; }
            public float Health { get; set; }
            public Vector3 Position { get; set; }

            private IntegerValueState _playTimeState => new("PlayTime", PlayTime);
            private FloatValueState _healthState => new("Health", Health);
            private Vector3ValueState _positionState => new("Position", Position);

            public string Name => "MockGameState";

            public void Write(IStateOutput writeGraph)
            {
                writeGraph.StartObject(Name);
                _playTimeState.Write(writeGraph);
                _healthState.Write(writeGraph);
                _positionState.Write(writeGraph);
                writeGraph.EndObject();
            }

            public void Read(IStateInput readGraph)
            {
                readGraph.StartObject(Name);
                var pt = new IntegerValueState("PlayTime", 0);
                pt.Read(readGraph);
                PlayTime = pt.Value;

                var h = new FloatValueState("Health", 0f);
                h.Read(readGraph);
                Health = h.Value;

                var p = new Vector3ValueState("Position", Vector3.zero);
                p.Read(readGraph);
                Position = p.Value;

                readGraph.EndObject();
            }
        }
    }
}
