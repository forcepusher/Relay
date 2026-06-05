using System;
using UnityEngine;

namespace BananaParty.WebSocketRelay
{
    public static class JsonBuilder
    {
        public static string Build<T>(string name, T value)
        {
            string formattedValue;

            if (value == null)
            {
                formattedValue = "null";
            }
            else if (value is bool b)
            {
                formattedValue = b.ToString().ToLower();
            }
            else if (value is string s)
            {
                string escaped = s.Replace("\\", "\\\\").Replace("\"", "\\\"");
                formattedValue = $"\"{escaped}\"";
            }
            else if (value is Enum e)
            {
                formattedValue = $"\"{e}\"";
            }
            else if (value is Vector2 || value is Vector3 || value is Vector4 ||
                     value is Quaternion || value is Vector2Int || value is Vector3Int)
            {
                formattedValue = $"\"{value}\"";
            }
            else
            {
                formattedValue = value.ToString();
            }

            return $"\"{name}\": {formattedValue}";
        }
    }
}
