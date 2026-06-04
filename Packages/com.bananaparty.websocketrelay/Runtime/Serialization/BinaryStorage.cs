using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStorage : IStateStorage<int>
    {
        private readonly Dictionary<int, object> _data = new();

        public void WriteInt(int key, int value) => _data[key] = value;
        public int ReadInt(int key) => _data.TryGetValue(key, out var val) ? (int)val : 0;

        public void WriteFloat(int key, float value) => _data[key] = value;
        public float ReadFloat(int key) => _data.TryGetValue(key, out var val) ? (float)val : 0f;

        public void WriteBool(int key, bool value) => _data[key] = value;
        public bool ReadBool(int key) => _data.TryGetValue(key, out var val) ? (bool)val : false;

        public void WriteString(int key, string value) => _data[key] = value;
        public string ReadString(int key) => _data.TryGetValue(key, out var val) ? (string)val : string.Empty;

        public void WriteByte(int key, byte value) => _data[key] = value;
        public byte ReadByte(int key) => _data.TryGetValue(key, out var val) ? (byte)val : 0;

        public void WriteShort(int key, short value) => _data[key] = value;
        public short ReadShort(int key) => _data.TryGetValue(key, out var val) ? (short)val : 0;

        public void WriteLong(int key, long value) => _data[key] = value;
        public long ReadLong(int key) => _data.TryGetValue(key, out var val) ? (long)val : 0L;

        public void WriteDouble(int key, double value) => _data[key] = value;
        public double ReadDouble(int key) => _data.TryGetValue(key, out var val) ? (double)val : 0d;

        public void WriteVector2(int key, Vector2 value) => _data[key] = value;
        public Vector2 ReadVector2(int key) => _data.TryGetValue(key, out var val) ? (Vector2)val : Vector2.zero;

        public void WriteVector3(int key, Vector3 value) => _data[key] = value;
        public Vector3 ReadVector3(int key) => _data.TryGetValue(key, out var val) ? (Vector3)val : Vector3.zero;

        public void WriteVector4(int key, Vector4 value) => _data[key] = value;
        public Vector4 ReadVector4(int key) => _data.TryGetValue(key, out var val) ? (Vector4)val : Vector4.zero;

        public void WriteVector2Int(int key, Vector2Int value) => _data[key] = value;
        public Vector2Int ReadVector2Int(int key) => _data.TryGetValue(key, out var val) ? (Vector2Int)val : Vector2Int.zero;

        public void WriteVector3Int(int key, Vector3Int value) => _data[key] = value;
        public Vector3Int ReadVector3Int(int key) => _data.TryGetValue(key, out var val) ? (Vector3Int)val : Vector3Int.zero;

        public void WriteQuaternion(int key, Quaternion value) => _data[key] = value;
        public Quaternion ReadQuaternion(int key) => _data.TryGetValue(key, out var val) ? (Quaternion)val : Quaternion.identity;

        public void WriteColor(int key, Color value) => _data[key] = value;
        public Color ReadColor(int key) => _data.TryGetValue(key, out var val) ? (Color)val : Color.clear;
    }
}
