namespace BananaParty.WebSocketRelay
{
    public struct StateEntry
    {
        public StateEntry(string key, object state)
        {
            Key = key;
            State = state;
        }

        public readonly string Key;
        public readonly object State;
    }
}
