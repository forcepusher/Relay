namespace BananaParty.WebSocketRelay
{
    // Key is either string for JSON or int32 for binary
    public interface IStateStorage<TKey>
    {
        // Write and Read for every value type
        void WriteInt(TKey key, int value);
        int ReadInt(TKey key);
    }
}
