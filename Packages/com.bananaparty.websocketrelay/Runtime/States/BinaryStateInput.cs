using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStateInput : IStateInput
    {
        private readonly byte[] _data;
        private int _pos;
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public BinaryStateInput(byte[] data)
        {
            _data = data ?? Array.Empty<byte>();
        }

        public void ReadObject(string name, List<IState> states)
        {
            StartObject(name);

            foreach (IState state in states)
                state.ReadState(this);

            EndObject();
        }

        public void ReadStaticArray(string name, List<IState> states)
        {
            StartArray(name);

            foreach (IState state in states)
                state.ReadState(this);

            EndArray();
        }

        public void ReadDynamicArray(string name, List<IState> states)
        {
            StartArray(name);
            int count = ReadIntArrayEntry();

            while (states.Count > count)
                states.RemoveAt(states.Count - 1);

            for (int i = 0; i < count; i++)
                states[i].ReadState(this);

            EndArray();
        }

        public string ReadString(string name)
        {
            VerifyEntryName(name);
            return ReadStringValue();
        }

        public int ReadInt(string name)
        {
            VerifyEntryName(name);
            return ReadInt32();
        }

        public float ReadFloat(string name)
        {
            VerifyEntryName(name);
            return ReadFloat32();
        }

        public bool ReadBool(string name)
        {
            VerifyEntryName(name);
            return ReadBoolValue();
        }

        private void StartObject(string name)
        {
            VerifyNameHash(name);
            _inArrayStack.Push(false);
        }

        private void EndObject()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        private void StartArray(string name)
        {
            VerifyNameHash(name);
            _inArrayStack.Push(true);
        }

        private void EndArray()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        private int ReadIntArrayEntry()
        {
            VerifyEntryName(null);
            return ReadInt32();
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

        private bool ReadBoolValue()
        {
            if (_pos >= _data.Length)
                return false;

            return _data[_pos++] != 0;
        }

        private string ReadStringValue()
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
