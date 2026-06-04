namespace BananaParty.WebSocketRelay
{
    // Key is either string for JSON or int32 for binary
    public interface IStateStorage<TKey>
    {
        // Write and Read for every value type
        void WriteInt(TKey key, int value);
        int ReadInt(TKey key);

        void WriteFloat(TKey key, float value);
        float ReadFloat(TKey key);

        void WriteBool(TKey key, bool value);
        bool ReadBool(TKey key);

        void WriteString(TKey key, string value);
        string ReadString(TKey key);

        void WriteLong(TKey key, long value);
        long ReadLong(TKey key);

        void WriteDouble(TKey key, double value);
        double ReadDouble(TKey key);

        void WriteByte(TKey key, byte value);
        byte ReadByte(TKey key);
    }
}
