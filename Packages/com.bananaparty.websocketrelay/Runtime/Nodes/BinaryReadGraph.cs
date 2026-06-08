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

        private const byte ObjectOpen = 0x01;
        private const byte ObjectClose = 0x02;
        private const byte ArrayOpen = 0x03;
        private const byte ArrayClose = 0x04;
        private const byte Int32Type = 0x10;
        private const byte Float32Type = 0x11;
        private const byte BoolType = 0x12;
        private const byte StringType = 0x13;

        public BinaryReadGraph(byte[] data)
        {
            _data = data ?? Array.Empty<byte>();
        }

        public void StartObject(string name)
        {
            ReadMarker(ObjectOpen);
            ReadContainerName(name);
            _inArrayStack.Push(false);
        }

        public void EndObject()
        {
            ReadMarker(ObjectClose);
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public void StartArray(string name)
        {
            ReadMarker(ArrayOpen);
            ReadContainerName(name);
            _inArrayStack.Push(true);
        }

        public void EndArray()
        {
            ReadMarker(ArrayClose);
            if (_inArrayStack.Count > 0)
                _inArrayStack.Pop();
        }

        public string ReadEntry(string name)
        {
            byte type = ReadTypeMarker();
            if (!TryMatchEntryName(name))
                return null;

            return ReadValueAsString(type);
        }

        public int ReadIntEntry(string name)
        {
            byte type = ReadTypeMarker();
            if (!TryMatchEntryName(name))
                return 0;

            if (type != Int32Type)
                return 0;

            return ReadInt32();
        }

        public float ReadFloatEntry(string name)
        {
            byte type = ReadTypeMarker();
            if (!TryMatchEntryName(name))
                return 0f;

            if (type != Float32Type)
                return 0f;

            return ReadFloat32();
        }

        public bool ReadBoolEntry(string name)
        {
            byte type = ReadTypeMarker();
            if (!TryMatchEntryName(name))
                return false;

            if (type != BoolType)
                return false;

            return ReadBool();
        }

        public string ReadStringEntry(string name)
        {
            byte type = ReadTypeMarker();
            if (!TryMatchEntryName(name))
                return null;

            if (type != StringType)
                return null;

            return ReadString();
        }

        public int ReadIntArrayEntry()
        {
            byte type = ReadTypeMarker();
            ReadName();
            if (type != Int32Type)
                return 0;

            return ReadInt32();
        }

        public float ReadFloatArrayEntry()
        {
            byte type = ReadTypeMarker();
            ReadName();
            if (type != Float32Type)
                return 0f;

            return ReadFloat32();
        }

        public bool ReadBoolArrayEntry()
        {
            byte type = ReadTypeMarker();
            ReadName();
            if (type != BoolType)
                return false;

            return ReadBool();
        }

        public string ReadStringArrayEntry()
        {
            byte type = ReadTypeMarker();
            ReadName();
            if (type != StringType)
                return null;

            return ReadString();
        }

        private void ReadMarker(byte expected)
        {
            if (_pos >= _data.Length || _data[_pos] != expected)
                return;

            _pos++;
        }

        private byte ReadTypeMarker()
        {
            if (_pos >= _data.Length)
                return 0;

            return _data[_pos++];
        }

        private void ReadContainerName(string expectedName)
        {
            ReadName();
        }

        private bool TryMatchEntryName(string expectedName)
        {
            string entryName = ReadName();

            if (InArray || string.IsNullOrEmpty(expectedName))
                return true;

            return entryName == expectedName;
        }

        private string ReadName()
        {
            if (_pos + 2 > _data.Length)
                return null;

            ushort length = BitConverter.ToUInt16(_data, _pos);
            _pos += 2;

            if (length == 0)
                return string.Empty;

            if (_pos + length > _data.Length)
                return null;

            string name = Encoding.UTF8.GetString(_data, _pos, length);
            _pos += length;
            return name;
        }

        private string ReadValueAsString(byte type)
        {
            switch (type)
            {
                case Int32Type:
                    return ReadInt32().ToString();
                case Float32Type:
                    return ReadFloat32().ToString();
                case BoolType:
                    return ReadBool().ToString().ToLowerInvariant();
                case StringType:
                    return ReadString();
                default:
                    return null;
            }
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
    }
}
