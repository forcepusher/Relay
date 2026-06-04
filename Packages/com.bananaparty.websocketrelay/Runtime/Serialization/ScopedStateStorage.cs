using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class ScopedStateStorage : IStateStorage
    {
        private readonly IStateStorage _storage;
        private readonly string _prefix;

        public ScopedStateStorage(IStateStorage storage, string prefix)
        {
            _storage = storage;
            _prefix = prefix;
        }

        private string GetKey(string key) => $"{_prefix}.{key}";

        public void WriteInt(string key, int value) => _storage.WriteInt(GetKey(key), value);
        public int ReadInt(string key) => _storage.ReadInt(GetKey(key));
        public bool HasInt(string key) => _storage.HasInt(GetKey(key));

        public void WriteFloat(string key, float value) => _storage.WriteFloat(GetKey(key), value);
        public float ReadFloat(string key) => _storage.ReadFloat(GetKey(key));
        public bool HasFloat(string key) => _storage.HasFloat(GetKey(key));

        public void WriteBool(string key, bool value) => _storage.WriteBool(GetKey(key), value);
        public bool ReadBool(string key) => _storage.ReadBool(GetKey(key));
        public bool HasBool(string key) => _storage.HasBool(GetKey(key));

        public void WriteString(string key, string value) => _storage.WriteString(GetKey(key), value);
        public string ReadString(string key) => _storage.ReadString(GetKey(key));
        public bool HasString(string key) => _storage.HasString(GetKey(key));

        public void WriteByte(string key, byte value) => _storage.WriteByte(GetKey(key), value);
        public byte ReadByte(string key) => _storage.ReadByte(GetKey(key));
        public bool HasByte(string key) => _storage.HasByte(GetKey(key));

        public void WriteShort(string key, short value) => _storage.WriteShort(GetKey(key), value);
        public short ReadShort(string key) => _storage.ReadShort(GetKey(key));
        public bool HasShort(string key) => _storage.HasShort(GetKey(key));

        public void WriteLong(string key, long value) => _storage.WriteLong(GetKey(key), value);
        public long ReadLong(string key) => _storage.ReadLong(GetKey(key));
        public bool HasLong(string key) => _storage.HasLong(GetKey(key));

        public void WriteDouble(string key, double value) => _storage.WriteDouble(GetKey(key), value);
        public double ReadDouble(string key) => _storage.ReadDouble(GetKey(key));
        public bool HasDouble(string key) => _storage.HasDouble(GetKey(key));

        public void WriteVector2(string key, Vector2 value) => _storage.WriteVector2(GetKey(key), value);
        public Vector2 ReadVector2(string key) => _storage.ReadVector2(GetKey(key));
        public bool HasVector2(string key) => _storage.HasVector2(GetKey(key));

        public void WriteVector3(string key, Vector3 value) => _storage.WriteVector3(GetKey(key), value);
        public Vector3 ReadVector3(string key) => _storage.ReadVector3(GetKey(key));
        public bool HasVector3(string key) => _storage.HasVector3(GetKey(key));

        public void WriteVector4(string key, Vector4 value) => _storage.WriteVector4(GetKey(key), value);
        public Vector4 ReadVector4(string key) => _storage.ReadVector4(GetKey(key));
        public bool HasVector4(string key) => _storage.HasVector4(GetKey(key));

        public void WriteVector2Int(string key, Vector2Int value) => _storage.WriteVector2Int(GetKey(key), value);
        public Vector2Int ReadVector2Int(string key) => _storage.ReadVector2Int(GetKey(key));
        public bool HasVector2Int(string key) => _storage.HasVector2Int(GetKey(key));

        public void WriteVector3Int(string key, Vector3Int value) => _storage.WriteVector3Int(GetKey(key), value);
        public Vector3Int ReadVector3Int(string key) => _storage.ReadVector3Int(GetKey(key));
        public bool HasVector3Int(string key) => _storage.HasVector3Int(GetKey(key));

        public void WriteQuaternion(string key, Quaternion value) => _storage.WriteQuaternion(GetKey(key), value);
        public Quaternion ReadQuaternion(string key) => _storage.ReadQuaternion(GetKey(key));
        public bool HasQuaternion(string key) => _storage.HasQuaternion(GetKey(key));

        public void WriteColor(string key, Color value) => _storage.WriteColor(GetKey(key), value);
        public Color ReadColor(string key) => _storage.ReadColor(GetKey(key));
        public bool HasColor(string key) => _storage.HasColor(GetKey(key));
    }
}
