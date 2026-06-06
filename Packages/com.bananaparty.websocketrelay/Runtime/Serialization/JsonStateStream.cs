using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public class JsonStateStream : IStateNode
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

        public bool ReadBool(string name)
        {
            string val = CleanRead(); // "name":true
            return ExtractJsonValue(val, name).ToLower() == "true";
        }

        public void WriteByte(string name, byte value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public byte ReadByte(string name)
        {
            string val = CleanRead();
            return byte.Parse(ExtractJsonValue(val, name));
        }

        public void WriteInt(string name, int value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public int ReadInt(string name)
        {
            string val = CleanRead();
            return int.Parse(ExtractJsonValue(val, name));
        }

        public void WriteFloat(string name, float value)
        {
            _writeBuffer.Add($"\"{name}\":{value.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
        }

        public float ReadFloat(string name)
        {
            string val = CleanRead();
            return float.Parse(ExtractJsonValue(val, name), System.Globalization.CultureInfo.InvariantCulture);
        }

        public void WriteLong(string name, long value)
        {
            _writeBuffer.Add($"\"{name}\":{value}");
        }

        public long ReadLong(string name)
        {
            string val = CleanRead();
            return long.Parse(ExtractJsonValue(val, name));
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

        public string ReadString(string name)
        {
            string val = CleanRead();
            string result = ExtractJsonValue(val, name);
            if (result == "null") return null;
            return result;
        }

        public void WriteEnum<T>(string name, T value) where T : Enum
        {
            WriteInt(name, Convert.ToInt32(value));
        }

        public T ReadEnum<T>(string name) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), ReadInt(name));
        }

        // Unity Types - Serialized as JSON objects for readability/standard
        public void WriteVector2(string name, Vector2 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector2 ReadVector2(string name)
        {
            string val = CleanRead(); // "name":{"x":1,"y":2}
            string jsonObject = ExtractJsonValue(val, name);
            string xStr = ExtractJsonValue(jsonObject, "x");
            string yStr = ExtractJsonValue(jsonObject, "y");
            return new Vector2(float.Parse(xStr, System.Globalization.CultureInfo.InvariantCulture), float.Parse(yStr, System.Globalization.CultureInfo.InvariantCulture));
        }

        public void WriteVector3(string name, Vector3 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector3 ReadVector3(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Vector3(
                float.Parse(ExtractJsonValue(jsonObject, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "z"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteVector4(string name, Vector4 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"w\":{value.w.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Vector4 ReadVector4(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Vector4(
                float.Parse(ExtractJsonValue(jsonObject, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "z"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "w"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteQuaternion(string name, Quaternion value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"y\":{value.y.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"z\":{value.z.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"w\":{value.w.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}");
        }

        public Quaternion ReadQuaternion(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Quaternion(
                float.Parse(ExtractJsonValue(jsonObject, "x"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "y"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "z"), System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(ExtractJsonValue(jsonObject, "w"), System.Globalization.CultureInfo.InvariantCulture)
            );
        }

        public void WriteVector2Int(string name, Vector2Int value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x},\"y\":{value.y}}}");
        }

        public Vector2Int ReadVector2Int(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Vector2Int(int.Parse(ExtractJsonValue(jsonObject, "x")), int.Parse(ExtractJsonValue(jsonObject, "y")));
        }

        public void WriteVector3Int(string name, Vector3Int value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"x\":{value.x},\"y\":{value.y},\"z\":{value.z}}}");
        }

        public Vector3Int ReadVector3Int(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Vector3Int(int.Parse(ExtractJsonValue(jsonObject, "x")), int.Parse(ExtractJsonValue(jsonObject, "y")), int.Parse(ExtractJsonValue(jsonObject, "z")));
        }

        public void WriteColor32(string name, Color32 value)
        {
            _writeBuffer.Add($"\"{name}\":{{\"r\":{value.r},\"g\":{value.g},\"b\":{value.b},\"a\":{value.a}}}");
        }

        public Color32 ReadColor32(string name)
        {
            string val = CleanRead();
            string jsonObject = ExtractJsonValue(val, name);
            return new Color32(byte.Parse(ExtractJsonValue(jsonObject, "r")), byte.Parse(ExtractJsonValue(jsonObject, "g")), byte.Parse(ExtractJsonValue(jsonObject, "b")), byte.Parse(ExtractJsonValue(jsonObject, "a")));
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
