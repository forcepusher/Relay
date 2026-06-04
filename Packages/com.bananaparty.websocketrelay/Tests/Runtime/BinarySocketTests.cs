using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace BananaParty.WebSocketRelay.Tests
{
    public class BinarySocketTests
    {
        private Socket _socketA;
        private Socket _socketB;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return RelayServerLauncher.StartCoroutine();

            _socketA = new("ws://localhost:23144");
            _socketB = new("ws://localhost:23144");

            Assert.IsFalse(_socketA.IsConnected, $"{nameof(_socketA.IsConnected)} is {true} immediately after creation.");
            Assert.IsFalse(_socketB.IsConnected, $"{nameof(_socketB.IsConnected)} is {true} immediately after creation.");

            _socketA.Connect();
            _socketB.Connect();

            Assert.IsFalse(_socketA.HasUnreadPayloadQueue, $"{nameof(_socketA.HasUnreadPayloadQueue)} is {true} immediately after creation.");
            Assert.IsFalse(_socketB.HasUnreadPayloadQueue, $"{nameof(_socketB.HasUnreadPayloadQueue)} is {true} immediately after creation.");

            yield return new WaitWhile(() => !_socketA.IsConnected || !_socketB.IsConnected, TestParameters.ConnectTimeoutThreshold);

            Assert.IsTrue(_socketA.IsConnected, $"{nameof(_socketA.Connect)} did not flip {nameof(_socketA.IsConnected)} to {true} within {nameof(TestParameters.ConnectTimeoutThreshold)} of {TestParameters.ConnectTimeoutThreshold} seconds.");
            Assert.IsTrue(_socketB.IsConnected, $"{nameof(_socketB.Connect)} did not flip {nameof(_socketB.IsConnected)} to {true} within {nameof(TestParameters.ConnectTimeoutThreshold)} of {TestParameters.ConnectTimeoutThreshold} seconds.");
        }

        [UnityTest]
        public IEnumerator ShouldRelaySmallMessage()
        {
            yield return TestRelay(new byte[] { 1, 0, 42, 228, 255, 0 });
        }

        [UnityTest]
        public IEnumerator ShouldRelaySequenceOfMessages()
        {
            yield return TestRelay(GenerateRandomBytes(4096));
            yield return TestRelay(GenerateRandomBytes(1234));
        }

        [UnityTest]
        public IEnumerator ShouldRelayHugeMessage()
        {
            yield return TestRelay(GenerateRandomBytes(40000));
        }

        private IEnumerator TestRelay(byte[] bytesToSend)
        {
            _socketA.Send(bytesToSend);

            yield return new WaitWhile(() => !_socketB.HasUnreadPayloadQueue, TestParameters.ReceiveTimeoutThreshold);

            Assert.IsTrue(_socketB.HasUnreadPayloadQueue, $"Timeout waiting for message. {nameof(_socketB.HasUnreadPayloadQueue)} did not flip to {true}.");

            byte[] receivedBytes = _socketB.ReadPayloadQueue();

            Assert.IsTrue(bytesToSend.SequenceEqual(receivedBytes), $"Received corrupted data from relay. Expected {bytesToSend.Length} bytes, but received {receivedBytes.Length}.");
        }

        private byte[] GenerateRandomBytes(int length)
        {
            var random = new Random();
            byte[] bytes = new byte[length];
            for (int byteIterator = 0; byteIterator < bytes.Length; byteIterator += 1)
                bytes[byteIterator] = (byte)random.Next(byte.MaxValue);

            return bytes;
        }

        [UnityTearDown]
        public IEnumerator Teardown()
        {
            _socketA.Disconnect();
            _socketB.Disconnect();

            yield return new WaitWhile(() => _socketA.IsConnected || _socketB.IsConnected, TestParameters.DisconnectTimeoutThreshold);

            Assert.IsFalse(_socketA.IsConnected, $"{nameof(_socketA.Disconnect)} did not flip {nameof(_socketA.IsConnected)} to {false} within {nameof(TestParameters.DisconnectTimeoutThreshold)} of {TestParameters.DisconnectTimeoutThreshold} seconds.");
            Assert.IsFalse(_socketB.IsConnected, $"{nameof(_socketB.Disconnect)} did not flip {nameof(_socketB.IsConnected)} to {false} within {nameof(TestParameters.DisconnectTimeoutThreshold)} of {TestParameters.DisconnectTimeoutThreshold} seconds.");

            yield return RelayServerLauncher.StopCoroutine();
        }
    }
}
