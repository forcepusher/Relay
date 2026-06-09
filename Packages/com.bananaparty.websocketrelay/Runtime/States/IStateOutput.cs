using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IStateOutput
    {
        void WriteObject(string name, List<IState> states);
        void WriteStaticArray(string name, List<IState> states);
        void WriteDynamicArray(string name, List<IState> states);
        void WriteInt(string name, int value);
        void WriteLong(string name, long value);
        void WriteFloat(string name, float value);
        void WriteDouble(string name, double value);
        void WriteBool(string name, bool value);
        void WriteString(string name, string value);
    }
}
