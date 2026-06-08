namespace BananaParty.WebSocketRelay
{
    public interface IWriteGraph
    {
        void StartObject(string name);
        void EndObject();
        void StartArray(string name);
        void EndArray();
        void WriteEntry(string name, int value);
        void WriteEntry(string name, float value);
        void WriteEntry(string name, bool value);
        void WriteEntry(string name, string value);
        void WriteEntry(int value);
        void WriteEntry(float value);
        void WriteEntry(bool value);
        void WriteArrayEntry(string value);
    }
}
