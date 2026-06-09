using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStateInput : IReadGraph
    {
        private readonly byte[] _data;
        private int _pos;
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public BinaryStateInput(byte[] data)
        {
            _data = data ?? Array.Empty<byte>();
        }

        public void StartObject(string name)
        {
            VerifyNameHash(name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            VerifyNameHash(name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public string ReadEntry(string name)
        {
            VerifyEntryName(name);
            return ReadInt32().ToString();
        }

        public int ReadIntEntry(string name)
        {
            VerifyEntryName(name);
            return ReadInt32();
        }

        public float ReadFloatEntry(string name)
        {
            VerifyEntryName(name);
            return ReadFloat32();
        }

        public bool ReadBoolEntry(string name)
        {
            VerifyEntryName(name);
            return ReadBool();
        }

        public string ReadStringEntry(string name)
        {
            VerifyEntryName(name);
            return ReadString();
        }

        public int ReadIntArrayEntry()
        {
            VerifyEntryName(null);
            return ReadInt32();
        }

        public float ReadFloatArrayEntry()
        {
            VerifyEntryName(null);
            return ReadFloat32();
        }

        public bool ReadBoolArrayEntry()
        {
            VerifyEntryName(null);
            return ReadBool();
        }

        public string ReadStringArrayEntry()
        {
            VerifyEntryName(null);
            return ReadString();
        }

        private void VerifyEntryName(string expectedName)
        {
            VerifyNameHash(InArray ? null : expectedName);
        }

        private void VerifyNameHash(string expectedName)
        {
            int nameHash = ReadNameHash();
            int expectedHash = GetNameHash(expectedName);

            if (nameHash != expectedHash)
            {
                throw new InvalidDataException(
                    $"Name hash mismatch. Expected '{expectedName ?? string.Empty}' ({expectedHash}), got {nameHash}.");
            }
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
