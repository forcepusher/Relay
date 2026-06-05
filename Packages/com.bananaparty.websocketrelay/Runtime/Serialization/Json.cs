namespace BananaParty.WebSocketRelay
{
    public static class Json
    {
        public static string ConvertToText(string name, object value)
        {
            if (value == null)
                return $"\"{name}\": null";

            string formattedValue;
            if (value is string str)
            {
                formattedValue = $"\"{str.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
            }
            else if (value is bool b)
            {
                formattedValue = b.ToString().ToLower();
            }
            else
            {
                formattedValue = value.ToString();
            }

            return $"\"{name}\": {formattedValue}";
        }
    }
}
