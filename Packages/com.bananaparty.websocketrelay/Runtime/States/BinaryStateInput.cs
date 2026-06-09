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

        public void ReadDynamicArray<T>(string name, List<T> states) where T : IKeyedState
        {
            StartArray(name);
            int count = ReadIntArrayEntry();

            while (states.Count > count)
                states.RemoveAt(states.Count - 1);

            if (states.Count < count)
                throw new InvalidOperationException($"Dynamic array '{name}' requires {count} entries but only {states.Count} exist.");

            for (int i = 0; i < count; i++)
                states[i].ReadState(this);

            EndArray();
        }

        public void ReadDynamicArray<T>(string name, List<T> states, IFactory<T> factory) where T : IKeyedState
        {
            StartArray(name);
            int count = ReadIntArrayEntry();

            var incoming = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                T staging = factory.Create(Guid.Empty);
                staging.ReadState(this);
                incoming.Add(staging);
            }

            var stateMap = new Dictionary<Guid, T>();
            foreach (T state in states)
                stateMap[state.StateKey.Value] = state;

            var next = new List<T>(incoming.Count);
            foreach (T staging in incoming)
            {
                Guid key = staging.StateKey.Value;
                if (stateMap.TryGetValue(key, out T existing))
                {
                    CopyStateFrom(staging, existing);
                    next.Add(existing);
                    stateMap.Remove(key);
                }
                else
                {
                    T entry = factory.Create(key);
                    CopyStateFrom(staging, entry);
                    next.Add(entry);
                }
                factory.Dispose(staging);
            }

            foreach (T orphaned in stateMap.Values)
                factory.Dispose(orphaned);

            states.Clear();
            states.AddRange(next);

            EndArray();
        }

        private void CopyStateFrom(IState source, IState target)
        {
            var output = new BinaryStateOutput();
            source.WriteState(output);
            target.ReadState(new BinaryStateInput(output.ToArray()));
        }

        public string ReadString(string name)
        {
            VerifyEntryName(name);
            return ReadStringValue();
        }

        public byte ReadByte(string name)
        {
            VerifyEntryName(name);
            return ReadByteValue();
        }

        public int ReadInt(string name)
        {
            VerifyEntryName(name);
            return ReadInt32();
        }

        public long ReadLong(string name)
        {
            VerifyEntryName(name);
            return ReadInt64();
        }

        public float ReadFloat(string name)
        {
            VerifyEntryName(name);
            return ReadFloat32();
        }

        public double ReadDouble(string name)
        {
            VerifyEntryName(name);
            return ReadFloat64();
        }

        public bool ReadBool(string name)
        {
            VerifyEntryName(name);
            return ReadBoolValue();
        }

        public Guid ReadGuid(string name)
        {
            VerifyEntryName(name);
            return ReadGuidValue();
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
            int expectedHash = Hash.FromName(expectedName);

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

        private byte ReadByteValue()
        {
            if (_pos >= _data.Length)
                return 0;

            return _data[_pos++];
        }

        private int ReadInt32()
        {
            if (_pos + 4 > _data.Length)
                return 0;

            int value = BitConverter.ToInt32(_data, _pos);
            _pos += 4;
            return value;
        }

        private long ReadInt64()
        {
            if (_pos + 8 > _data.Length)
                return 0L;

            long value = BitConverter.ToInt64(_data, _pos);
            _pos += 8;
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

        private double ReadFloat64()
        {
            if (_pos + 8 > _data.Length)
                return 0d;

            double value = BitConverter.ToDouble(_data, _pos);
            _pos += 8;
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

        private Guid ReadGuidValue()
        {
            if (_pos + 16 > _data.Length)
                return Guid.Empty;

            byte[] bytes = new byte[16];
            Array.Copy(_data, _pos, bytes, 0, 16);
            _pos += 16;
            return new Guid(bytes);
        }
    }
}
