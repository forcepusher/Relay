using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStorage : IStateStorage<int>
    {
        private readonly Dictionary<int, int> _ints = new();
        private readonly Dictionary<int, float> _floats = new();
        private readonly Dictionary<int, bool> _bools = new();
        private readonly Dictionary<int, string> _strings = new();
        private readonly Dictionary<int, byte> _bytes = new();
        private readonly Dictionary<int, short> _shorts = new();
        private readonly Dictionary<int, long> _longs = new();
        private readonly Dictionary<int, double> _doubles = new();
        private readonly Dictionary<int, Vector2> _vector2s = new();
        private readonly Dictionary<int, Vector3> _vector3s = new();
        private readonly Dictionary<int, Vector4> _vector4s = new();
        private readonly Dictionary<int, Vector2Int> _vector2Ints = new();
        private readonly Dictionary<int, Vector3Int> _vector3Ints = new();
        private readonly Dictionary<int, Quaternion> _quaternions = new();
        private readonly Dictionary<int, Color> _colors = new();

        public void WriteInt(int key, int value) => _ints[key] = value;
        public int ReadInt(int key) => _ints[key];
        public bool HasInt(int key) => _ints.ContainsKey(key);

        public void WriteFloat(int key, float value) => _floats[key] = value;
        public float ReadFloat(int key) => _floats[key];
        public bool HasFloat(int key) => _floats.ContainsKey(key);

        public void WriteBool(int key, bool value) => _bools[key] = value;
        public bool ReadBool(int key) => _bools[key];
        public bool HasBool(int key) => _bools.ContainsKey(key);

        public void WriteString(int key, string value) => _strings[key] = value;
        public string ReadString(int key) => _strings[key];
        public bool HasString(int key) => _strings.ContainsKey(key);

        public void WriteByte(int key, byte value) => _bytes[key] = value;
        public byte ReadByte(int key) => _bytes[key];
        public bool HasByte(int key) => _bytes.ContainsKey(key);

        public void WriteShort(int key, short value) => _shorts[key] = value;
        public short ReadShort(int key) => _shorts[key];
        public bool HasShort(int key) => _shorts.ContainsKey(key);

        public void WriteLong(int key, long value) => _longs[key] = value;
        public long ReadLong(int key) => _longs[key];
        public bool HasLong(int key) => _longs.ContainsKey(key);

        public void WriteDouble(int key, double value) => _doubles[key] = value;
        public double ReadDouble(int key) => _doubles[key];
        public bool HasDouble(int key) => _doubles.ContainsKey(key);

        public void WriteVector2(int key, Vector2 value) => _vector2s[key] = value;
        public Vector2 ReadVector2(int key) => _vector2s[key];
        public bool HasVector2(int key) => _vector2s.ContainsKey(key);

        public void WriteVector3(int key, Vector3 value) => _vector3s[key] = value;
        public Vector3 ReadVector3(int key) => _vector3s[key];
        public bool HasVector3(int key) => _vector3s.ContainsKey(key);

        public void WriteVector4(int key, Vector4 value) => _vector4s[key] = value;
        public Vector4 ReadVector4(int key) => _vector4s[key];
        public bool HasVector4(int key) => _vector4s.ContainsKey(key);

        public void WriteVector2Int(int key, Vector2Int value) => _vector2Ints[key] = value;
        public Vector2Int ReadVector2Int(int key) => _vector2Ints[key];
        public bool HasVector2Int(int key) => _vector2Ints.ContainsKey(key);

        public void WriteVector3Int(int key, Vector3Int value) => _vector3Ints[key] = value;
        public Vector3Int ReadVector3Int(int key) => _vector3Ints[key];
        public bool HasVector3Int(int key) => _vector3Ints.ContainsKey(key);

        public void WriteQuaternion(int key, Quaternion value) => _quaternions[key] = value;
        public Quaternion ReadQuaternion(int key) => _quaternions[key];
        public bool HasQuaternion(int key) => _quaternions.ContainsKey(key);

        public void WriteColor(int key, Color value) => _colors[key] = value;
        public Color ReadColor(int key) => _colors[key];
        public bool HasColor(int key) => _colors.ContainsKey(key);
    }
}
