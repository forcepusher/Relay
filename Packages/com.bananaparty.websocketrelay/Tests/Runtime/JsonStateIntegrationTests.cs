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
            stateA.WriteState(writeGraph);
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
            stateB.ReadState(readGraph);

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
            private IntegerState _playTimeState = new("PlayTime", 0);
            private FloatState _healthState = new("Health", 0f);
            private Vector3State _positionState = new("Position", Vector3.zero);
            private List<IState> _states;

            private List<IState> StatesList => _states ??= new List<IState>
            {
                _playTimeState,
                _healthState,
                _positionState
            };

            public int PlayTime
            {
                get => _playTimeState.Value;
                set => _playTimeState.Value = value;
            }

            public float Health
            {
                get => _healthState.Value;
                set => _healthState.Value = value;
            }

            public Vector3 Position
            {
                get => _positionState.Value;
                set => _positionState.Value = value;
            }

            public string StateName => "MockGameState";

            public void WriteState(IStateOutput stateOutput) => stateOutput.WriteObject(StateName, StatesList);

            public void ReadState(IStateInput stateInput) => stateInput.ReadObject(StateName, StatesList);
        }
    }
}
