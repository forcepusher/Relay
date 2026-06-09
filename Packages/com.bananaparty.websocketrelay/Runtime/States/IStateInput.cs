using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IStateInput
    {
        void ReadObject(string name, List<IState> states);
        void ReadStaticArray(string name, List<IState> states);
        void ReadDynamicArray(string name, List<IState> states);
        string ReadString(string name);
        byte ReadByte(string name);
        int ReadInt(string name);
        long ReadLong(string name);
        float ReadFloat(string name);
        double ReadDouble(string name);
        bool ReadBool(string name);
        Guid ReadGuid(string name);
    }
}
