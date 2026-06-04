using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public interface IStateStorage<TKey>
    {
        void WriteInt(TKey key, int value);
        int ReadInt(TKey key);
        bool HasInt(TKey key);

        void WriteFloat(TKey key, float value);
        float ReadFloat(TKey key);
        bool HasFloat(TKey key);

        void WriteBool(TKey key, bool value);
        bool ReadBool(TKey key);
        bool HasBool(TKey key);

        void WriteString(TKey key, string value);
        string ReadString(TKey key);
        bool HasString(TKey key);

        void WriteByte(TKey key, byte value);
        byte ReadByte(TKey key);
        bool HasByte(TKey key);

        void WriteShort(TKey key, short value);
        short ReadShort(TKey key);
        bool HasShort(TKey key);

        void WriteLong(TKey key, long value);
        long ReadLong(TKey key);
        bool HasLong(TKey key);

        void WriteDouble(TKey key, double value);
        double ReadDouble(TKey key);
        bool HasDouble(TKey key);

        void WriteVector2(TKey key, Vector2 value);
        Vector2 ReadVector2(TKey key);
        bool HasVector2(TKey key);

        void WriteVector3(TKey key, Vector3 value);
        Vector3 ReadVector3(TKey key);
        bool HasVector3(TKey key);

        void WriteVector4(TKey key, Vector4 value);
        Vector4 ReadVector4(TKey key);
        bool HasVector4(TKey key);

        void WriteVector2Int(TKey key, Vector2Int value);
        Vector2Int ReadVector2Int(TKey key);
        bool HasVector2Int(TKey key);

        void WriteVector3Int(TKey key, Vector3Int value);
        Vector3Int ReadVector3Int(TKey key);
        bool HasVector3Int(TKey key);

        void WriteQuaternion(TKey key, Quaternion value);
        Quaternion ReadQuaternion(TKey key);
        bool HasQuaternion(TKey key);

        void WriteColor(TKey key, Color value);
        Color ReadColor(TKey key);
        bool HasColor(TKey key);
    }
}
