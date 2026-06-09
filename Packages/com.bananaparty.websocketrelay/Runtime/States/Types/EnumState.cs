using System;

namespace BananaParty.WebSocketRelay
{
    public class EnumState<T> : IState where T : struct, Enum
    {
        public string StateName { get; private set; }
        public T Value { get; set; }

        public EnumState(string name, T initialValue)
        {
            StateName = name;
            Value = initialValue;
        }

        public void WriteState(IStateOutput stateOutput) =>
            stateOutput.WriteInt(StateName, Convert.ToInt32(Value));

        public void ReadState(IStateInput stateInput) =>
            Value = (T)Enum.ToObject(typeof(T), stateInput.ReadInt(StateName));
    }
}
