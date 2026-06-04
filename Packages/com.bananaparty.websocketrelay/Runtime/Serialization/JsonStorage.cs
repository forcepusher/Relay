using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class JsonStorage : IStateStorage<string>
    {
        private readonly Dictionary<string, object> _data = new();

        public void WriteInt(string key, int value) => _data[key] = value;
        public int ReadInt(string key) => _data.TryGetValue(key, out var val) ? (int)val : 0;
        public bool HasInt(string key) => _data.TryGetValue(key, out var val) && val is int;

        public void WriteFloat(string key, float value) => _data[key] = value;
        public float ReadFloat(string key) => _data.TryGetValue(key, out var val) ? (float)val : 0f;
        public bool HasFloat(string key) => _data.TryGetValue(key, out var val) && val is float;

        public void WriteBool(string key, bool value) => _data[key] = value;
        public bool ReadBool(string key) => _data.TryGetValue(key, out var val) ? (bool)val : false;
        public bool HasBool(string key) => _data.TryGetValue(key, out var val) && val is bool;

        public void WriteString(string key, string value) => _data[key] = value;
        public string ReadString(string key) => _data.TryGetValue(key, out var val) ? (string)val : string.Empty;
        public bool HasString(string key) => _data.TryGetValue(key, out var val) && val is string;

        public void WriteByte(string key, byte value) => _data[key] = value;
        public byte ReadByte(string key) => _data.TryGetValue(key, out var val) ? (byte)val : 0;
        public bool HasByte(string key) => _data.TryGetValue(key, out var val) && val is byte;

        public void WriteShort(string key, short value) => _data[key] = value;
        public short ReadShort(string key) => _data.TryGetValue(key, out var val) ? (short)val : 0;
        public bool HasShort(string key) => _data.TryGetValue(key, out var val) && val is short;

        public void WriteLong(string key, long value) => _data[key] = value;
        public long ReadLong(string key) => _data.TryGetValue(key, out var val) ? (long)val : 0L;
        public bool HasLong(string key) => _data.TryGetValue(key, out var val) && val is long;

        public void WriteDouble(string key, double value) => _data[key] = value;
        public double ReadDouble(string key) => _data.TryGetValue(key, out var val) ? (double)val : 0d;
        public bool HasDouble(string key) => _data.TryGetValue(key, out var val) && val is double;

        public void WriteVector2(string key, Vector2 value) => _data[key] = value;
        public Vector2 ReadVector2(string key) => _data.TryGetValue(key, out var val) ? (Vector2)val : Vector2.zero;
        public bool HasVector2(string key) => _data.TryGetValue(key, out var val) && val is Vector2;

        public void WriteVector3(string key, Vector3 value) => _data[key] = value;
        public Vector3 ReadVector3(string key) => _data.TryGetValue(key, out var val) ? (Vector3)val : Vector3.zero;
        public bool HasVector3(string key) => _data.TryGetValue(key, out var val) && val is Vector3;

        public void WriteVector4(string key, Vector4 value) => _data[key] = value;
        public Vector4 ReadVector4(string key) => _data.TryGetValue(key, out var val) ? (Vector4)val : Vector4.zero;
        public bool HasVector4(string key) => _data.TryGetValue(key, out var val) && val is Vector4;

        public void WriteVector2Int(string key, Vector2Int value) => _data[key] = value;
        public Vector2Int ReadVector2Int(string key) => _data.TryGetValue(key, out var val) ? (Vector2Int)val : Vector2Int.zero;
        public bool HasVector2Int(string key) => _data.TryGetValue(key, out var val) && val is Vector2Int;

        public void WriteVector3Int(string key, Vector3Int value) => _data[key] = value;
        public Vector3Int ReadVector3Int(string key) => _data.TryGetValue(key, out var val) ? (Vector3Int)val : Vector3Int.zero;
        public bool HasVector3Int(string key) => _data.TryGetValue(key, out var val) && val is Vector3Int;

        public void WriteQuaternion(string key, Quaternion value) => _data[key] = value;
        public Quaternion ReadQuaternion(string key) => _data.TryGetValue(key, out var val) ? (Quaternion)val : Quaternion.identity;
        public bool HasQuaternion(string key) => _data.TryGetValue(key, out var val) && val is Quaternion;

        public void WriteColor(string key, Color value) => _data[key] = value;
        public Color ReadColor(string key) => _data.TryGetValue(key, out var val) ? (Color)val : Color.clear;
        public bool HasColor(string key) => _data.TryGetValue(key, out var val) && val is Color;
    }
}
