using UnityEngine;
using System;

namespace BananaParty.WebSocketRelay
{
    public interface IStateStream
    {
        string ToString();

        void WriteBool(bool value);
        bool ReadBool();

        void WriteByte(byte value);
        byte ReadByte();

        void WriteInt(int value);
        int ReadInt();

        void WriteFloat(float value);
        float ReadFloat();

        void WriteLong(long value);
        long ReadLong();

        void WriteString(string value);
        string ReadString();

        void WriteEnum<T>(T value) where T : Enum;
        T ReadEnum<T>() where T : Enum;

        void WriteVector2(Vector2 value);
        Vector2 ReadVector2();

        void WriteVector3(Vector3 value);
        Vector3 ReadVector3();

        void WriteVector4(Vector4 value);
        Vector4 ReadVector4();

        void WriteQuaternion(Quaternion value);
        Quaternion ReadQuaternion();

        void WriteVector2Int(Vector2Int value);
        Vector2Int ReadVector2Int();

        void WriteVector3Int(Vector3Int value);
        Vector3Int ReadVector3Int();

        void WriteColor32(Color32 value);
        Color32 ReadColor32();
    }
}
