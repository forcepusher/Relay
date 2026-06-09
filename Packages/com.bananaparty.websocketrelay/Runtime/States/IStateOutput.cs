using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public interface IStateOutput
    {
        void WriteObject(string name, List<IState> states);
        void WriteArray(string name, List<IState> states);
        void WriteInt(string name, int value);
        void WriteFloat(string name, float value);
        void WriteBool(string name, bool value);
        void WriteString(string name, string value);
    }
}
