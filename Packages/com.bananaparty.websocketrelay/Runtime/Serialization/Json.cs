namespace BananaParty.WebSocketRelay
{
    public static class Json
    {
        public static string ConvertToText(string name, string value)
        {
            if (value == null)
                return $"\"{name}\": null";

            return $"\"{name}\": \"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
        }

        public static string ConvertToText(string name, bool value)
        {
            return $"\"{name}\": {value.ToString().ToLower()}";
        }

        public static string ConvertToText(string name, object value)
        {
            if (value == null)
                return $"\"{name}\": null";

            return $"\"{name}\": {value.ToString()}";
        }
    }
}
