using System;
using System.Collections.Generic;
using System.Text;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStateOutput : IStateOutput
    {
        private readonly List<byte> _buffer = new();
        private readonly Stack<bool> _inArrayStack = new();

        private bool InArray => _inArrayStack.Count > 0 && _inArrayStack.Peek();

        public void WriteObject(string name, List<IState> states)
        {
            StartObject(name);

            foreach (IState state in states)
                state.WriteState(this);

            EndObject();
        }

        public void WriteStaticArray(string name, List<IState> states)
        {
            StartArray(name);

            foreach (IState state in states)
                state.WriteState(this);

            EndArray();
        }

        public void WriteDynamicArray(string name, List<IState> states)
        {
            StartArray(name);
            WriteEntry(states.Count);

            foreach (IState state in states)
                state.WriteState(this);

            EndArray();
        }

        public void WriteByte(string name, byte value) => WriteEntry(name, value);

        public void WriteInt(string name, int value) => WriteEntry(name, value);

        public void WriteLong(string name, long value) => WriteEntry(name, value);

        public void WriteFloat(string name, float value) => WriteEntry(name, value);

        public void WriteDouble(string name, double value) => WriteEntry(name, value);

        public void WriteBool(string name, bool value) => WriteEntry(name, value);

        public void WriteString(string name, string value) => WriteEntry(name, value);

        public void WriteGuid(string name, Guid value) => WriteEntry(name, value);

        public byte[] ToArray() => _buffer.ToArray();

        private void StartObject(string name)
        {
            WriteNameHash(name);
            _inArrayStack.Push(false);
        }

        private void EndObject()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        private void StartArray(string name)
        {
            WriteNameHash(name);
            _inArrayStack.Push(true);
        }

        private void EndArray()
        {
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        private void WriteEntry(string name, byte value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.Add(value);
        }

        private void WriteEntry(string name, int value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteEntry(string name, long value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteEntry(string name, float value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteEntry(string name, double value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteEntry(string name, bool value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.Add(value ? (byte)1 : (byte)0);
        }

        private void WriteEntry(string name, string value)
        {
            WriteNameHash(InArray ? null : name);
            byte[] stringBytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
            _buffer.AddRange(BitConverter.GetBytes((ushort)stringBytes.Length));
            _buffer.AddRange(stringBytes);
        }

        private void WriteEntry(string name, Guid value)
        {
            WriteNameHash(InArray ? null : name);
            _buffer.AddRange(value.ToByteArray());
        }

        private void WriteEntry(int value)
        {
            WriteNameHash(null);
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteNameHash(string name)
        {
            _buffer.AddRange(BitConverter.GetBytes(Hash.IntFromName(name)));
        }
    }
}
