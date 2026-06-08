using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryWriteGraph : IWriteGraph
    {
        private readonly List<byte> _buffer = new();
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        private const byte ObjectOpen = 0x01;
        private const byte ObjectClose = 0x02;
        private const byte ArrayOpen = 0x03;
        private const byte ArrayClose = 0x04;
        private const byte Int32Type = 0x10;
        private const byte Float32Type = 0x11;
        private const byte BoolType = 0x12;
        private const byte StringType = 0x13;

        public void StartObject(string name)
        {
            _buffer.Add(ObjectOpen);
            WriteName(name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            _buffer.Add(ObjectClose);
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            _buffer.Add(ArrayOpen);
            WriteName(name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            _buffer.Add(ArrayClose);
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void WriteEntry(string name, int value) => WriteValue(Int32Type, name, BitConverter.GetBytes(value));

        public void WriteEntry(string name, float value) => WriteValue(Float32Type, name, BitConverter.GetBytes(value));

        public void WriteEntry(string name, bool value) => WriteValue(BoolType, name, new[] { (byte)(value ? 1 : 0) });

        public void WriteEntry(string name, string value)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
            byte[] payload = new byte[2 + stringBytes.Length];
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)stringBytes.Length), 0, payload, 0, 2);
            Buffer.BlockCopy(stringBytes, 0, payload, 2, stringBytes.Length);
            WriteValue(StringType, name, payload);
        }

        public void WriteEntry(int value) => WriteValue(Int32Type, null, BitConverter.GetBytes(value));

        public void WriteEntry(float value) => WriteValue(Float32Type, null, BitConverter.GetBytes(value));

        public void WriteEntry(bool value) => WriteValue(BoolType, null, new[] { (byte)(value ? 1 : 0) });

        public void WriteArrayEntry(string value) => WriteEntry(null, value);

        public byte[] ToArray() => _buffer.ToArray();

        private void WriteValue(byte type, string name, byte[] valueBytes)
        {
            _buffer.Add(type);
            WriteName(InArray ? null : name);
            _buffer.AddRange(valueBytes);
        }

        private void WriteName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _buffer.AddRange(BitConverter.GetBytes((ushort)0));
                return;
            }

            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            _buffer.AddRange(BitConverter.GetBytes((ushort)nameBytes.Length));
            _buffer.AddRange(nameBytes);
        }
    }
}
