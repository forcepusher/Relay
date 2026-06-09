using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStateOutput : IWriteGraph
    {
        private readonly List<byte> _buffer = new();
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public void StartObject(string name)
        {
            WriteNameHash(name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            WriteNameHash(name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void WriteEntry(string name, int value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteEntry(string name, float value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteEntry(string name, bool value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.Add(value ? (byte)1 : (byte)0);
        }

        public void WriteEntry(string name, string value)
        {
            WriteNameHash(InArray ? null : name);
            byte[] stringBytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
            _buffer.AddRange(BitConverter.GetBytes((ushort)stringBytes.Length));
            _buffer.AddRange(stringBytes);
        }

        public void WriteEntry(int value)
        {
            WriteNameHash(null);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteEntry(float value)
        {
            WriteNameHash(null);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteEntry(bool value)
        {
            WriteNameHash(null);
            _buffer.Add(value ? (byte)1 : (byte)0);
        }

        public void WriteArrayEntry(string value) => WriteEntry(null, value);

        public byte[] ToArray() => _buffer.ToArray();

        private void WriteNameHash(string name)
        {
            _buffer.AddRange(BitConverter.GetBytes(GetNameHash(name)));
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
