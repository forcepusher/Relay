using System;
using System.Text;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class ByteArrayStateStream : IStateStream
    {
        private byte[] _buffer;
        private int _position;

        public ByteArrayStateStream(int initialCapacity = 1024)
        {
            _buffer = new byte[initialCapacity];
            _position = 0;
        }

        public ByteArrayStateStream(byte[] data)
        {
            _buffer = data;
            _position = 0;
        }

        private void EnsureCapacity(int additionalBytes)
        {
            if (_position + additionalBytes > _buffer.Length)
            {
                int newCapacity = Math.Max(_buffer.Length * 2, _position + additionalBytes);
                Array.Resize(ref _buffer, newCapacity);
            }
        }

        public byte[] ToArray()
        {
            byte[] result = new byte[_position];
            Array.Copy(_buffer, 0, result, 0, _position);
            return result;
        }

        public void Reset()
        {
            _position = 0;
        }

        // Primitives
        public void WriteBool(bool value)
        {
            EnsureCapacity(1);
            _buffer[_position++] = (byte)(value ? 1 : 0);
        }

        public bool ReadBool()
        {
            return _buffer[_position++] != 0;
        }

        public void WriteByte(byte value)
        {
            EnsureCapacity(1);
            _buffer[_position++] = value;
        }

        public byte ReadByte()
        {
            return _buffer[_position++];
        }

        public void WriteInt(int value)
        {
            EnsureCapacity(4);
            _buffer[_position++] = (byte)(value & 0xFF);
            _buffer[_position++] = (byte)((value >> 8) & 0xFF);
            _buffer[_position++] = (byte)((value >> 16) & 0xFF);
            _buffer[_position++] = (byte)((value >> 24) & 0xFF);
        }

        public int ReadInt()
        {
            return _buffer[_position++] | (_buffer[_position++] << 8) | (_buffer[_position++] << 16) | (_buffer[_position++] << 24);
        }

        public void WriteLong(long value)
        {
            EnsureCapacity(8);
            for (int i = 0; i < 8; i++)
            {
                _buffer[_position++] = (byte)((value >> (i * 8)) & 0xFF);
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
            EnsureCapacity(bytes.Length);
            Array.Copy(bytes, 0, _buffer, _position, bytes.Length);
            _position += bytes.Length;
        }

        public string ReadString()
        {
            int length = ReadInt();
            if (length == -1) return null;

            byte[] bytes = new byte[length];
            Array.Copy(_buffer, _position, bytes, 0, length);
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
