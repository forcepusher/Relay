using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class JsonStateStream : IStateStream
    {
        private List<string> _writeBuffer;
        private string[] _readBuffer;
        private int _position;

        public JsonStateStream()
        {
            _writeBuffer = new List<string>();
            _position = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < _writeBuffer.Count; i++)
            {
                sb.Append(_writeBuffer[i]);
                if (i < _writeBuffer.Count - 1) sb.Append(",");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public void Reset()
        {
            _position = 0;
        }

        private string CleanRead()
        {
            string val = _readBuffer[_position++].Trim();
            return val.Trim('"', ' ');
        }

        // Primitives
        public void WriteBool(string name, bool value)
        {
            _writeBuffer.Add($"\"{name}\":{(value ? "true" : "false")}");
        }

        public bool ReadBool()
        {
            return CleanRead().ToLower() == "true";
        }

        public void WriteByte(string name, byte value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public byte ReadByte()
        {
            return byte.Parse(CleanRead());
        }

        public void WriteInt(string name, int value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public int ReadInt()
        {
            return int.Parse(CleanRead());
        }

        public void WriteFloat(string name, float value)
        {
            _writeBuffer.Add($"\"{name}\":{value.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
        }

        public float ReadFloat()
        {
            return float.Parse(CleanRead(), System.Globalization.CultureInfo.InvariantCulture);
        }

        public void WriteLong(string name, long value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public long ReadLong()
        {
            return long.Parse(CleanRead());
        }

        public void WriteString(string name, string value)
        {
            if (value == null)
            {
                _writeBuffer.Add($"\"{name}\":null");
                return;
            }
            _writeBuffer.Add($"\"{name}\":\"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"");
        }

        public string ReadString()
        {
            string val = CleanRead();
            if (val == "null") return null;
            return val;
        }

        public void WriteEnum<T>(string name, T value) where T : Enum
        {
            WriteInt(name, Convert.ToInt32(value));
        }

        public T ReadEnum<T>() where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), ReadInt());
        }

        // Unity Types - Serialized as JSON objects for readability/standard
        public void WriteVector2(string name, Vector2 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector2 ReadVector2()
        {
            string val = CleanRead(); // {"x":1,"y":2}
            string xStr = ExtractJsonValue(val, "x");
            string yStr = ExtractJsonValue(val, "y");
            return new Vector2(float.Parse(xStr, System.Globalization.CultureInfo.InvariantCulture), float.Parse(yStr, System.Globalization.CultureInfo.InvariantCulture));
        }

        public void WriteVector3(string name, Vector3 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector3 ReadVector3()
        {
            string val = CleanRead();
            return new Vector3(
                float.Parse(ExtractJsonValue(val, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "z"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteVector4(string name, Vector4 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"w\":{value.w.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector4 ReadVector4()
        {
            string val = CleanRead();
            return new Vector4(
                float.Parse(ExtractJsonValue(val, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "z"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "w"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteQuaternion(string name, Quaternion value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"w\":{value.w.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Quaternion ReadQuaternion()
        {
            string val = CleanRead();
            return new Quaternion(
                float.Parse(ExtractJsonValue(val, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "z"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(val, "w"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteVector2Int(string name, Vector2Int value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x},\"y\":{value.y}}}");
        }

        public Vector2Int ReadVector2Int()
        {
            string val = CleanRead();
            return new Vector2Int(int.Parse(ExtractJsonValue(val, "x")), int.Parse(ExtractJsonValue(val, "y")));
        }

        public void WriteVector3Int(string name, Vector3Int value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x},\"y\":{value.y},\"z\":{value.z}}}");
        }

        public Vector3Int ReadVector3Int()
        {
            string val = CleanRead();
            return new Vector3Int(int.Parse(ExtractJsonValue(val, "x")), int.Parse(ExtractJsonValue(val, "y")), int.Parse(ExtractJsonValue(val, "z")));
        }

        public void WriteColor32(string name, Color32 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"r\":{value.r},\"g\":{value.g},\"b\":{value.b},\"a\":{value.a}}}");
        }

        public Color32 ReadColor32()
        {
            string val = CleanRead();
            return new Color32(byte.Parse(ExtractJsonValue(val, "r")), byte.Parse(ExtractJsonValue(val, "g")), byte.Parse(ExtractJsonValue(val, "b")), byte.Parse(ExtractJsonValue(val, "a")));
        }

        private string ExtractJsonValue(string json, string key)
        {
            string search = $"\"{key}\":";
            int start = json.IndexOf(search);
            if (start == -1) throw new Exception($"Key {key} not found in JSON: {json}");
            start += search.Length;

            // Skip whitespace
            while (start < json.Length && char.IsWhiteSpace(json[start])) start++;

            int end;
            if (json[start] == '\"')
            {
                start++;
                end = json.IndexOf('\"', start);
            }
            else
            {
                end = json.IndexOfAny(new char[] { ',', '}' }, start);
                if (end == -1) end = json.Length;
            }

            return json.Substring(start, end - start).Trim();
        }
    }
}
