using UnityEngine;
using System;

namespace BananaParty.WebSocketRelay
{
    public interface IStateStream
    {
        string ToString();

        void WriteBool(string name, bool value);
        bool ReadBool();

        void WriteByte(string name, byte value);
        byte ReadByte();

        void WriteInt(string name, int value);
        int ReadInt();

        void WriteFloat(string name, float value);
        float ReadFloat();

        void WriteLong(string name, long value);
        long ReadLong();

        void WriteString(string name, string value);
        string ReadString();

        void WriteEnum<T>(string name, T value) where T : Enum;
        T ReadEnum<T>() where T : Enum;

        void WriteVector2(string name, Vector2 value);
        Vector2 ReadVector2();

        void WriteVector3(string name, Vector3 value);
        Vector3 ReadVector3();

        void WriteVector4(string name, Vector4 value);
        Vector4 ReadVector4();

        void WriteQuaternion(string name, Quaternion value);
        Quaternion ReadQuaternion();

        void WriteVector2Int(string name, Vector2Int value);
        Vector2Int ReadVector2Int();

        void WriteVector3Int(string name, Vector3Int value);
        Vector3Int ReadVector3Int();

        void WriteColor32(string name, Color32 value);
        Color32 ReadColor32();
    }
}
