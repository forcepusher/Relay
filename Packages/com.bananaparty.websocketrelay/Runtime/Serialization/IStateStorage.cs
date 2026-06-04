using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public interface IStateStorage
    {
        void WriteInt(string key, int value);
        int ReadInt(string key);
        bool HasInt(string key);

        void WriteFloat(string key, float value);
        float ReadFloat(string key);
        bool HasFloat(string key);

        void WriteBool(string key, bool value);
        bool ReadBool(string key);
        bool HasBool(string key);

        void WriteString(string key, string value);
        string ReadString(string key);
        bool HasString(string key);

        void WriteByte(string key, byte value);
        byte ReadByte(string key);
        bool HasByte(string key);

        void WriteShort(string key, short value);
        short ReadShort(string key);
        bool HasShort(string key);

        void WriteLong(string key, long value);
        long ReadLong(string key);
        bool HasLong(string key);

        void WriteDouble(string key, double value);
        double ReadDouble(string key);
        bool HasDouble(string key);

        void WriteVector2(string key, Vector2 value);
        Vector2 ReadVector2(string key);
        bool HasVector2(string key);

        void WriteVector3(string key, Vector3 value);
        Vector3 ReadVector3(string key);
        bool HasVector3(string key);

        void WriteVector4(string key, Vector4 value);
        Vector4 ReadVector4(string key);
        bool HasVector4(string key);

        void WriteVector2Int(string key, Vector2Int value);
        Vector2Int ReadVector2Int(string key);
        bool HasVector2Int(string key);

        void WriteVector3Int(string key, Vector3Int value);
        Vector3Int ReadVector3Int(string key);
        bool HasVector3Int(string key);

        void WriteQuaternion(string key, Quaternion value);
        Quaternion ReadQuaternion(string key);
        bool HasQuaternion(string key);

        void WriteColor(string key, Color value);
        Color ReadColor(string key);
        bool HasColor(string key);
    }
}
