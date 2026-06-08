using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryReadGraph : IReadGraph
    {
        private readonly byte[] _data;
        private int _pos;
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public BinaryReadGraph(byte[] data)
        {
            _data = data ?? Array.Empty<byte>();
        }

        public void StartObject(string name)
        {
            ReadContainerName(name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            ReadContainerName(name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public string ReadEntry(string name)
        {
            if (!TryMatchEntryName(name))
                return null;

            return ReadInt32().ToString();
        }

        public int ReadIntEntry(string name)
        {
            if (!TryMatchEntryName(name))
                return 0;

            return ReadInt32();
        }

        public float ReadFloatEntry(string name)
        {
            if (!TryMatchEntryName(name))
                return 0f;

            return ReadFloat32();
        }

        public bool ReadBoolEntry(string name)
        {
            if (!TryMatchEntryName(name))
                return false;

            return ReadBool();
        }

        public string ReadStringEntry(string name)
        {
            if (!TryMatchEntryName(name))
                return null;

            return ReadString();
        }

        public int ReadIntArrayEntry()
        {
            if (!TryMatchEntryName(null))
                return 0;

            return ReadInt32();
        }

        public float ReadFloatArrayEntry()
        {
            if (!TryMatchEntryName(null))
                return 0f;

            return ReadFloat32();
        }

        public bool ReadBoolArrayEntry()
        {
            if (!TryMatchEntryName(null))
                return false;

            return ReadBool();
        }

        public string ReadStringArrayEntry()
        {
            if (!TryMatchEntryName(null))
                return null;

            return ReadString();
        }

        private void ReadContainerName(string expectedName)
        {
            ReadNameHash();
        }

        private bool TryMatchEntryName(string expectedName)
        {
            int nameHash = ReadNameHash();

            if (InArray || string.IsNullOrEmpty(expectedName))
                return true;

            return nameHash == GetNameHash(expectedName);
        }

        private int ReadNameHash()
        {
            if (_pos + 4 > _data.Length)
                return 0;

            int hash = BitConverter.ToInt32(_data, _pos);
            _pos += 4;
            return hash;
        }

        private int ReadInt32()
        {
            if (_pos + 4 > _data.Length)
                return 0;

            int value = BitConverter.ToInt32(_data, _pos);
            _pos += 4;
            return value;
        }

        private float ReadFloat32()
        {
            if (_pos + 4 > _data.Length)
                return 0f;

            float value = BitConverter.ToSingle(_data, _pos);
            _pos += 4;
            return value;
        }

        private bool ReadBool()
        {
            if (_pos >= _data.Length)
                return false;

            return _data[_pos++] != 0;
        }

        private string ReadString()
        {
            if (_pos + 2 > _data.Length)
                return null;

            ushort length = BitConverter.ToUInt16(_data, _pos);
            _pos += 2;

            if (length == 0)
                return string.Empty;

            if (_pos + length > _data.Length)
                return null;

            string value = Encoding.UTF8.GetString(_data, _pos, length);
            _pos += length;
            return value;
        }

        private static int GetNameHash(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;

            unchecked
            {
                int hash = 17;
                foreach (char character in name)
                    hash = hash * 31 + character;

                return hash;
            }
        }
    }
}
