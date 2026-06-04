using UnityEngine;

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

        void WriteByte(TKey key, byte value);
        byte ReadByte(TKey key);

        void WriteLong(TKey key, long value);
        long ReadLong(TKey key);

        void WriteDouble(TKey key, double value);
        double ReadDouble(TKey key);

        void WriteVector2(TKey key, Vector2 value);
        Vector2 ReadVector2(TKey key);

        void WriteVector3(TKey key, Vector3 value);
        Vector3 ReadVector3(TKey key);

        void WriteVector4(TKey key, Vector4 value);
        Vector4 ReadVector4(TKey key);

        void WriteVector2Int(TKey key, Vector2Int value);
        Vector2Int ReadVector2Int(TKey key);

        void WriteVector3Int(TKey key, Vector3Int value);
        Vector3Int ReadVector3Int(TKey key);

        void WriteQuaternion(TKey key, Quaternion value);
        Quaternion ReadQuaternion(TKey key);

        void WriteColor(TKey key, Color value);
        Color ReadColor(TKey key);
    }
}
