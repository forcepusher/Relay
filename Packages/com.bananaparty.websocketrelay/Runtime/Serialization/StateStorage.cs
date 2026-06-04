using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class StateStorage
    {
        private readonly Dictionary<string, int> _ints = new();
        private readonly Dictionary<string, float> _floats = new();
        private readonly Dictionary<string, bool> _bools = new();
        private readonly Dictionary<string, string> _strings = new();
        private readonly Dictionary<string, byte> _bytes = new();
        private readonly Dictionary<string, short> _shorts = new();
        private readonly Dictionary<string, long> _longs = new();
        private readonly Dictionary<string, double> _doubles = new();
        private readonly Dictionary<string, Vector2> _vector2s = new();
        private readonly Dictionary<string, Vector3> _vector3s = new();
        private readonly Dictionary<string, Vector4> _vector4s = new();
        private readonly Dictionary<string, Vector2Int> _vector2Ints = new();
        private readonly Dictionary<string, Vector3Int> _vector3Ints = new();
        private readonly Dictionary<string, Quaternion> _quaternions = new();
        private readonly Dictionary<string, Color> _colors = new();

        public void WriteInt(string key, int value) => _ints[key] = value;
        public int ReadInt(string key) => _ints[key];
        public bool HasInt(string key) => _ints.ContainsKey(key);

        public void WriteFloat(string key, float value) => _floats[key] = value;
        public float ReadFloat(string key) => _floats[key];
        public bool HasFloat(string key) => _floats.ContainsKey(key);

        public void WriteBool(string key, bool value) => _bools[key] = value;
        public bool ReadBool(string key) => _bools[key];
        public bool HasBool(string key) => _bools.ContainsKey(key);

        public void WriteString(string key, string value) => _strings[key] = value;
        public string ReadString(string key) => _strings[key];
        public bool HasString(string key) => _strings.ContainsKey(key);

        public void WriteByte(string key, byte value) => _bytes[key] = value;
        public byte ReadByte(string key) => _bytes[key];
        public bool HasByte(string key) => _bytes.ContainsKey(key);

        public void WriteShort(string key, short value) => _shorts[key] = value;
        public short ReadShort(string key) => _shorts[key];
        public bool HasShort(string key) => _shorts.ContainsKey(key);

        public void WriteLong(string key, long value) => _longs[key] = value;
        public long ReadLong(string key) => _longs[key];
        public bool HasLong(string key) => _longs.ContainsKey(key);

        public void WriteDouble(string key, double value) => _doubles[key] = value;
        public double ReadDouble(string key) => _doubles[key];
        public bool HasDouble(string key) => _doubles.ContainsKey(key);

        public void WriteVector2(string key, Vector2 value) => _vector2s[key] = value;
        public Vector2 ReadVector2(string key) => _vector2s[key];
        public bool HasVector2(string key) => _vector2s.ContainsKey(key);

        public void WriteVector3(string key, Vector3 value) => _vector3s[key] = value;
        public Vector3 ReadVector3(string key) => _vector3s[key];
        public bool HasVector3(string key) => _vector3s.ContainsKey(key);

        public void WriteVector4(string key, Vector4 value) => _vector4s[key] = value;
        public Vector4 ReadVector4(string key) => _vector4s[key];
        public bool HasVector4(string key) => _vector4s.ContainsKey(key);

        public void WriteVector2Int(string key, Vector2Int value) => _vector2Ints[key] = value;
        public Vector2Int ReadVector2Int(string key) => _vector2Ints[key];
        public bool HasVector2Int(string key) => _vector2Ints.ContainsKey(key);

        public void WriteVector3Int(string key, Vector3Int value) => _vector3Ints[key] = value;
        public Vector3Int ReadVector3Int(string key) => _vector3Ints[key];
        public bool HasVector3Int(string key) => _vector3Ints.ContainsKey(key);

        public void WriteQuaternion(string key, Quaternion value) => _quaternions[key] = value;
        public Quaternion ReadQuaternion(string key) => _quaternions[key];
        public bool HasQuaternion(string key) => _quaternions.ContainsKey(key);

        public void WriteColor(string key, Color value) => _colors[key] = value;
        public Color ReadColor(string key) => _colors[key];
        public bool HasColor(string key) => _colors.ContainsKey(key);
    }
}
