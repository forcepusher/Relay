namespace BananaParty.WebSocketRelay
{
    public struct KeyedState
    {
        public KeyedState(string key, object state)
        {
            Key = key;
            State = state;
        }

        public readonly string Key;
        public readonly object State;
    }
}
