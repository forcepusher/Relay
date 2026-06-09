using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IStateInput
    {
        void ReadObject(string name, List<IState> states);
        void ReadStaticArray(string name, List<IState> states);
        void ReadDynamicArray<T>(string name, List<T> states) where T : IState;
        void ReadDynamicArray<T>(string name, List<T> states, IFactory<T> factory) where T : IState;
        void CopyStateFrom(IState source, IState target);
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
