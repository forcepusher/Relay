using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class BinaryStateStream : IStateStream
    {
        private List<byte> _buffer;
        private int _position;

        public BinaryStateStream(int initialCapacity = 1024)
        {
            _buffer = new List<byte>(initialCapacity);
            _position = 0;
        }

        public BinaryStateStream(byte[] data)
        {
            _buffer = new List<byte>(data);
            _position = 0;
        }

        public byte[] ToArray()
        {
            byte[] result = new byte[_position];
            _buffer.CopyTo(0, result, 0, _position);
            return result;
        }

        public void Reset()
        {
            _position = 0;
        }

        private void WriteInternal(byte value)
        {
            if (_position < _buffer.Count)
                _buffer[_position] = value;
            else
                _buffer.Add(value);
            _position++;
        }

        // Primitives
        public void WriteBool(bool value)
        {
            WriteInternal((byte)(value ? 1 : 0));
        }

        public bool ReadBool()
        {
            return _buffer[_position++] != 0;
        }

        public void WriteByte(byte value)
        {
            WriteInternal(value);
        }

        public byte ReadByte()
        {
            return _buffer[_position++];
        }

        public void WriteInt(int value)
        {
            WriteInternal((byte)(value & 0xFF));
            WriteInternal((byte)((value >> 8) & 0xFF));
            WriteInternal((byte)((value >> 16) & 0xFF));
            WriteInternal((byte)((value >> 24) & 0xFF));
        }

        public int ReadInt()
        {
            return _buffer[_position++] | (_buffer[_position++] << 8) | (_buffer[_position++] << 16) | (_buffer[_position++] << 24);
        }

        public void WriteLong(long value)
        {
            for (int i = 0; i < 8; i++)
            {
                WriteInternal((byte)((value >> (i * 8)) & 0xFF));
            }
        }

        public long ReadLong()
        {
            long value = 0;
            for (int i = 0; i < 8; i++)
            {
                value |= (long)_buffer[_position++] << (i * 8);
            }
            return value;
        }

        public void WriteFloat(float value)
        {
            WriteInt(BitConverter.SingleToInt32Bits(value));
        }

        public float ReadFloat()
        {
            return BitConverter.Int32BitsToSingle(ReadInt());
        }

        // Strings and GUIDs
        public void WriteString(string value)
        {
            if (value == null)
            {
                WriteInt(-1);
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            WriteInt(bytes.Length);
            foreach (byte b in bytes) WriteInternal(b);
        }

        public string ReadString()
        {
            int length = ReadInt();
            if (length == -1) return null;

            byte[] bytes = new byte[length];
            _buffer.CopyTo(_position, bytes, 0, length);
            _position += length;
            return Encoding.UTF8.GetString(bytes);
        }

        // Enums
        public void WriteEnum<T>(T value) where T : Enum
        {
            WriteInt(Convert.ToInt32(value));
        }

        public T ReadEnum<T>() where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), ReadInt());
        }

        // Unity Types
        public void WriteVector2(Vector2 value)
        {
            WriteFloat(value.x);
            WriteFloat(value.y);
        }

        public Vector2 ReadVector2()
        {
            return new Vector2(ReadFloat(), ReadFloat());
        }

        public void WriteVector3(Vector3 value)
        {
            WriteFloat(value.x);
            WriteFloat(value.y);
            WriteFloat(value.z);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
        }

        public void WriteVector4(Vector4 value)
        {
            WriteFloat(value.x);
            WriteFloat(value.y);
            WriteFloat(value.z);
            WriteFloat(value.w);
        }

        public Vector4 ReadVector4()
        {
            return new Vector4(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        }

        public void WriteQuaternion(Quaternion value)
        {
            WriteFloat(value.x);
            WriteFloat(value.y);
            WriteFloat(value.z);
            WriteFloat(value.w);
        }

        public Quaternion ReadQuaternion()
        {
            return new Quaternion(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        }

        public void WriteVector2Int(Vector2Int value)
        {
            WriteInt(value.x);
            WriteInt(value.y);
        }

        public Vector2Int ReadVector2Int()
        {
            return new Vector2Int(ReadInt(), ReadInt());
        }

        public void WriteVector3Int(Vector3Int value)
        {
            WriteInt(value.x);
            WriteInt(value.y);
            WriteInt(value.z);
        }

        public Vector3Int ReadVector3Int()
        {
            return new Vector3Int(ReadInt(), ReadInt(), ReadInt());
        }

        public void WriteColor32(Color32 value)
        {
            WriteByte(value.r);
            WriteByte(value.g);
            WriteByte(value.b);
            WriteByte(value.a);
        }

        public Color32 ReadColor32()
        {
            return new Color32(ReadByte(), ReadByte(), ReadByte(), ReadByte());
        }
    }
}
