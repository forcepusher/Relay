using UnityEngine;
using System;

namespace BananaParty.WebSocketRelay
{
    public interface IObjectNode
    {
        void WriteBool(string name, bool value);
        bool ReadBool(string name);

        void WriteByte(string name, byte value);
        byte ReadByte(string name);

        void WriteInt(string name, int value);
        int ReadInt(string name);

        void WriteFloat(string name, float value);
        float ReadFloat(string name);

        void WriteLong(string name, long value);
        long ReadLong(string name);

        void WriteString(string name, string value);
        string ReadString(string name);

        void WriteEnum<T>(string name, T value) where T : Enum;
        T ReadEnum<T>(string name) where T : Enum;

        void WriteVector2(string name, Vector2 value);
        Vector2 ReadVector2(string name);

        void WriteVector3(string name, Vector3 value);
        Vector3 ReadVector3(string name);

        void WriteVector4(string name, Vector4 value);
        Vector4 ReadVector4(string name);

        void WriteQuaternion(string name, Quaternion value);
        Quaternion ReadQuaternion(string name);

        void WriteVector2Int(string name, Vector2Int value);
        Vector2Int ReadVector2Int(string name);

        void WriteVector3Int(string name, Vector3Int value);
        Vector3Int ReadVector3Int(string name);

        void WriteColor32(string name, Color32 value);
        Color32 ReadColor32(string name);
    }
}
