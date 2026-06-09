using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IStateInput
    {
        void ReadObject(string name, List<IState> states);
        void ReadStaticArray(string name, List<IState> states);
        void ReadDynamicArray(string name, List<IState> states);
        string ReadString(string name);
        int ReadInt(string name);
        float ReadFloat(string name);
        bool ReadBool(string name);
    }
}
