namespace BananaParty.WebSocketRelay
{
    public interface IStateInput
    {
        void StartObject(string name);
        void EndObject();
        void StartArray(string name);
        void EndArray();
        string ReadEntry(string name);
        int ReadIntEntry(string name);
        float ReadFloatEntry(string name);
        bool ReadBoolEntry(string name);
        string ReadStringEntry(string name);
        int ReadIntArrayEntry();
        float ReadFloatArrayEntry();
        bool ReadBoolArrayEntry();
        string ReadStringArrayEntry();
    }
}
