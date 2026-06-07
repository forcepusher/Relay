using System;

namespace BananaParty.WebSocketRelay.Tests
{
    public static class TestParameters
    {
        public const int RelayServerPort = 23144; // Leet for RELAY

        public const float ConnectTimeoutThreshold = 3f;
        public const float ReceiveTimeoutThreshold = 5f;
        public const float DisconnectTimeoutThreshold = 3f;
    }
}
